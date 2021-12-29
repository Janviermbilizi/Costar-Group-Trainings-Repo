using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace API.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {

        }
        public DatabaseContext(DbContextOptions <DatabaseContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}