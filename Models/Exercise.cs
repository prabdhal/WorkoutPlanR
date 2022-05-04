﻿using System.ComponentModel.DataAnnotations;

namespace WorkoutPlannerWebApp.Models
{
  public class Exercise
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter the name of the exercise.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Please enter the description of the exercise.")]
    public string Description { get; set; }

    [Url]
    [Display(Name = "Tutorial Video Link")]
    public string TutorialVideoLink { get; set; }
  }
}
