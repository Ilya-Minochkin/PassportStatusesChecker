using CheckerService.Mapper;
using CheckerService.Logger.Abstractions;
using CheckerService.Models;
using Database.Repositories;
using Chat = CheckerService.Models.Chat;

namespace CheckerService.Services
{
    public interface IChatService
    {
        Task Add(Chat chat);
        Task<Chat> Get(int id);
        Task<List<Chat>> GetAll();
    }
    public class ChatService : IChatService
    {
        private readonly IChatsRepository chatsRepository;
        private readonly ILogger log;       

        public ChatService(IChatsRepository chatsRepository, ILogger log)
        {
            this.chatsRepository = chatsRepository;
            this.log = log; 
        }

        public async Task Add(Chat chat)
        {
            try
            {
                var dbChat = SimpleObjectMapper.GetMappedObject<Chat, Database.Entities.Chat>(chat);
                await chatsRepository.Save(dbChat);
                log.Information($"New chat saved, chatId={chat.ChatId}, internalId={dbChat.Id}");
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
        }

        public async Task<Chat> Get(int id)
        {
            var dbChat = await chatsRepository.GetChat(id);
            var chat = SimpleObjectMapper.GetMappedObject<Database.Entities.Chat,  Chat>(dbChat);
            log.Information($"Get chat with chatId={dbChat.ChatId} and internal id={id}");

            return chat;
        }

        public async Task<List<Chat>> GetAll()
        {
            var dbChats = await chatsRepository.GetAll();
            return dbChats.Where(x => x.ChatId > 0)
                .Select(SimpleObjectMapper.GetMappedObject<Database.Entities.Chat, Chat>)
                .ToList();
        }
    }
}
