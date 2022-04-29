namespace WorkoutPlannerWebApp.Models
{
    public class WorkoutPhase
    {
        public int Id { get; set; }
        public WorkoutProgram WorkoutProgram { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public string Content { get; set; }
        public IEnumerable<WorkoutDay> WorkoutDays { get; set; }
    }
}
