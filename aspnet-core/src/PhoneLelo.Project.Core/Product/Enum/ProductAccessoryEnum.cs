using System.ComponentModel;

namespace PhoneLelo.Project.Product.Enum
{
    public enum ProductAccessoryEnum
    {
        [Description("Charger")]
        Charger,

        [Description("Wireless Charger")]
        WirelessCharger,

        [Description("Box")]
        Box,

        [Description("HandsFree")]
        HandFree,

        [Description("AirPods")]
        AirPods,
    }

    public enum RamEnum
    {
        [Description("256 MB")]
        TwoHundredAndFiftySixGB = 256,

        [Description("512 MB")]
        FiveHundredAndTwelveGB = 512,

        [Description("1 GB")]
        OneGB = 1,

        [Description("2 GB")]
        TwoGB = 2,

        [Description("3 GB")]
        ThreeGB = 3,

        [Description("4 GB")]
        FourGB = 4,

        [Description("6 GB")]
        SixGB = 6,

        [Description("8 GB")]
        EightGB = 8,

        [Description("10 GB")]
        TenGB = 10,

        [Description("12 GB")]
        TwelveGB = 12,
    }

    public enum StorageEnum
    {
        [Description("1 GB")]
        OneGB = 1,

        [Description("2 GB")]
        TwoGB = 2,

        [Description("4 GB")]
        FourGB = 4,

        [Description("8 GB")]
        EightGB = 8,

        [Description("12 GB")]
        TwelveGB = 12,

        [Description("32 GB")]
        ThirtyTwoGB = 32,

        [Description("64 GB")]
        SixtyFourGB = 64,

        [Description("128 GB")]
        OneHundredAndTwentyEightGB = 128,

        [Description("256 GB")]
        TwoHundredAndFiftySixGB = 256,

        [Description("512 GB")]
        FiveHundredAndTwelveGB = 512,

        [Description("1 TB")]
        OneTB = 1024,
    }
}
