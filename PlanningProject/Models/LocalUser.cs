namespace PlanningProject.Models
{
    public class LocalUser
    {
        public required string UserName { get; set; }
        public int Id { get; set; }
        public int? Card { get; set; } // null, ha még nem választott lapot
        public bool HasChosen()
        {
            return Card != null;
        }
    }
}