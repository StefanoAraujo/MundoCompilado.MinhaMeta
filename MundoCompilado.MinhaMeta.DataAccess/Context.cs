using MundoCompilado.MinhaMeta.DataAccess.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MundoCompilado.MinhaMeta.DataAccess
{
    public class Context : DbContext
    {
        public Context()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Configuration>());
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var newException = new FormattedDbEntityValidationException(e);
                throw newException;
            }
        }

        /// <summary>
        /// Used to debug and see the models's state (Only for tests)
        /// </summary>
        private void SeeChanges()
        {
            var x = (from t in ChangeTracker.Entries()
                     where t.State == EntityState.Modified
                     select new
                     {
                         Original = t.OriginalValues.PropertyNames.ToDictionary(pn => pn, pn => t.OriginalValues[pn]),
                         Current = t.CurrentValues.PropertyNames.ToDictionary(pn => pn, pn => t.CurrentValues[pn]),
                     });

            var changeInfo = x.Select(t => GetChanges(t.Original, t.Current));
        }

        private static string GetChanges(Dictionary<string, object> original, Dictionary<string, object> current)
        {
            var result = new StringBuilder();
            foreach (var item in original)
            {
                var currentValue = current[item.Key];
                if (((item.Value == null && currentValue != null) || (item.Value != null && currentValue == null)) ||
                    (item.Value != null && !item.Value.Equals(currentValue)))
                {
                    result.AppendLine(string.Format("{0} - Original {1} - Current {2}", item.Key, item.Value, currentValue));
                }
            }

            return result.ToString();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null &&
                        type.BaseType.IsGenericType &&
                        type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                if (type.IsGenericType)
                {
                    continue;
                }

                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);

        }

        /// <summary>
        /// Get DbSet
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>DbSet</returns>
        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }

    #region FormattedDbEntityValidationException
    public class FormattedDbEntityValidationException : Exception
    {
        public FormattedDbEntityValidationException(DbEntityValidationException innerException) :
            base(null, innerException)
        {
        }

        public override string Message
        {
            get
            {
                var innerException = InnerException as DbEntityValidationException;
                if (innerException != null)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine();
                    sb.AppendLine();
                    foreach (var eve in innerException.EntityValidationErrors)
                    {
                        sb.AppendLine(string.Format("- Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().FullName, eve.Entry.State));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            sb.AppendLine(string.Format("-- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                                ve.PropertyName,
                                eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                                ve.ErrorMessage));
                        }
                    }
                    sb.AppendLine();

                    return sb.ToString();
                }

                return base.Message;
            }
        }
    }
    #endregion
}
