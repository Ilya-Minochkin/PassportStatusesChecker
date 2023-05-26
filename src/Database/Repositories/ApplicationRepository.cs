using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories
{
    public interface IApplicationRepository
    {
        Task<Application> Get(int id);
        Task<List<Application>> GetByChatId(int chatId);
        Task Save(Application application);
        Task Update(Application application);
    }
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly PgDbContext context;

        public ApplicationRepository(PgDbContext context)
        {
            this.context = context;
        }

        public async Task<Application> Get(int id)
        {
            return await context.Applications.FirstAsync(x => x.Id == id);
        }

        public async Task<List<Application>> GetByChatId(int chatId)
        {
            return await context.Applications
                .Where(x => x.Chat.ChatId == chatId)
                .Select(x => x)
                .ToListAsync();
        }

        public async Task Save(Application application)
        {
            await context.Applications.AddAsync(application);
            await context.SaveChangesAsync();
        }

        public async Task Update(Application application)
        {
            context.Update(application);
            await context.SaveChangesAsync();
        }
    }
}
