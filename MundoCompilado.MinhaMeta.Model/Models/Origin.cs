using System.Collections.Generic;

namespace MundoCompilado.MinhaMeta.Model.Models
{
    public class Origin : Base
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Contribuition> Contribuitions { get; set; }
    }
}
