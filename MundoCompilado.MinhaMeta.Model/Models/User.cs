using System.Collections.Generic;

namespace MundoCompilado.MinhaMeta.Model.Models
{
    public class User : Base
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; } = true;
        public virtual ICollection<Goal> Goals { get; set; }
    }
}
