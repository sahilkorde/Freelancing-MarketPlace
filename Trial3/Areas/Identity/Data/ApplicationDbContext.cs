﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Trial3.Models;

namespace Trial3.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.Entity<Project>()
            .HasOne(x => x.Employer)
            .WithMany(x => x.UserProjects)
            .HasForeignKey(x => x.EmployerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Project>()
            .HasOne(x => x.Freelancer)
            .WithMany(x => x.AcceptedProject)
            .HasForeignKey(x => x.FreelancerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Bid>()
            .HasOne(x => x.Freelancer)
            .WithMany(x => x.Bids)
            .HasForeignKey(x => x.FreelancerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Bid>()
            .HasOne(x => x.Project)
            .WithMany(x => x.ProjectBids)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Review>()
            .HasOne(x => x.User)
            .WithMany(x => x.UserReviews)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Messages>()
            .HasOne(x => x.MessageBox)
            .WithMany(x => x.Messages)
            .HasForeignKey(x => x.MessageBoxId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<MessageBox>()
            .HasOne(x => x.Employer)
            .WithMany(x => x.EmployerMessageBoxes)
            .HasForeignKey(x => x.EmployerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<MessageBox>()
            .HasOne(x => x.Freelancer)
            .WithMany(x => x.FreelacerMessageBoxes)
            .HasForeignKey(x => x.FreelancerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<MessageBox>()
            .HasOne(x => x.Project)
            .WithOne(x => x.Messagebox)
            .OnDelete(DeleteBehavior.Cascade);

    }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Bid> Bids { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<MessageBox> MessageBoxes { get; set; }
    public DbSet<Messages> Messages { get; set; }

}
