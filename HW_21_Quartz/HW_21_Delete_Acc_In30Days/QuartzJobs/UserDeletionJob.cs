using HW_21_Delete_Acc_In30Days.Models;
using Quartz;

namespace HW_21_Delete_Acc_In30Days.QuartzJobs
{
    [DisallowConcurrentExecution]
    public class UserDeletionJob : IJob
    {
        private static List<User> _users = new()
        {
            new User { Id = 1, Name = "user1", DeleteAt = DateTime.Now.AddSeconds(0) },
            new User { Id = 2, Name = "user2", DeleteAt = DateTime.Now.AddSeconds(+5) },
            new User { Id = 3, Name = "user3", DeleteAt = null },
            new User { Id = 4, Name = "user4", DeleteAt = DateTime.Now.AddSeconds(-15) },
            new User { Id = 5, Name = "user5", DeleteAt = DateTime.Now.AddSeconds(+10) },
            new User { Id = 6, Name = "user6", DeleteAt = DateTime.Now.AddSeconds(+15) },
            new User { Id = 7, Name = "user7", DeleteAt = DateTime.Now.AddSeconds(+20) },
        };

        private readonly ILogger<UserDeletionJob> _logger;
        public UserDeletionJob(ILogger<UserDeletionJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            // Пусть будут юзеры которые запросили удаление 10 сек назад - для наглядности тестирования в консоле...
            var usersToDelete = _users.Where(u => u.DeleteAt != null && (DateTime.Now - u.DeleteAt.Value).TotalSeconds >= 10).ToList();

            // Пользователи которых необходимо уведомить о удалении ( прошло 5 сек после запроса (и не более 10cек))
            var usersToNotife = _users.Where(u => u.DeleteAt != null && 
                            (DateTime.Now - u.DeleteAt.Value).TotalSeconds >= 5 && 
                            (DateTime.Now - u.DeleteAt.Value).TotalSeconds <10).ToList();


            // Уведомляем....
            foreach (var user in usersToNotife)
            {
                _logger.LogInformation($"Notification for {user.Name}: Your account will be deleted in 5 seconds))) hurry to cancel the deletion :)  \n");
            }

            // Удаляем....
            foreach (var user in usersToDelete)
            {
                _users.Remove(user);
                _logger.LogInformation($"User {user.Name} was deleted...\n");
            }

            return Task.CompletedTask;
        }
    }
}
