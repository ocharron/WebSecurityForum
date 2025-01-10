namespace Forum.Entities
{
    public class SecurityLog
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required TypeLog TypeLog { get; set; }
        public required bool TimedOut { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }

    public enum TypeLog
    {
        LogInAttempt,
        LogInAttemptWhileTimedOut,
        SuccessfulLogIn,
        LogOut
    };
}
