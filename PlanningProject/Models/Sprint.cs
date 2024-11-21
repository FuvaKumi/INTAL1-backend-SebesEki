using PlanningProject.Models;

namespace PlanningProject.Models
{

    public class Sprint
    {
        public int Id { get; set; }
        public string Self { get; set; }
        public string State { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CreatedDate { get; set; }
        public int OriginBoardId { get; set; }
        public string Goal { get; set; }
        public int MaxPlayers { get; set; } = 10;
        public List<LocalUser>? LocalUsers { get; set; }
        public int UserCount { get; set; } = 0;

        public void updateUserCount()
        {
            UserCount = LocalUsers.Count;
        }

    }

    public class SprintsResponse
    {
        public int MaxResults { get; set; }
        public int StartAt { get; set; }
        public int Total { get; set; }
        public bool IsLast { get; set; }
        public List<Sprint> Values { get; set; }
    }



}
