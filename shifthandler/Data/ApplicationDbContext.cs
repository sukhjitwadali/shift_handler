using Microsoft.EntityFrameworkCore;
using shifthandler.Models;
using System.Collections.Generic;

namespace shifthandler.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Shifts> Shifts { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Invitation> Invitations { get; set; }

    }
}
