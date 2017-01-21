using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public abstract class ApplicationDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim,
        TUserToken> : IdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>
        where TUser : IdentityUser<TKey, TUserClaim, TUserRole, TUserLogin>
        where TRole : IdentityRole<TKey, TUserRole, TRoleClaim>
        where TKey : IEquatable<TKey>
        where TUserClaim : IdentityUserClaim<TKey>
        where TUserRole : IdentityUserRole<TKey>
        where TUserLogin : IdentityUserLogin<TKey>
        where TRoleClaim : IdentityRoleClaim<TKey>
        where TUserToken : IdentityUserToken<TKey>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }

        public new DbSet<TRoleClaim> RoleClaims { get; set; }
        public new DbSet<TRole> Roles { get; set; }
        public new DbSet<TUserClaim> UserClaims { get; set; }
        public new DbSet<TUserLogin> UserLogins { get; set; }
        public new DbSet<TUserRole> UserRoles { get; set; }
        public new DbSet<TUser> Users { get; set; }
        public new DbSet<TUserToken> UserTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUserRole>().HasKey(x => new {x.RoleId, x.UserId, x.ClubId});
        }
    }

    public class AppDbContext :
        ApplicationDbContext
        <ApplicationUser, ApplicationRole, int, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
            ApplicationRoleClaim, ApplicationUserToken>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Club> Clubs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUserRole>().HasKey(x => new {x.RoleId, x.UserId, x.ClubId});           
        }
    }
    }