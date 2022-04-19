﻿using System.ComponentModel.DataAnnotations;
using WorkoutPlannerWebApp.HelperMethods;

namespace WorkoutPlannerWebApp.Models
{
  public class Exercise
  {
    [Key]
    public string Id { get; set; }
    
    [Required(ErrorMessage = "Please enter the name of the exercise.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Please enter the description of the exercise.")]
    public string Description { get; set; }

    public string ReferenceLink { get; set; }

    [Range(1,20)]
    [Required(ErrorMessage = "Please enter the number of sets for this exercise.")]
    public int Sets { get; set; }

    [Range(1, 50)]
    [Required(ErrorMessage = "Please enter the minimum number of repetitions for each set.")]
    [Display(Name = "Minimum Repetitions")]
    public int MinRepetition { get; set; }

    [Range(1, 50)]
    [Display(Name = "Maximum Repetitions")]
    public int MaxRepetition { get; set; }

    //[Required(ErrorMessage = "Please enter the primary muscle group targetted.")]
    //[Display(Name = "Primary Muscle Group Target(s)")]
    //public MuscleGroup PrimaryTarget { get; set; }

    //[Display(Name = "Secondary Muscle Group Target(s)")]
    //public MuscleGroup SecondaryTarget { get; set; }
  }
}
