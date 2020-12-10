using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TCC.Models;

namespace TCC.Data
{
    public class TCCContext : IdentityDbContext<Usuario>
    {
        public TCCContext (DbContextOptions<TCCContext> options): base(options)
        {
        }

        public DbSet<Fruta> Frutas { get; set; }
        public DbSet<Receita> Receitas { get; set; }

        internal T GetValue<T>(T v)
        {
            throw new NotImplementedException();
        }

        public DbSet<TCC.Models.Receita> Receita { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            modelBuilder.Entity<Usuario>(entity => entity.Property(m => m.NormalizedEmail).HasMaxLength(85));
            modelBuilder.Entity<Usuario>(entity => entity.Property(m => m.NormalizedUserName).HasMaxLength(85));

            modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.NormalizedName).HasMaxLength(85));

            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));

            modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));

            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).HasMaxLength(85));

            modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));

            const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";

            const string ROLE_ID = ADMIN_ID;
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = ROLE_ID,
                Name = "Administrador",
                NormalizedName = "ADMINISTRADOR"
            },
            new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Ajudante",
                NormalizedName = "AJUDANTE"
            });

            var hasher = new PasswordHasher<Usuario>();
            modelBuilder.Entity<Usuario>().HasData(new Usuario
            {
                Id = ADMIN_ID,
                Nome = "Admin",
                UserName = "admin@healthier.com.br",
                NormalizedUserName = "ADMIN@HEALTHIER.COM.BR",
                Email = "admin@healthier.com.br",
                NormalizedEmail = "ADMIN@HEALTHIER.COM.BR",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "123456"),
                SecurityStamp = hasher.GetHashCode().ToString()
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });


        }

    }
}
