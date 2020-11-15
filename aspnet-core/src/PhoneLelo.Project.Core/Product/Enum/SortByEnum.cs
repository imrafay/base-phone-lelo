using System.ComponentModel;

namespace PhoneLelo.Project.Product.Enum
{
    public enum SortByEnum
    {
        [Description("Name")]
        Name = 1,

        [Description("CreationTime DESC")]
        Newest = 2,

        [Description("CreationTime")]
        Oldest = 3,

        [Description("Price DESC")]
        MaxPrice = 4,

        [Description("Price")]
        MinPrice = 5,
    }

    public enum SortByDateEnum
    {
        [Description("CreationTime DESC")]
        Newest = 1,

        [Description("CreationTime")]
        Oldest = 2,
    }
}
