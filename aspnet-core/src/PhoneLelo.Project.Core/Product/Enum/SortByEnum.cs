using System.ComponentModel;

namespace PhoneLelo.Project.Product.Enum
{
    public enum SortByEnum
    {
        [Description("Name")]
        Name = 1,

        [Description("Newest")]
        Newest = 2,

        [Description("Oldest")]
        Oldest = 3,

        [Description("MaxPrice")]
        MaxPrice = 4,

        [Description("MinPrice")]
        MinPrice = 5,
    }
}
