using Microsoft.EntityFrameworkCore;
using Task_API.Models;

namespace Task_API.Dbcontext
{
    public class Applicationdbcontext : DbContext
    {
        public Applicationdbcontext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Signup_Model> Signup_tbl { get; set; }
    }
}
