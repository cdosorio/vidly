namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'5ebb3746-e79d-48bf-9b08-91e11c572f9f', N'cdosorio@outlook.com', 0, N'AJQwHl8E0LkYZJxxImeLvFy0qENlLJTrZ3IsOX7Lu36Kn+hkPeid4/WJ/KZmsbxiBQ==', N'3f18ce93-1ca7-414f-a2b3-109a99fcebea', NULL, 0, 0, NULL, 1, 0, N'cdosorio@outlook.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'b27b9fe8-2c56-41cd-92bf-1ff2ebd59c31', N'cdosorio@gmail.com', 0, N'AHjVRh8r3y9eKjgVqel2Xdl9dmv2n3qMKBl6zKr0+e/8OGkUYC54wE16WgcATtj2kA==', N'c17a5882-19c3-4afa-b441-a6d4e088790c', NULL, 0, 0, NULL, 1, 0, N'cdosorio@gmail.com')

                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'558a04da-89cb-46e4-a150-de78d0e913c8', N'CanManageMovies')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5ebb3746-e79d-48bf-9b08-91e11c572f9f', N'558a04da-89cb-46e4-a150-de78d0e913c8')
                ");

        }

        public override void Down()
        {
        }
    }
}
