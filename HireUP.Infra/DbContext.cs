using HireUP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HireUP.Infra
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Culture> Cultures { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Hackaton> Hackatons { get; set; }
        public DbSet<Problem> Problems { get; set; }
        public DbSet<Solution> Solutions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HireUP");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Url).IsRequired().HasMaxLength(500);
            });

            modelBuilder.Entity<Culture>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.Icon).IsRequired();
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Experience>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasConversion<string>();
            });

            // Employer configuration
            modelBuilder.Entity<Employer>(entity =>
            {   
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Document).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.Role).HasMaxLength(100);
                entity.Property(e => e.GeoLocationId).HasMaxLength(100);
                entity.Property(e => e.CreatedAt).IsRequired();

                // 1:1 relationship with Attachment (PhotoId)
                entity.HasOne(e => e.PhotoId)
                    .WithMany()
                    .HasForeignKey("AttachmentId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);

                // N:N relationship with Skill
                entity.HasMany(e => e.Skill)
                    .WithMany()
                    .UsingEntity<Dictionary<string, object>>(
                        "EmployerSkill",
                        j => j.HasOne<Skill>().WithMany().HasForeignKey("SkillId"),
                        j => j.HasOne<Employer>().WithMany().HasForeignKey("EmployerId"));

                // N:N relationship with Experience
                entity.HasMany(e => e.Experience)
                    .WithMany()
                    .UsingEntity<Dictionary<string, object>>(
                        "EmployerExperience",
                        j => j.HasOne<Experience>().WithMany().HasForeignKey("ExperienceId"),
                        j => j.HasOne<Employer>().WithMany().HasForeignKey("EmployerId"));
            });

            // Enterprise configuration
            modelBuilder.Entity<Enterprise>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Document).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(1000); 
                entity.Property(e => e.Field).HasMaxLength(50); 
                entity.Property(e => e.GeoLocationId).HasMaxLength(100);
                entity.Property(e => e.CreatedAt).IsRequired();

                // 1:1 relationship with Attachment (PhotoId)
                entity.HasOne(e => e.PhotoId)
                    .WithMany()
                    .HasForeignKey("AttachmentId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false); 
            });

            // Event configuration
            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.IsPrivate).IsRequired();
                entity.Property(e => e.CodeAcess).HasMaxLength(30);
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.StartDate).IsRequired();
                entity.Property(e => e.EndDate).IsRequired();

                // N:1 relationship with Enterprise
                entity.HasOne<Enterprise>()
                    .WithMany()
                    .HasForeignKey(e => e.EnterpriseId)
                    .OnDelete(DeleteBehavior.Restrict);

                // N:N relationship with Attachment
                entity.HasMany(e => e.Attachments)
                    .WithMany()
                    .UsingEntity<Dictionary<string, object>>(
                        "EventAttachment",
                        j => j.HasOne<Attachment>().WithMany().HasForeignKey("AttachmentId"),
                        j => j.HasOne<Event>().WithMany().HasForeignKey("EventId"));

                // N:N relationship with Employer
                entity.HasMany(e => e.EmployeesIds)
                    .WithMany()
                    .UsingEntity<Dictionary<string, object>>(
                        "EventEmployer",
                        j => j.HasOne<Employer>().WithMany().HasForeignKey("EmployerId"),
                        j => j.HasOne<Event>().WithMany().HasForeignKey("EventId"));
            });

            // Hackaton configuration
            modelBuilder.Entity<Hackaton>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.IsPrivate).IsRequired();
                entity.Property(e => e.CodeAcess).HasMaxLength(50);
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.StartDate).IsRequired();
                entity.Property(e => e.EndDate).IsRequired();

                // N:1 relationship with Enterprise
                entity.HasOne<Enterprise>()
                    .WithMany()
                    .HasForeignKey(e => e.EnterpriseId)
                    .OnDelete(DeleteBehavior.Restrict);

                // N:N relationship with Attachment
                entity.HasMany(e => e.Attachments)
                    .WithMany()
                    .UsingEntity<Dictionary<string, object>>(
                        "HackatonAttachment",
                        j => j.HasOne<Attachment>().WithMany().HasForeignKey("AttachmentId"),
                        j => j.HasOne<Hackaton>().WithMany().HasForeignKey("HackatonId"));

                // N:N relationship with Employer
                entity.HasMany(e => e.EmployeesIds)
                    .WithMany()
                    .UsingEntity<Dictionary<string, object>>(
                        "HackatonEmployer",
                        j => j.HasOne<Employer>().WithMany().HasForeignKey("EmployerId"),
                        j => j.HasOne<Hackaton>().WithMany().HasForeignKey("HackatonId"));
            });

            // Problem configuration
            modelBuilder.Entity<Problem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.CreatedAt);

                // N:1 relationship with Enterprise
                entity.HasOne<Enterprise>()
                    .WithMany()
                    .HasForeignKey(e => e.EnterpriseId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Solution configuration
            modelBuilder.Entity<Solution>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(50);
                entity.Property(e => e.CreatedAt);

                // N:1 relationship with Problem
                entity.HasOne<Problem>()
                    .WithMany()
                    .HasForeignKey(e => e.ProblemId)
                    .OnDelete(DeleteBehavior.Restrict);

                // N:1 relationship with Employer
                entity.HasOne<Employer>()
                    .WithMany()
                    .HasForeignKey(e => e.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);

                // 1:1 relationship with Attachment
                entity.HasOne(e => e.Attachment)
                    .WithMany()
                    .HasForeignKey("AttachmentId")
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}