using MundoCompilado.MinhaMeta.DataAccess;
using MundoCompilado.MinhaMeta.Model.Interfaces;
using System.Linq;

namespace MundoCompilado.MinhaMeta.Domain
{
    public class Domain<TData> : IDomain<TData> where TData : class, IModel
    {
        private IData<TData> _data;

        public Domain(IData<TData> data)
        {
            _data = data;
        }

        public virtual IQueryable<TData> Get()
        {
            return _data.Get();
        }

        public virtual TData Get(int id)
        {
            return _data.Get(id);
        }

        public virtual void Remove(int id)
        {
            _data.Remove(id);
        }

        public virtual TData Save<TViewModel>(TViewModel vm) where TViewModel : class, IKey
        {
            return _data.Save(vm);
        }
    }
}
