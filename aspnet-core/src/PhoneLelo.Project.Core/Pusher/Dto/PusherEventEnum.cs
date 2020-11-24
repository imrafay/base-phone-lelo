using System.ComponentModel;

namespace PhoneLelo.Project.Pusher.Dto
{
    public enum RealTimeEventEnum
    {
        [Description("test-user")]
        TestUser,

        [Description("chat-message")]
        ChatMessage,
    }
}
