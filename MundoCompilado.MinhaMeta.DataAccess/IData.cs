using MundoCompilado.MinhaMeta.Model.Interfaces;
using System.Linq;

namespace MundoCompilado.MinhaMeta.DataAccess
{
    public interface IData<TData> where TData : class, IKey
    {
        IQueryable<TData> Get();

        TData Get(int id);

        TData Save<TViewModel>(TViewModel vm) where TViewModel : class, IKey;

        void Remove(int id);
    }
}
