using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public interface IChatsRepository
    {
        Task<Chat> GetChat(int id);
        Task Save(Chat chat);
    }
    public class ChatsRepository : IChatsRepository
    {
        private readonly PgDbContext context;

        public ChatsRepository(PgDbContext context)
        {
            this.context = context;
        }

        public async Task<Chat> GetChat(int id)
        {
            return await context.Chats.FirstAsync(chat => chat.Id == id);
        }

        public async Task Save(Chat chat)
        {
            await context.Chats.AddAsync(chat);
            await context.SaveChangesAsync();
        }
    }
}
