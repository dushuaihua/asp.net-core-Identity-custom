using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Models
{    
    public class ApplicationUser : IdentityUser<int,ApplicationUserClaim,ApplicationUserRole,ApplicationUserLogin>
    {
    }

    public class Club
    {
        public int Id { get; set; }
    }
   // public class ApplicationRoleClaim : IdentityRoleClaim<int> { }
    public class ApplicationUserClaim : IdentityUserClaim<int> { }
    public class ApplicationUserLogin : IdentityUserLogin<int> { }
    public class ApplicationUserToken : IdentityUserToken<int> { }

    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole> store, IEnumerable<IRoleValidator<ApplicationRole>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<ApplicationRole>> logger, IHttpContextAccessor contextAccessor) : base(store, roleValidators, keyNormalizer, errors, logger, contextAccessor)
        {
        }
    }

    public class ApplicationRoleStore :
        RoleStore<ApplicationRole, AppDbContext, int, ApplicationUserRole, ApplicationRoleClaim>
    {
        public ApplicationRoleStore(AppDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }

        protected override ApplicationRoleClaim CreateRoleClaim(ApplicationRole role, Claim claim)
        {
            throw new NotImplementedException();
        }
    }
    public class ApplicationSignInManager : SignInManager<ApplicationUser>
    {
        public ApplicationSignInManager(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<ApplicationUser>> logger) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger)
        {
        }
    }
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
    }
    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public int ClubId { get; set; }
        public virtual Club Club { get; set; }
    }
    public class ApplicationRole : IdentityRole<int, ApplicationUserRole, ApplicationRoleClaim>
    {       
    }
    public class ApplicationRoleClaim : IdentityRoleClaim<int> { }
    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, AppDbContext, int, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationUserToken>
    {

    }
}