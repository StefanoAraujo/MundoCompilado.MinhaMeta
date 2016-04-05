using System;

namespace MundoCompilado.MinhaMeta.Model.Interfaces
{
    public interface IModel : IKey
    {
        DateTime DtSaved { get; set; }
    }
}
