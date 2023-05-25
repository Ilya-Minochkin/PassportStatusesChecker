using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public interface IPublicStatusesRepository
    {
        Task<PublicStatus> GetPublicStatus(int id);
        Task Save(PublicStatus publicStatus);
        Task Update(PublicStatus publicStatus);
    }
    public class PublicStatusesRepository : IPublicStatusesRepository
    {
        private readonly PgDbContext context;

        public PublicStatusesRepository(PgDbContext context)
        {
            this.context = context;
        }

        public async Task<PublicStatus> GetPublicStatus(int id)
        {
            return await context.PublicStatuses.FirstAsync(x => x.Id == id);
        }

        public async Task Save(PublicStatus publicStatus)
        {
            await context.PublicStatuses.AddAsync(publicStatus);
            await context.SaveChangesAsync();
        }

        public async Task Update(PublicStatus publicStatus)
        {
            context.PublicStatuses.Update(publicStatus);
            await context.SaveChangesAsync();
        }
    }
}
