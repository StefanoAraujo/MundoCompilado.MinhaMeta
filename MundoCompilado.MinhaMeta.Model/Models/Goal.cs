using System.Collections.Generic;

namespace MundoCompilado.MinhaMeta.Model.Models
{
    public class Goal : Base
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Purpose { get; set; }
        public string PurposeName { get; set; } //reais, parcelas,..
        public int UserId { get; set; }
        public User User { get; set; }
        public virtual ICollection<Contribuition> Contribuitions { get; set; }
    }
}
