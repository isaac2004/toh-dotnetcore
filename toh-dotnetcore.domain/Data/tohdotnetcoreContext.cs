﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tohdotnetcore;

namespace tohdotnetcore.domain.Models
{
    public class tohdotnetcoreContext : DbContext
    {
        public tohdotnetcoreContext (DbContextOptions<tohdotnetcoreContext> options)
            : base(options)
        {
        }

        public DbSet<Hero> Hero { get; set; }
    }
}
