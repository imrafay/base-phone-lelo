using Microsoft.AspNetCore.Identity;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Zero.Configuration;
using PhoneLelo.Project.Authorization.Roles;
using PhoneLelo.Project.Authorization.Users;
using PhoneLelo.Project.MultiTenancy;
using Abp.Domain.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using PhoneLelo.Project.Chat;
using PhoneLelo.Project.Product.Dto;
using PhoneLelo.Project.Chat.Dto;

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
            long senderId,
            long receiverId)
        {
            var messages = (from chat in _chatMessageRepository.GetAll()
                            .Where(x=>
                                ((x.SenderId== senderId &&
                                x.ReceiverId== receiverId) ||
                                x.SenderId == receiverId &&
                                x.ReceiverId == senderId))

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
