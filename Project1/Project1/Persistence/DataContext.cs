﻿using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<Customer>
    {
        public DataContext()
        { }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { 
        }

        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<LocationProductInfo> LocationProductInfoes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    }
}
