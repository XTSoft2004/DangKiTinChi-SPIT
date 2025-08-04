using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.ContextDB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSet cho các thực thể
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Classes> Classes { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<HistoryMoney> HistoryMoneys { get; set; }
        public DbSet<InfoProxy> InfoProxys { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        // DbSet cho các mối quan hệ
        public DbSet<AccountClasses> AccountClasses { get; set; }   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AccountClasses>()
                .HasKey(dc => new { dc.AccountId, dc.ClassesId });

            modelBuilder.Entity<User>()
               .HasOne(u => u.Role)       // Một User có một Role
               .WithMany(r => r.Users)     // Một Role có nhiều User
               .HasForeignKey(u => u.RoleId)  // User có khóa ngoại trỏ đến Role
               .OnDelete(DeleteBehavior.Restrict); // Khi Role bị xóa, User không bị xóa

            modelBuilder.Entity<TokenUser>()
                .HasOne(u => u.User)
                .WithMany(t => t.TokenUsers)
                .HasPrincipalKey(u => u.Id)       // liên kết tới User.UserId
                .HasForeignKey(t => t.UserId)         // khóa ngoại ở TokenUser
                .OnDelete(DeleteBehavior.SetNull);

            var roleValues = Enum.GetValues(typeof(Role_Enum)).Cast<Role_Enum>().ToArray();
            var roles = roleValues
                .Select((role, index) => new Role
                {
                    Id = -(index + 1), // Sử dụng ID âm cho dữ liệu seed
                    DisplayName = role.GetEnumDisplayName(),
                    CreatedBy = "System"
                })
                .ToArray();
            modelBuilder.Entity<Role>().HasData(roles);

            modelBuilder.Entity<User>().HasData(
               new User
               {
                   Id = -1,
                   UserName = "admin",
                   Password = "admin", // Cần mã hóa mật khẩu trong môi trường sản xuất
                   FullName = "Administrator",
                   RoleId = -1, // Phải khớp với Role.Id = -1
                   Money = 0,
                   CreatedBy = "System",
                   //CreatedDate = DateTime.UtcNow // Bỏ chú thích nếu cần
               }
            );
        }
    }
}