using MundoCompilado.MinhaMeta.Model.Interfaces;
using System;

namespace MundoCompilado.MinhaMeta.Model.Models
{
    public class Base : IModel
    {
        public int Id { get; set; }
        public DateTime DtSaved { get; set; } = DateTime.Now;
    }
}
