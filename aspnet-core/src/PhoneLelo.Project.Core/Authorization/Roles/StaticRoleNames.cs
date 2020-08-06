namespace PhoneLelo.Project.Authorization.Roles
{
    public static class StaticRoleNames
    {
        public static class Host
        {
            public const string Admin = "Admin";
        }

        public static class Tenants
        {
            public const string Admin = "Admin";
            public const string ShopOwner = "ShopOwner";
            public const string Seller = "Seller";

            public const string AdminDisplayName = "Admin";
            public const string ShopOwnerDisplayName = "Shop Owner";
            public const string SellerDisplayName = "Individual Seller";
        }
    }
}
