#nullable disable
using WorkoutPlannerWebApp.Models;

namespace WorkoutPlannerWebApp.ViewModels
{
    public class IndexViewModel
    {
        public int PageNumber { get; set; }
        public int MaxPage { get; set; }
        public IEnumerable<WorkoutProgram> WorkoutPrograms { get; set; }
    }
}
