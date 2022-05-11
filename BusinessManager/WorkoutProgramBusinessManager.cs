using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
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

        public IndexWorkoutProgramViewModel GetIndexWorkoutProgramsViewModel(string searchString, int page = 0)
        {
            var programs = workoutProgramService.GetPublishedWorkoutProgramList(searchString ?? String.Empty);
            
            const int pageSize = 10;
            var count = programs.Count();
            var data = programs.Skip(page * pageSize).Take(pageSize).ToList();

            var maxPage = (count / pageSize) - (count % pageSize == 0 ? 1 : 0);

            return new IndexWorkoutProgramViewModel
            {
                WorkoutPrograms = data,
                SearchString = searchString,
                PageNumber = page,
                MaxPage = maxPage,
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
