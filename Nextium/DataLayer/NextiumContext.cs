using Microsoft.EntityFrameworkCore;
using Nextium.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nextium.DataLayer
{
    public class NextiumContext : DbContext
    {
        public NextiumContext(DbContextOptions<NextiumContext> options) : base(options) { }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Requirements> Requirements { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Donation> Donations { get; set; }

    }
}
