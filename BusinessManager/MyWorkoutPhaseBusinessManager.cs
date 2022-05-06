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

        public CreateWorkoutPhaseViewModel GetCreateWorkoutPhaseViewModel(int programId)
        {
            var program = workoutProgramService.GetWorkoutProgram(programId);

            return new CreateWorkoutPhaseViewModel
            {
                WorkoutProgram = program,
                WorkoutPhase = null,
            };
        }

        public EditWorkoutPhaseViewModel GetEditWorkoutPhaseViewModel(int phaseId)
        {
            var phase = workoutPhaseService.GetWorkoutPhase(phaseId, ModelType.WorkoutPhase);

            var program = workoutProgramService.GetWorkoutProgram(phase.WorkoutProgram.Id);

            return new EditWorkoutPhaseViewModel
            {
                WorkoutProgram = program,
                WorkoutPhase = phase,
            };
        }

        public EditWorkoutDayViewModel GetEditWorkoutDayViewModel(int dayId)
        {
            var day = workoutDayService.GetWorkoutDay(dayId, ModelType.WorkoutDay);
            var phase = workoutPhaseService.GetWorkoutPhase(day.WorkoutPhase.Id, ModelType.WorkoutPhase);
            var program = workoutProgramService.GetWorkoutProgram(day.WorkoutProgram.Id);

            return new EditWorkoutDayViewModel
            {
                WorkoutProgram = program,
                WorkoutPhase = phase,
                WorkoutDay = day
            };
        }

        public ViewWorkoutPhaseViewModel GetViewWorkoutPhasesViewModel(int programId, int? dayId)
        {
            var program = workoutProgramService.GetWorkoutProgram(programId);

            var phases = workoutPhaseService.GetWorkoutPhaseList(programId, ModelType.WorkoutProgram);

            return new ViewWorkoutPhaseViewModel
            {
                WorkoutProgram = program,
                WorkoutPhases = phases,
                WorkoutPhase = null,
                CopyWorkoutLinkId = dayId,
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

        public async Task<ActionResult<WorkoutDay>> EditWorkoutDay(CreateExerciseViewModel editViewModel)
        {
            var day = workoutDayService.GetWorkoutDay(editViewModel.WorkoutDay.Id, ModelType.WorkoutDay);
            var phase = workoutPhaseService.GetWorkoutPhase(day.WorkoutPhase.Id, ModelType.WorkoutPhase);

            day.Name = editViewModel.WorkoutDay.Name;
            day.Description = editViewModel.WorkoutDay.Description;

            var p = workoutPhaseService.UpdateWorkoutPhaseSync(phase);
            var d = await workoutDayService.UpdateWorkoutDay(day);

            return day;
        }

        public async Task<WorkoutPhase> CreateWorkoutPhase(CreateWorkoutPhaseViewModel createViewModel)
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
                    DayNumber = i + 1,
                    Name = "Rest",
                    Description = null,
                    CustomExercises = null,
                };
                workoutDays.Add(day);
            }
            phase.WorkoutDays = workoutDays;

            await workoutPhaseService.AddWorkoutPhase(phase);

            return phase;
        }

        public ActionResult<WorkoutPhase> EditWorkoutPhase(EditWorkoutPhaseViewModel editViewModel)
        {
            var phase = workoutPhaseService.GetWorkoutPhase(editViewModel.WorkoutPhase.Id, ModelType.WorkoutPhase);

            phase.Name = editViewModel.WorkoutPhase.Name;
            phase.Duration = editViewModel.WorkoutPhase.Duration;
            phase.PhaseDetails = editViewModel.WorkoutPhase.PhaseDetails;

            workoutPhaseService.UpdateWorkoutPhaseSync(phase);
            return phase;
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
            day.Description = null;
            day.CustomExercises = null;

            await workoutDayService.UpdateWorkoutDay(day);

            return day;
        }

        public async Task<WorkoutDay> PasteWorkoutDay(int copyDayId, int pasteDayId)
        {
            var copyDay = workoutDayService.GetWorkoutDay(copyDayId, ModelType.WorkoutDay);
            var pasteDay = workoutDayService.GetWorkoutDay(pasteDayId, ModelType.WorkoutDay);

            pasteDay.Name = copyDay.Name;
            pasteDay.Description = copyDay.Description;
            
            foreach (var exercise in pasteDay.CustomExercises)
            {
                var day = await exerciseService.RemoveCustomExercise(exercise);
            }
            pasteDay.CustomExercises = null;

            List<CustomExercise> exerciseList = new List<CustomExercise>();

            foreach (var exercise in copyDay.CustomExercises)
            {
                var customExercise = new CustomExercise
                {
                    ExerciseId = exercise.ExerciseId,
                    Exercise = exercise.Exercise,
                    Sets = exercise.Sets,
                    MinRepetition = exercise.MinRepetition,
                    MaxRepetition = exercise.MaxRepetition,
                    RestInterval = exercise.RestInterval,
                    WorkoutProgram = pasteDay.WorkoutProgram,
                    WorkoutPhase = pasteDay.WorkoutPhase,
                    WorkoutDay = pasteDay,
                };
                var day = await exerciseService.AddCustomExercise(customExercise);
                exerciseList.Add(customExercise);
            }
            pasteDay.CustomExercises = exerciseList;

            var d = await workoutDayService.UpdateWorkoutDay(pasteDay);
            return d;
        }
    }
}
