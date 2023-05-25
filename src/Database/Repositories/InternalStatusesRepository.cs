using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public interface IInternalStatusesRepository
    {
        Task<InternalStatus> GetInternalStatus(int id);
        Task Save(InternalStatus internalStatus);
        Task Update(InternalStatus internalStatus);
    }
    public class InternalStatusesRepository : IInternalStatusesRepository
    {
        private readonly PgDbContext context;

        public InternalStatusesRepository(PgDbContext context)
        {
            this.context = context;
        }

        public async Task<InternalStatus> GetInternalStatus(int id)
        {
            return await context.InternalStatuses.FirstAsync(x => x.Id == id);
        }

        public async Task Save(InternalStatus internalStatus)
        {
            await context.InternalStatuses.AddAsync(internalStatus);
            await context.SaveChangesAsync();
        }

        public async Task Update(InternalStatus internalStatus)
        {
            context.InternalStatuses.Update(internalStatus);
            await context.SaveChangesAsync();
        }
    }
}
