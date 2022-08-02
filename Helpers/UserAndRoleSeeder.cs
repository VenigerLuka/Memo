using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace MemoProject.Helpers
{
    public class UserAndRoleSeeder
    {
        public static void SeedData(UserManager<IdentityUser> userManager)
        {
            SeedUsers(userManager).Wait();
        }
        private static async Task SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (await userManager.FindByEmailAsync("vengierluka@gmail.com") == null)
            {
                IdentityUser user = new()
                {
                    Id = "5f9d16dd-e1bb-4493-8023-2802e5b40b84",
                    UserName = "vecager",
                    Email = "venigerluka@gmail.com",
                };


                IdentityResult result = await userManager.CreateAsync(user, "Luka.22");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
            if (await userManager.FindByEmailAsync("marko.veniger@gmail.com") == null)
            {
                IdentityUser user2 = new()
                {
                    Id = "d4374641-9519-4931-92c8-23e8c452a875",
                    UserName = "mvng",
                    Email = "marko.veniger@gmail.com",

                };
                IdentityResult result2 = await userManager.CreateAsync(user2, "Marko.22");
                if (result2.Succeeded)
                {
                    await userManager.AddToRoleAsync(user2, "Standard");
                }
            }



        }
    }
}
