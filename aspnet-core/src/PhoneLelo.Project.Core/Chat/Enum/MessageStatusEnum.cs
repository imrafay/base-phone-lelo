using System.ComponentModel;

namespace PhoneLelo.Project.Chat.Enum
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
