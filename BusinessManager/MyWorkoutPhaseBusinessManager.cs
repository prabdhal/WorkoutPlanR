using Microsoft.AspNetCore.Identity;
using WorkoutPlannerWebApp.BusinessManager.Interfaces;
using WorkoutPlannerWebApp.Models;
using WorkoutPlannerWebApp.Services.Interfaces;
using WorkoutPlannerWebApp.ViewModels.MyWorkoutProgramsViewModels;

namespace WorkoutPlannerWebApp.BusinessManager
{
    public class MyWorkoutPhaseBusinessManager : IMyWorkoutPhaseBusinessManager
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWorkoutProgramService workoutProgramService;
        private readonly IWorkoutPhaseService workoutPhaseService;


        public MyWorkoutPhaseBusinessManager(
            UserManager<ApplicationUser> userManager,
            IWorkoutProgramService workoutProgramService,
            IWorkoutPhaseService workoutPhaseService)
        {
            this.userManager = userManager;
            this.workoutPhaseService = workoutPhaseService;
            this.workoutProgramService = workoutProgramService;
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

        public Task<WorkoutPhase> DeleteWorkoutPhase(int id)
        {
            throw new NotImplementedException();
        }

        public CreateWorkoutPhaseMyWorkoutProgramViewModel GetCreateWorkoutPhaseMyWorkoutProgramViewModel(int programId)
        {
            var program = workoutProgramService.GetWorkoutProgram(programId);

            var phases = workoutPhaseService.GetWorkoutPhaseList(programId);

            return new CreateWorkoutPhaseMyWorkoutProgramViewModel
            {
                WorkoutProgram = program,
                WorkoutPhases = phases,
                WorkoutPhase = null,
            };
        }

        public WorkoutPhase GetWorkoutPhase(int phaseId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WorkoutPhase> GetWorkoutPhases()
        {
            throw new NotImplementedException();
        }
    }
}
