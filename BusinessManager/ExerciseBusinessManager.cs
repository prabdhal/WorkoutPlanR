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
        private readonly IWorkoutProgramService workoutProgramService;
        private readonly IWorkoutPhaseService workoutPhaseService;
        private readonly IWorkoutDayService workoutDayService;
        private readonly IExerciseService exerciseService;


        public ExerciseBusinessManager(
            IWorkoutProgramService workoutProgramService,
            IWorkoutPhaseService workoutPhaseService,
            IWorkoutDayService workoutDayService,
            IExerciseService exerciseService)
        {
            this.workoutProgramService = workoutProgramService;
            this.workoutPhaseService = workoutPhaseService;
            this.workoutDayService = workoutDayService;
            this.exerciseService = exerciseService;
        }

        public CustomExercise GetCustomExercise(int exerciseId)
        {
            var exercise = exerciseService.GetCustomExercise(exerciseId, ModelType.CustomExercise);
            var phase = workoutPhaseService.GetWorkoutPhase(exercise.WorkoutProgram.Id, ModelType.WorkoutProgram);
            var day = workoutDayService.GetWorkoutDay(exercise.WorkoutProgram.Id, ModelType.WorkoutProgram);

            exercise.WorkoutPhase = phase;
            exercise.WorkoutDay = day;

            return exercise;
        }

        public IEnumerable<Exercise> GetExercises()
        {
            return exerciseService.GetExerciseList();
        }

        public CreateExerciseViewModel GetCreateExerciseMyWorkoutProgramsViewModel(int id, ModelType modelType, out IEnumerable<Exercise> exercises)
        {
            var day = workoutDayService.GetWorkoutDay(id, modelType);
            var program = workoutProgramService.GetWorkoutProgram(day.WorkoutProgram.Id);
            var phase = workoutPhaseService.GetWorkoutPhase(day.WorkoutPhase.Id, ModelType.WorkoutPhase);

            var e = exerciseService.GetExerciseList();

            var customExercises = exerciseService.GetCustomExerciseList(id, ModelType.WorkoutDay);

            exercises = e;

            return new CreateExerciseViewModel
            {
                WorkoutProgram = program,
                WorkoutPhase = phase,
                WorkoutDay = day,
                Exercises = e,
                CustomExercises = customExercises,
                CustomExercise = null,
            };
        }

        public async Task<CustomExercise> CreateCustomExercise(CreateExerciseViewModel createViewModel)
        {
            var customExercise = createViewModel.CustomExercise;

            var day = workoutDayService.GetWorkoutDay(createViewModel.WorkoutDay.Id, ModelType.WorkoutDay);
            var program = workoutProgramService.GetWorkoutProgram(day.WorkoutProgram.Id);
            var phase = workoutPhaseService.GetWorkoutPhase(program.Id, ModelType.WorkoutProgram);
            var exercise = exerciseService.GetExercise(createViewModel.CustomExercise.ExerciseId);

            customExercise.Exercise = exercise;
            customExercise.WorkoutDay = day;
            customExercise.WorkoutPhase = phase;
            customExercise.WorkoutProgram = program;

            await exerciseService.AddCustomExercise(customExercise);
            return customExercise;
        }

        public async Task<CustomExercise> DeleteCustomExercise(int id, ModelType modelType)
        {
            var customExercise = exerciseService.GetCustomExercise(id, modelType);

            await exerciseService.RemoveCustomExercise(customExercise);
            return customExercise;
        }
    }
}
