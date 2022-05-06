﻿using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorkoutPlannerWebApp.Models;
using WorkoutPlannerWebApp.ViewModels;

namespace WorkoutPlannerWebApp.BusinessManager.Interfaces
{
    public interface IMyWorkoutProgramBusinessManager
    {
        IndexViewModel GetIndexMyWorkoutProgramsViewModel(string searchString);
        CreateMyWorkoutProgramViewModel GetCreateMyWorkoutProgramsViewModel();
        DetailMyWorkoutProgramViewModel GetDetailMyWorkoutProgramsViewModel(int programId);
        EditMyWorkoutProgramViewModel GetEditMyWorkoutProgramsViewModel(int programId);
        WorkoutProgram GetWorkoutProgram(int programId);
        Task<WorkoutProgram> CreateWorkoutProgram(CreateMyWorkoutProgramViewModel createViewModel, ClaimsPrincipal claims);
        Task<ActionResult<WorkoutProgram>> EditWorkoutProgram(EditMyWorkoutProgramViewModel editViewModel);
        Task<ActionResult<WorkoutProgram>> DeleteWorkoutProgram(int programId);
    }
}
