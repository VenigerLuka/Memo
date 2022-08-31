using System;
using System.Security.Claims;

namespace MemoProject.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal claimsPrincipal)
        {
            string userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new InvalidOperationException("There is no logged in user");
            }
            return userId;
        }
    }
}
