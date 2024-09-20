namespace ProcrastinatorBackend.DTO
{
    public class TaskDTO
    {
        public int Userid { get; set; }
        public string? Task1 { get; set; }
        public DateOnly Deadline { get; set; }
        public string? Details { get; set; }

        public Boolean Iscomplete { get; set; }
        public DateOnly Created { get; set; }
    }
}
