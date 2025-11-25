using Core.Entities;
using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LibraryManagement.Infrastructure.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        // Domain Entities
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<BorrowRecord> BorrowRecords { get; set; }

        // Auth Entities
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -----------------------------
            // BOOK
            // -----------------------------
            modelBuilder.Entity<Book>()
                .Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Book>()
                .Property(p => p.Author)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Book>()
                .Property(p => p.ISBN)
                .HasMaxLength(50);

            // -----------------------------
            // MEMBER
            // -----------------------------
            modelBuilder.Entity<Member>()
                .Property(m => m.FullName)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Member>()
                .Property(m => m.Email)
                .HasMaxLength(200);

            // -----------------------------
            // BORROW RECORD (Many-to-One)
            // -----------------------------
            modelBuilder.Entity<BorrowRecord>()
                .HasOne(br => br.Member)
                .WithMany()
                .HasForeignKey(br => br.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BorrowRecord>()
                .HasOne(br => br.Book)
                .WithMany()
                .HasForeignKey(br => br.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            // -----------------------------
            // USER
            // -----------------------------
            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(200)
                .IsRequired();

            // -----------------------------
            // ROLE
            // -----------------------------
            modelBuilder.Entity<Role>()
                .Property(r => r.RoleName)
                .HasMaxLength(100)
                .IsRequired();

            // -----------------------------
            // USERROLE (Many-To-Many)
            // -----------------------------
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);
        }
    }
}
