﻿using WorkoutPlannerWebApp.Models;

namespace WorkoutPlannerWebApp.ViewModels.MyWorkoutProgramsViewModels
{
    public class CreateWorkoutPhaseMyWorkoutProgramViewModel
    {
        public WorkoutProgram WorkoutProgram { get; set; }
        public IEnumerable<WorkoutPhase> WorkoutPhases { get; set; }
        public WorkoutPhase WorkoutPhase { get; set; }
    }
}
