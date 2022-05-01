using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkoutPlannerWebApp.BusinessManager.Interfaces;
using WorkoutPlannerWebApp.Models;
using WorkoutPlannerWebApp.Services.Interfaces;
using WorkoutPlannerWebApp.ViewModels;
using WorkoutPlannerWebApp.ViewModels.MyWorkoutProgramsViewModels;

namespace WorkoutPlannerWebApp.BusinessManager
{
    public class MyWorkoutPhaseBusinessManager : IMyWorkoutPhaseBusinessManager
    {
        private readonly IWorkoutProgramService workoutProgramService;
        private readonly IWorkoutPhaseService workoutPhaseService;
        private readonly IWorkoutDayService workoutDayService;
        private readonly IExerciseService exerciseService;


        public MyWorkoutPhaseBusinessManager(
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

        public CreateWorkoutPhaseMyWorkoutProgramViewModel GetCreateWorkoutPhaseMyWorkoutProgramViewModel(int programId)
        {
            var program = workoutProgramService.GetWorkoutProgram(programId);

            var phases = workoutPhaseService.GetWorkoutPhaseList(programId, ModelType.WorkoutProgram);

            return new CreateWorkoutPhaseMyWorkoutProgramViewModel
            {
                WorkoutProgram = program,
                WorkoutPhases = phases,
                WorkoutPhase = null,
            };
        }

        public WorkoutDay GetWorkoutDay(int id, ModelType modelType)
        {
            return workoutDayService.GetWorkoutDay(id, modelType);
        }

        public WorkoutPhase GetWorkoutPhase(int phaseId, ModelType modelType)
        {
            return workoutPhaseService.GetWorkoutPhase(phaseId, ModelType.WorkoutPhase);
        }

        public async Task<WorkoutPhase> CreateWorkoutPhase(CreateWorkoutPhaseMyWorkoutProgramViewModel createViewModel)
        {
            var program = workoutProgramService.GetWorkoutProgram(createViewModel.WorkoutProgram.Id);
            var phase = createViewModel.WorkoutPhase;

            phase.WorkoutProgram = program;

            int days = 7;
            List<WorkoutDay> workoutDays = new List<WorkoutDay>();

            for (int i = 0; i < days; i++)
            {
                WorkoutDay day = new WorkoutDay
                {
                    WorkoutProgram = program,
                    WorkoutPhase = phase,
                    Name = "Rest",
                    CustomExercises = null,
                };
                workoutDays.Add(day);
            }
            phase.WorkoutDays = workoutDays;

            await workoutPhaseService.AddWorkoutPhase(phase);

            return phase;
        }

        public ActionResult<WorkoutDay> EditWorkoutDay(CreateExerciseMyWorkoutProgramViewModel editViewModel)
        {
            var day = workoutDayService.GetWorkoutDay(editViewModel.WorkoutDay.Id, ModelType.WorkoutDay);

            day.Name = editViewModel.WorkoutDay.Name;

            workoutDayService.UpdateWorkoutDay(day);

            return day;
        }

        public async Task<WorkoutPhase> DeleteWorkoutPhase(int phaseId)
        {
            var phase = workoutPhaseService.GetWorkoutPhase(phaseId, ModelType.WorkoutPhase);
            var days = workoutDayService.GetWorkoutDayList(phaseId, ModelType.WorkoutPhase);
            var exercises = exerciseService.GetCustomExerciseList(phaseId, ModelType.WorkoutPhase);

            // delete exercises
            foreach (var exercise in exercises)
            {
                await exerciseService.RemoveCustomExercise(exercise);
            }

            // delete days
            foreach (var day in days)
            {
                await workoutDayService.RemoveWorkoutDay(day);
            }

            // delete phase
            await workoutPhaseService.RemoveWorkoutPhase(phase);
            return phase;
        }

        public async Task<ActionResult<WorkoutDay>> ClearWorkoutDay(int dayId)
        {
            var day = workoutDayService.GetWorkoutDay(dayId, ModelType.WorkoutDay);
            var exercises = exerciseService.GetCustomExerciseList(dayId, ModelType.WorkoutDay);

            // delete exercises
            foreach (var exercise in exercises)
            {
                await exerciseService.RemoveCustomExercise(exercise);
            }

            // reset day
            day.Name = "Rest";
            day.CustomExercises = null;

            workoutDayService.UpdateWorkoutDay(day);

            return day;
        }
    }
}
