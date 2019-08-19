using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ChallengeBackend.Models
{
    public class UserLogin : IdentityUserLogin<int>
    { }

    public class UserRole : IdentityUserRole<int>
    { }

    public class UserClaim : IdentityUserClaim<int>
    { }
    public class Role : IdentityRole<int, UserRole>
    {
        public Role() { }
        public Role(string name) { Name = name; }
    }

    public class UserStore : UserStore<User, Role, int,
    UserLogin, UserRole, UserClaim>
    {
        public UserStore(ApplicationDbContext context)
          : base(context)
        {
        }
    }

    public class RoleStoreIntPk : RoleStore<Role, int, UserRole>
    {
        public RoleStoreIntPk(ApplicationDbContext context)
           : base(context)
        {
        }
    }

    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
        public User()
        {
            this.User_Rooms = new HashSet<User_Room>();
            this.Messages = new HashSet<Message>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }


        public virtual ICollection<User_Room> User_Rooms { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public ApplicationDbContext()
           : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            var context = new ApplicationDbContext();
            context.Database.CreateIfNotExists();
            ChallengeBackend.Models.Helpers.DBContextHelper.Instance.Context = context;
            return context;
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<User_Room> User_Rooms { get; set; }
    }
    /*
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }*/
}