using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Lab_4.Models;

namespace Lab_4.Data
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@mail.ru";
            string adminPassword = "Admin";
            string managerEmail = "manager@mail.ru";
            string managerPassword = "Manager";
            string userEmail = "user@mail.ru";
            string userPassword = "User";

            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("manager") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("manager"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail };
                IdentityResult adminResult = await userManager.CreateAsync(admin, adminPassword);
                if (adminResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }

                User manager = new User { Email = managerEmail, UserName = managerEmail };
                IdentityResult managerResult = await userManager.CreateAsync(manager, managerPassword);
                if (managerResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(manager, "manager");
                }

                User user = new User { Email = userEmail, UserName = userEmail };
                IdentityResult userResult = await userManager.CreateAsync(user, userPassword);
                if (userResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "user");
                }
            }
        }
    }
}