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


        public MyWorkoutProgramBusinessManager(
            UserManager<ApplicationUser> userManager,
            IWorkoutProgramService workoutProgramService)
        {
            this.userManager = userManager;
            this.workoutProgramService = workoutProgramService;
        }


        public WorkoutProgram GetWorkoutProgram(int programId)
        {
            return workoutProgramService.GetWorkoutProgram(programId);
        }

        public IndexMyWorkoutProgramViewModel GetIndexMyWorkoutProgramsViewModel(string searchString)
        {
            var programs = workoutProgramService.GetWorkoutProgramList(searchString ?? String.Empty);

            return new IndexMyWorkoutProgramViewModel()
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

        public async Task<ActionResult<WorkoutProgram>> EditWorkoutProgram(EditMyWorkoutProgramViewModel editViewModel)
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

            program = await workoutProgramService.UpdateWorkoutProgram(program);
            return program;
        }

        public async Task<ActionResult<WorkoutProgram>> DeleteWorkoutProgram(int programId)
        {
            var program = workoutProgramService.GetWorkoutProgram(programId);
            if (program is null)
                return new NotFoundResult();

            await workoutProgramService.RemoveWorkoutProgram(program);
            return program;
        }
    }
}
