namespace Inspinia_MVC5_SeedProject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ABContext : DbContext
    {
        public ABContext()
            : base("name=ABContext")
        {
        }

        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Cours> Courses { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branch>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Branch>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.Branch)
                .HasForeignKey(e => e.branch_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.Students)
                .WithRequired(e => e.Branch)
                .HasForeignKey(e => e.branch_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Branch)
                .HasForeignKey(e => e.branch_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cours>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Cours>()
                .Property(e => e.key)
                .IsUnicode(false);

            modelBuilder.Entity<Cours>()
                .HasMany(e => e.Enrollments)
                .WithRequired(e => e.Cours)
                .HasForeignKey(e => e.course_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cours>()
                .HasMany(e => e.Results)
                .WithRequired(e => e.Cours)
                .HasForeignKey(e => e.course_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Enrollments)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.student_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Results)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.student_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.address)
                .IsUnicode(false);
        }
    }
}