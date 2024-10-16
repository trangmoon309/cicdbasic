﻿using DevopsAssignment.Models;
using Microsoft.EntityFrameworkCore;

namespace DevopsAssignment.Database
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
