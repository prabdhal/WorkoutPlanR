using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorkoutPlannerWebApp.BusinessManager.Interfaces;
using WorkoutPlannerWebApp.Models;
using WorkoutPlannerWebApp.Services.Interfaces;
using WorkoutPlannerWebApp.ViewModels;

namespace WorkoutPlannerWebApp.BusinessManager
{
    public class MyWorkoutProgramBusinessManager : IMyWorkoutProgramBusinessManager
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWorkoutProgramService workoutProgramService;
        private readonly IWorkoutPhaseService workoutPhaseService;
        private readonly IWorkoutDayService workoutDayService;
        private readonly IExerciseService exerciseService;


        public MyWorkoutProgramBusinessManager(
            UserManager<ApplicationUser> userManager,
            IWorkoutProgramService workoutProgramService,
            IWorkoutPhaseService workoutPhaseService,
            IWorkoutDayService workoutDayService,
            IExerciseService exerciseService)
        {
            this.userManager = userManager;
            this.workoutProgramService = workoutProgramService;
            this.workoutPhaseService = workoutPhaseService;
            this.workoutDayService = workoutDayService;
            this.exerciseService = exerciseService;
        }


        public WorkoutProgram GetWorkoutProgram(int programId)
        {
            return workoutProgramService.GetWorkoutProgram(programId);
        }

        public IndexViewModel GetIndexMyWorkoutProgramsViewModel(string searchString)
        {
            var programs = workoutProgramService.GetWorkoutProgramList(searchString ?? String.Empty);

            return new IndexViewModel()
            {
                WorkoutPrograms = programs,
            };
        }

        public CreateMyWorkoutProgramViewModel GetCreateMyWorkoutProgramsViewModel()
        {
            return new CreateMyWorkoutProgramViewModel
            {
                WorkoutProgram = null,
            };
        }

        public DetailMyWorkoutProgramViewModel GetDetailMyWorkoutProgramsViewModel(int programId)
        {
            var program = workoutProgramService.GetWorkoutProgram(programId);

            return new DetailMyWorkoutProgramViewModel
            {
                WorkoutProgram = program,
            };
        }

        public EditMyWorkoutProgramViewModel GetEditMyWorkoutProgramsViewModel(int programId)
        {
            var program = workoutProgramService.GetWorkoutProgram(programId);

            return new EditMyWorkoutProgramViewModel
            {
                WorkoutProgram = program,
            };
        }

        public async Task<WorkoutProgram> CreateWorkoutProgram(CreateMyWorkoutProgramViewModel createViewModel, ClaimsPrincipal claims)
        {
            var program = createViewModel.WorkoutProgram;

            program.Publisher = await userManager.GetUserAsync(claims);
            program.CreatedOn = DateTime.Now;
            program.UpdatedOn = DateTime.Now;

            program = await workoutProgramService.AddWorkoutProgram(program);
            return program;
        }

        public ActionResult<WorkoutProgram> EditWorkoutProgram(EditMyWorkoutProgramViewModel editViewModel)
        {
            var program = workoutProgramService.GetWorkoutProgram(editViewModel.WorkoutProgram.Id);
            if (program is null)
                return new NotFoundResult();

            program.Name = editViewModel.WorkoutProgram.Name;
            program.ShortDescription = editViewModel.WorkoutProgram.ShortDescription;
            program.Difficulty = editViewModel.WorkoutProgram.Difficulty;
            program.LongDescription = editViewModel.WorkoutProgram.LongDescription;
            program.UpdatedOn = DateTime.Now;
            program.Published = editViewModel.WorkoutProgram.Published;

            program = workoutProgramService.UpdateWorkoutProgram(program);
            return program;
        }

        public async Task<ActionResult<WorkoutProgram>> DeleteWorkoutProgram(int programId)
        {
            var program = workoutProgramService.GetWorkoutProgram(programId);
            if (program is null)
                return new NotFoundResult();
            
            var phases = workoutPhaseService.GetWorkoutPhaseList(programId, ModelType.WorkoutProgram);
            var days = workoutDayService.GetWorkoutDayList(programId, ModelType.WorkoutProgram);
            var exercises = exerciseService.GetCustomExerciseList(programId, ModelType.WorkoutProgram);

            // Delete custom exercises per day
            foreach (var exercise in exercises)
            {
                await exerciseService.RemoveCustomExercise(exercise);
            }

            // Delete all 7 days per phase
            foreach (var day in days)
            {
                await workoutDayService.RemoveWorkoutDay(day);
            }

            // Delete all workout phases 
            foreach (var phase in phases)
            {
                await workoutPhaseService.RemoveWorkoutPhase(phase);
            }

            // Delete workout program
            await workoutProgramService.RemoveWorkoutProgram(program);
            return program;
        }
    }
}
