using Microsoft.AspNetCore.Identity;

namespace LibApp.Models
{
    public static class SeedUsers
    {
        public static void Seed(UserManager<IdentityUser> userMenager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoleData(roleManager);
            //SeedUserData(userMenager);
        }
        public static void SeedUserData(UserManager<IdentityUser> userMenager)
        {
            if (userMenager.FindByEmailAsync("owner@test.com").Result == null)
            {
                var user = new IdentityUser
                {
                    UserName = "Owner",
                    Email = "owner@test.com",
                    PasswordHash = "Test123!",
                };
                userMenager.CreateAsync(user).Wait();
                userMenager.AddToRoleAsync(user, "Owner").Wait();
            }

            if (userMenager.FindByEmailAsync("menager@test.com").Result == null)
            {
                var user = new IdentityUser
                {
                    UserName = "Menager",
                    Email = "menager@test.com",
                    PasswordHash = "Test123!",
                };
                userMenager.CreateAsync(user).Wait();
                userMenager.AddToRoleAsync(user, "Menager").Wait();
            }

            if (userMenager.FindByEmailAsync("user@test.com").Result == null)
            {
                var user = new IdentityUser
                {
                    UserName = "User",
                    Email = "user@test.com",
                    PasswordHash = "Test123!",
                };
                userMenager.CreateAsync(user).Wait();
                userMenager.AddToRoleAsync(user, "User").Wait();
            }
        }
        public static void SeedRoleData(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Owner").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Owner"
                };
                roleManager.CreateAsync(role);
            }

            if (!roleManager.RoleExistsAsync("Manager").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Menager"
                };
                roleManager.CreateAsync(role);
            }

            if (!roleManager.RoleExistsAsync("User").Result)
            {
                var role = new IdentityRole
                {
                    Name = "User"
                };
                roleManager.CreateAsync(role);
            }
        }
    }
}