using Microsoft.AspNetCore.Identity;
using WorkoutPlannerWebApp.BusinessManager.Interfaces;
using WorkoutPlannerWebApp.Models;
using WorkoutPlannerWebApp.Services.Interfaces;
using WorkoutPlannerWebApp.ViewModels;
using WorkoutPlannerWebApp.ViewModels.WorkoutProgramsViewModel;

namespace WorkoutPlannerWebApp.BusinessManager
{
    public class WorkoutProgramBusinessManager : IWorkoutProgramBusinessManager
    {

        private readonly IWorkoutProgramService workoutProgramService;
        private readonly IWorkoutPhaseService workoutPhaseService;


        public WorkoutProgramBusinessManager(
            IWorkoutProgramService workoutProgramService,
            IWorkoutPhaseService workoutPhaseService)
        {
            this.workoutProgramService = workoutProgramService;
            this.workoutPhaseService = workoutPhaseService;
        }

        public IndexWorkoutProgramViewModel GetIndexWorkoutProgramsViewModel(string searchString)
        {
            var programs = workoutProgramService.GetPublishedWorkoutProgramList(searchString ?? String.Empty);

            return new IndexWorkoutProgramViewModel
            {
                WorkoutPrograms = programs,
                SearchString = searchString,
            };
        }

        public DetailWorkoutProgramViewModel GetDetailWorkoutProgramsViewModel(int programId)
        {
            var program = workoutProgramService.GetWorkoutProgram(programId);

            return new DetailWorkoutProgramViewModel
            {
                WorkoutProgram = program,
            };
        }

        public PhaseDetailWorkoutProgramViewModel GetPhaseDetailWorkoutProgramsViewModel(int phaseId)
        {
            var phase = workoutPhaseService.GetWorkoutPhase(phaseId, ModelType.WorkoutPhase);
            var program = workoutProgramService.GetWorkoutProgram(phase.WorkoutProgram.Id);

            return new PhaseDetailWorkoutProgramViewModel
            {
                WorkoutProgram = program,
                WorkoutPhase = phase,
            };
        }

        public FullDetailWorkoutProgramViewModel GetFullDetailWorkoutProgramsViewModel(int programId)
        {
            var program = workoutProgramService.GetWorkoutProgram(programId);

            return new FullDetailWorkoutProgramViewModel
            {
                WorkoutProgram = program,
            };
        }
    }
}
