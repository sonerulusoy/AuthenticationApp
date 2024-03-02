using Microsoft.EntityFrameworkCore;


namespace AuthenticationApp
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(
                @"Data Source=DESKTOP-4R8RU59;
                Initial Catalog=HomeworkDb;
                Integrated Security=True;
                Connect Timeout=30;
                Encrypt=False;
                Trust Server Certificate=False;
                Application Intent=ReadWrite;
                Multi Subnet Failover=False"
            );
        }
        public DbSet<User> Users { get; set; }

    }
}

