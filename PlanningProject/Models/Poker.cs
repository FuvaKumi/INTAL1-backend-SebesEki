namespace PlanningProject.Models
{
    public class Poker
    {
        public required Sprint Sprint { get; set; }
        public List<Task>? Tasks { get; set; }
        public int MaxPlayers { get; set; } = 10;
        public List<LocalUser>? LocalUsers { get; set; }

        /// <returns>The number of<c>LocalUser</c>-s in<c>Poker</c></returns>
        public int LocalUserCount()
        {
            return (LocalUsers != null) ? LocalUsers.Count : 0;
        }

        /// <returns><c>true</c>if every<c>LocalUser</c>is ready.</returns>
        public bool IsEveryoneReady()
        {
            foreach (var user in LocalUsers)
            {
                if (!user.HasChosen())
                {
                    return false;
                }
            }
            return true;
        }

        /// <returns>The average of every<c>LocalUser</c>-s cards.</returns>
        public double GetCardsAverage()
        {
            return (double)LocalUsers.Select(x => x.Card).Average();
        }
    }
}