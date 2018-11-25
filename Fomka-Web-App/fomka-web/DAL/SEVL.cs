namespace fomka_web.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SEVL : DbContext
    {
        public SEVL()
            : base("name=SEVL")
        {
        }

        public virtual DbSet<DifficultyLevel> DifficultyLevels { get; set; }
        public virtual DbSet<Error> Errors { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Institute> Institutes { get; set; }
        public virtual DbSet<Mark> Marks { get; set; }
        public virtual DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public virtual DbSet<Standard> Standards { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DifficultyLevel>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<DifficultyLevel>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.DifficultyLevel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Error>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Error>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Group>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Group>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Group1)
                .HasForeignKey(e => e.Group)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Institute>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Institute>()
                .HasMany(e => e.Groups)
                .WithRequired(e => e.Institute)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Mark>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ProgrammingLanguage>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<ProgrammingLanguage>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.ProgrammingLanguage)
                .HasForeignKey(e => e.PLId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Standard>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Standard>()
                .Property(e => e.StandardFile)
                .IsUnicode(false);

            modelBuilder.Entity<Standard>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.Standard)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Task>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Task>()
                .HasMany(e => e.Marks)
                .WithRequired(e => e.Task)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Marks)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserType>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<UserType>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.UserType)
                .HasForeignKey(e => e.Type)
                .WillCascadeOnDelete(false);
        }
    }
}
