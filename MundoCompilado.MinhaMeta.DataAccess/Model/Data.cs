using MundoCompilado.MinhaMeta.Helper;
using MundoCompilado.MinhaMeta.Model.Interfaces;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace MundoCompilado.MinhaMeta.DataAccess.Model
{
    public class Data<TData> : IData<TData> where TData : class, IKey
    {
        protected Context _context;

        public Data(Context context)
        {
            _context = context;
        }

        public virtual IQueryable<TData> Get()
        {
            return _context.Set<TData>().AsQueryable();
        }

        public virtual TData Get(int id)
        {
            return _context.Set<TData>().Find(id);
        }

        public virtual void Remove(int id)
        {
            var model = Get(id);
            _context.Set<TData>().Remove(model);
        }

        public virtual TData Save<TViewModel>(TViewModel vm) where TViewModel : class, IKey
        {
            var entry = GetEntry(vm);
            TData model = null;

            switch (entry.State)
            {
                case System.Data.Entity.EntityState.Detached:
                    model = Get(vm.Id);
                    model = vm.Inject<TViewModel, TData>(model);
                    break;
                case System.Data.Entity.EntityState.Unchanged:
                    break;
                case System.Data.Entity.EntityState.Added:
                    model = vm.ConvertTo<TViewModel, TData>();
                    _context.Set<TData>().Add(model);
                    break;
                case System.Data.Entity.EntityState.Modified:
                    model = Get(vm.Id);
                    model = vm.Inject<TViewModel, TData>(model);
                    break;
                default:
                    throw new InvalidOperationException(entry.State.ToString());
            }
            _context.SaveChanges();
            return model;
        }

        protected virtual DbEntityEntry GetEntry(object model)
        {
            return _context.Entry(model);
        }
    }
}
