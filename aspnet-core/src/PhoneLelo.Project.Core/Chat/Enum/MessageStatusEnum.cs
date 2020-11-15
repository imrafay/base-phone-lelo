using System.ComponentModel;

namespace PhoneLelo.Project.Product.Enum
{
    public enum MessageStatusEnum
    {
        [Description("Deliver")]
        Deliver,

        [Description("Received")]
        Received,

        [Description("Seen")]
        Seen,
    }

}
