namespace HW_05.Models
{
    public static class DBInitializer
    {
        public static void DbInit(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { Name = "123", Email = "123@gmail.com", Password = "123", Token = Guid.NewGuid().ToString() },
                    new User { Name = "user", Email = "user@yahoo.com", Password = "123", Token = Guid.NewGuid().ToString() }
                );
                context.SaveChanges();
            }
        }
    }
}
