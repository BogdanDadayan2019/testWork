using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsCheck
{
    public class ApplicationContext : DbContext
    {

        public DbSet<GOODS> GOODS { get; set; }

        public ApplicationContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseOracle("Data Source=XE;User Id=SYSTEM;Password=name23;");
        }
    }
}