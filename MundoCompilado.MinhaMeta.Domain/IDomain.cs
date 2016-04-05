using MundoCompilado.MinhaMeta.Model.Interfaces;
using System.Linq;

namespace MundoCompilado.MinhaMeta.Domain
{
    public interface IDomain<TData> where TData : class, IModel
    {
        IQueryable<TData> Get();

        TData Get(int id);

        TData Save<TViewModel>(TViewModel vm) where TViewModel : class, IKey;

        void Remove(int id);
    }
}
