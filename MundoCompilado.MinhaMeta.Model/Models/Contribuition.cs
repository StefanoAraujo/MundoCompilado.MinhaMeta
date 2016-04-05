namespace MundoCompilado.MinhaMeta.Model.Models
{
    public class Contribuition : Base
    {
        public decimal Value { get; set; }
        public string Description { get; set; }

        public int GoalId { get; set; }
        public Goal Goal { get; set; }

        public int OriginId { get; set; }
        public Origin Origin { get; set; }
    }
}
