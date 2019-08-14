using System.Threading.Tasks;

namespace DevsTeam.Framework.Core.Activity
{
    public static  class ActivityIndicatorExtensions
    {
        public static async Task<T> TrackActivityWith<T>(this Task<T> task, ActivityIndicatorVm activityIndicator, string description = "", int showAfterMiliseconds = 200)
        {
            await Task.WhenAny(Task.Delay(showAfterMiliseconds), task);
            if (task.IsCompleted) return task.Result;
            using (activityIndicator.StartActivity(description)) return await task;
        }
        
        public static async Task TrackActivityWith(this Task task, ActivityIndicatorVm activityIndicator, string description = "", int showAfterMiliseconds = 200)
        {
            await Task.WhenAny(Task.Delay(showAfterMiliseconds), task);
            if (task.IsCompleted) return;
            using (activityIndicator.StartActivity(description)) await task;
        }
    }
}
