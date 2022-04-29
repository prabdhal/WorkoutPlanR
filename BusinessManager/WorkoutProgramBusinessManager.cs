using Microsoft.AspNetCore.Identity;
using WorkoutPlannerWebApp.BusinessManager.Interfaces;
using WorkoutPlannerWebApp.Models;
using WorkoutPlannerWebApp.Services.Interfaces;
using WorkoutPlannerWebApp.ViewModels;

namespace WorkoutPlannerWebApp.BusinessManager
{
    public class WorkoutProgramBusinessManager : IWorkoutProgramBusinessManager
    {

        private readonly IWorkoutProgramService workoutProgramService;


        public WorkoutProgramBusinessManager(IWorkoutProgramService workoutProgramService)
        {
            this.workoutProgramService = workoutProgramService;
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
    }
}
