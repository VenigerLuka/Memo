using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemoProject.Helpers
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole<string>>
    {


        public void Configure(EntityTypeBuilder<IdentityRole<string>> builder)
        {
            builder.HasData(
                new IdentityRole<string>
                {
                    Id = "administrator",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "af8be57e-983f-4f30-b4e2-7095dfefa91b"
                },
                new IdentityRole<string>
                {
                    Id = "standardUser",
                    Name = "Standard",
                    NormalizedName = "STANDARD",
                    ConcurrencyStamp = "687d7ec2-6464-409e-9e7b-b53130b35a3e"
                }
            );
        }


    }
}

