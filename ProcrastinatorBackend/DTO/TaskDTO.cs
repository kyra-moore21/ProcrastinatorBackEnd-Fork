namespace ProcrastinatorBackend.DTO
{
    public class TaskDTO
    {
        public int UserId { get; set; }
        public string Task { get; set; }
        public DateOnly Deadline { get; set; }
        public string Details { get; set; }

        public Boolean IsComplete { get; set; }
        public DateOnly Created { get; set; }
    }
}
