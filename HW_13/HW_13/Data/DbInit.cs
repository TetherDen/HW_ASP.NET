using HW_13.Models;
namespace HW_13.Data
{
    public static class DbInit
    {
        public static void Init(ApplicationDbContext db)
        {
            if(!db.Users.Any())
            {
                List<User> users = new List<User>()
                {
                    new User {Email = "123@gmail.com", Password = "123"},
                    new User {Email = "dwa@gmail.com", Password = "dwa"},
                };

                db.Users.AddRange(users);
                db.SaveChanges();
            }


        }
    }
}
