using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcDnevnik.Models;

namespace MvcDnevnik.Data
{
    public class MvcDnevnikContext : DbContext
    {
        public MvcDnevnikContext (DbContextOptions<MvcDnevnikContext> options)
            : base(options)
        {
        }

        public DbSet<MvcDnevnik.Models.Grade> Grade { get; set; } = default!;
    }
}
