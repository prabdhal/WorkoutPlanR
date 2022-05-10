using System.Text.Encodings.Web;
using WorkoutPlannerWebApp.Models;

namespace WorkoutPlannerWebApp.EmailServices
{
    public static class EmailMessageHandler
    {
        public static string EmailSubject(string emailSubject = "Confirmation")
        {
            return $"{ emailSubject } for WorkoutPlanR";
        }

        public static string EmailMessage(ApplicationUser user, string callbackUrl, string subject)
        {
            return ($"Dear { user.FirstName } { user.LastName }," +
                $"<br/>" +
                $"<br/>" +
                $"<br/>" +
                $"Please confirm your { subject.ToLower() } by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>." +
                $"<br/>" +
                $"<br/>" +
                $"<b>Your Account Information:</b>" +
                $"<br/>" +
                $"Full Name: { user.FirstName } { user.LastName }" +
                $"<br/>" +
                $"Email: { user.Email }" +
                $"<br/>" +
                $"<br/>" +
                $"Should you have any questions or concerns, please contact me at <a href='mailto:prab.dhaliwal95@gmail.com'>prab.dhaliwal95@gmail.com</a>" +
                $"<br/>" +
                $"<br/>" +
                $"Regards," +
                $"<br/>" +
                $"<br/>" +
                $"<br/>" +
                "Prabdeep Dhaliwal" +
                $"<br/>" +
                $"<br/>" +
                $"<a href='https://workoutplanr.azurewebsites.net/'>https://workoutplanr.azurewebsites.net/</a>");
        }
    }
}
