using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DotNetCancel.Web;

namespace DotNetCancel.Web.Models
{
    public class DotNetCancelWebContext : DbContext
    {
        public DotNetCancelWebContext (DbContextOptions<DotNetCancelWebContext> options)
            : base(options)
        {
        }

        public DbSet<DotNetCancel.Web.Elephant> Elephant { get; set; }
    }
}
