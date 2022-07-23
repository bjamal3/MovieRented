using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CompanyEmployees.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public class SampleData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<RepositoryContext>();

                string[] roles = new string[] { "Viewer", "Administrator" };

                foreach (string role in roles)
                {
                    var roleStore = new RoleStore<IdentityRole>(context);

                    if (!context.Roles.Any(r => r.Name == role))
                    {
                        roleStore.CreateAsync(new IdentityRole(role));
                    }
                }


                var adminUser = new User
                {
                    FirstName = "Default",
                    LastName = "Admin",
                    Email = "admindefault777@mailinator.com",
                    NormalizedEmail = "admindefault777@mailinator.com".ToUpper(),
                    UserName = "admindefault777@mailinator.com",
                    NormalizedUserName = "admindefault777@mailinator.com".ToUpper(),
                    PhoneNumber = "+111111111111",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                if (!context.Users.Any(u => u.UserName == adminUser.UserName))
                {
                    var password = new PasswordHasher<User>();
                    var hashed = password.HashPassword(adminUser, "Welcome123!");
                    adminUser.PasswordHash = hashed;

                    var userStore = new UserStore<User>(context);
                    var result = await userStore.CreateAsync(adminUser);
                }

                var adminRole = roles[1];
                await AssignRoles(scope.ServiceProvider, adminUser.Email, adminRole);

                await context.SaveChangesAsync();

                var defaultUser = new User
                {
                    FirstName = "Default",
                    LastName = "User",
                    Email = "userdefault777@mailinator.com",
                    NormalizedEmail = "userdefault777@mailinator.com".ToUpper(),
                    UserName = "userdefault777@mailinator.com",
                    NormalizedUserName = "userdefault777@mailinator.com".ToUpper(),
                    PhoneNumber = "+111111111221",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                if (!context.Users.Any(u => u.UserName == defaultUser.UserName))
                {
                    var password = new PasswordHasher<User>();
                    var hashed = password.HashPassword(defaultUser, "Welcome123!");
                    defaultUser.PasswordHash = hashed;

                    var userStore = new UserStore<User>(context);
                    var result = await userStore.CreateAsync(defaultUser);
                }

                var userRole = roles[0];

                await AssignRoles(scope.ServiceProvider, defaultUser.Email, userRole);

                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="email"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string role)
        {
            #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            UserManager<User> _userManager = services.GetService<UserManager<User>>();
            #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            User user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.AddToRoleAsync(user, role);

            return result;
        }
    }
}
