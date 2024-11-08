using EmirhanChatServer.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmirhanChatServer.WebAPI.Context
{
    public class AppLicationDbContext : DbContext
    {
        public AppLicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
    }
}
