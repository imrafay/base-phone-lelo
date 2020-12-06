using Abp.Domain.Repositories;
using PhoneLelo.Project.Authorization.Users;
using Abp.Domain.Services;
using System.Threading.Tasks;
using System.Linq;
using PhoneLelo.Project.Chat.Dto;
using Abp.Linq.Extensions;

namespace PhoneLelo.Project.Chat
{
    public class ChatMessageManager : DomainService
    {
        private readonly IRepository<ChatMessage,long> _chatMessageRepository;
        private readonly IRepository<User,long> _userRepository;

        public ChatMessageManager(
            IRepository<ChatMessage, long> chatMessageRepository,
            IRepository<User, long> userRepository)
        {
            _chatMessageRepository = chatMessageRepository;
            _userRepository = userRepository;
        }


        public IQueryable<ChatMessageDto> GetChatMessages(
            long receiverId,
            long senderId=0)
        {
            var messages = (from chat in _chatMessageRepository.GetAll()
                            
                            .WhereIf(senderId > 0 && receiverId > 0, x =>
                                (x.SenderId == senderId &&
                                x.ReceiverId == receiverId) ||
                                (x.SenderId == receiverId &&
                                x.ReceiverId == senderId))
                            .WhereIf(receiverId > 0,x=>
                                x.SenderId == receiverId ||
                                x.ReceiverId == receiverId)

                            join receiver in _userRepository.GetAll()
                            on chat.ReceiverId equals receiver.Id

                            join sender in _userRepository.GetAll()
                            on chat.SenderId equals sender.Id

                            select new ChatMessageDto 
                            { 
                                SenderId =sender.Id,
                                Date=chat.CreationTime,
                                MessageStatus=chat.MessageStatus,
                                ReceiverId = chat.ReceiverId,
                                ReceiverName =receiver.FullName,
                                SenderName=sender.FullName,
                                Message = chat.Message
                            });

            return messages;
        }

        public async Task CreateAsync(ChatMessage input)
        {
            await _chatMessageRepository.InsertAsync(input);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(ChatMessage input)
        {
            await _chatMessageRepository.UpdateAsync(input);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            await _chatMessageRepository.DeleteAsync(x => x.Id == id);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}
