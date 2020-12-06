namespace PhoneLelo.Project.Pusher.Dto
{
    public class PusherEventInputDto
    {    
        public long UserId { get; set; }
        public RealTimeEventEnum EventEnum { get; set; }
    } 
}
