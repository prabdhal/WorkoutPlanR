using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkoutPlannerWebApp.BusinessManager.Interfaces;
using WorkoutPlannerWebApp.Models;
using WorkoutPlannerWebApp.Services.Interfaces;
using WorkoutPlannerWebApp.ViewModels;

namespace WorkoutPlannerWebApp.BusinessManager
{
    public class ExerciseBusinessManager : IExerciseBusinessManager
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IExerciseService exerciseService;
        private readonly IWorkoutProgramService workoutProgramService;


        public ExerciseBusinessManager(
            UserManager<ApplicationUser> userManager,
            IExerciseService exerciseService,
            IWorkoutProgramService workoutProgramService)
        {
            this.userManager = userManager;
            this.exerciseService = exerciseService;
            this.workoutProgramService = workoutProgramService;
        }

        public CustomExercise GetCustomExercise(int exerciseId)
        {
            return exerciseService.GetCustomExercise(exerciseId);
        }

        public IEnumerable<Exercise> GetExercises()
        {
            return exerciseService.GetExerciseList();
        }

        public async Task<CustomExercise> CreateCustomExercise(CreateExerciseMyWorkoutProgramViewModel createViewModel)
        {
            var customExercise = createViewModel.CustomExercise;

            var program = workoutProgramService.GetWorkoutProgram(createViewModel.WorkoutProgram.Id);
            var exercise = exerciseService.GetExercise(createViewModel.CustomExercise.ExerciseId);

            customExercise.Exercise = exercise;
            customExercise.WorkoutProgram = program;

            await exerciseService.AddCustomExercise(customExercise);
            return customExercise;
        }

        public async Task<CustomExercise> DeleteCustomExercise(int id)
        {
            var customExercise = exerciseService.GetCustomExercise(id);

            await exerciseService.RemoveCustomExercise(customExercise);
            return customExercise;
        }

        public CreateExerciseMyWorkoutProgramViewModel GetCreateExerciseMyWorkoutProgramsViewModel(int programId, out IEnumerable<Exercise> exercises)
        {
            var program = workoutProgramService.GetWorkoutProgram(programId);

            var e = exerciseService.GetExerciseList();

            var customExercises = exerciseService.GetCustomExerciseList(programId);

            exercises = e;

            return new CreateExerciseMyWorkoutProgramViewModel
            {
                WorkoutProgram = program,
                Exercises = e,
                CustomExercises = customExercises,
                CustomExercise = null,
            };
        }
    }
}
