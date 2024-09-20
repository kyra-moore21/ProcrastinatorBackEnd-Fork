namespace ProcrastinatorBackend.DTO
{
    public class MealPlannerDTO
    {
        public int Userid { get; set; }

        public string? Title { get; set; }

        public string? Url { get; set; }
        public bool? Like { get; set; }
        public bool? Iscompleted { get; set; }
    }
}
