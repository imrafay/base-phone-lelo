using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Authorization.Users;
using Abp.Extensions;
using PhoneLelo.Project.Location;

namespace PhoneLelo.Project.Authorization.Users
{
    public class User : AbpUser<User>
    {
        public virtual string PhoneNumberCode { get; set; }

        public const string DefaultPassword = "123qwe";

        public virtual bool IsLocationFilled { get; set; }

        public long? StateId { get; set; }
        [ForeignKey("StateId")]
        public State StateFk { get; set; }


        public long? CityId { get; set; }
        [ForeignKey("CityId")]
        public City CityFk { get; set; }

        public long? NeighbourhoodId { get; set; }
        [ForeignKey("NeighbourhoodId")]
        public Neighbourhood NeighbourhoodFk { get; set; }

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Roles = new List<UserRole>()
            };

            user.SetNormalizedNames();

            return user;
        }
    }
}
