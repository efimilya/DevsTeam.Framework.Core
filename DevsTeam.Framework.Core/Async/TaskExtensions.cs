using System.Threading.Tasks;

namespace DevsTeam.Framework.Core.Async
{
    public static class TaskExtensions
    {
        public static async void DoNotWait(this Task task) => await task;
    }
}