using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academic_project_manager_WebAPI.Models;

namespace Academic_project_manager_WebAPI.Models
{
    public class AuthenticationContext:IdentityDbContext<ApplicationUser>
    {
        public AuthenticationContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<StudentModel> students { get; set; }
        public DbSet<Supervisor> supervisors { get; set; }
        public DbSet<project> projects { get; set; }
        public DbSet<Group> groups { get; set; }
        public DbSet<Coordinator> Coordinators { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(
                new { Id="1",Name="Admin",NormalizedName="ADMIN"},
                new { Id = "2", Name = "Coordinator", NormalizedName = "COORDINATOR" },
                new { Id = "3", Name = "Student", NormalizedName = "STUDENT" },
                new { Id = "4", Name = "Supervisor", NormalizedName = "SUPERVISOR" }
                );
            builder.Entity<Group>().HasMany(g => g.Student)
                .WithOne(s => s.Group).HasForeignKey(s => s.GroupId);
            builder.Entity<Group>().HasOne(a => a.project)
                .WithOne(b => b.Group).HasForeignKey<project>(b => b.groupId);
            builder.Entity<Supervisor>().HasMany(a => a.Groups)
                .WithOne(s => s.Supervisor).HasForeignKey(s => s.supervisorId);
            builder.Entity<Supervisor>().HasOne(a => a.coordinator)
                .WithOne(s => s.Supervisor).HasForeignKey<Coordinator>(a => a.supervisorId);
            builder.Entity<Coordinator>().HasMany(g => g.Student)
                .WithOne(c => c.Coordinator).HasForeignKey(s => s.coordinatorId);
            builder.HasSequence<int>("sequence", schema: "activity").
                StartsAt(0).IncrementsBy(1);
            builder.Entity<Activities>()
                .Property(p => p.Id).HasDefaultValueSql("NEXT VAlUE FOR activity.sequence");
            builder.Entity<Coordinator>().HasMany(c => c.Activities)
                .WithOne(c => c.Coordinator).HasForeignKey(s => s.coordinatorId);
            builder.Entity<Coordinator>().HasMany(c => c.Groups)
                .WithOne(c => c.Coordinator).HasForeignKey(s => s.coordinatorId);
        }  
        public DbSet<Academic_project_manager_WebAPI.Models.Activities> Activities { get; set; }
        
    }
}
