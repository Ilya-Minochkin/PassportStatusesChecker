using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public interface IReadinessResponsesRepository
    {
        Task<ReadinessResponse> GetReadinessResponse(int id);
        Task Save (ReadinessResponse response);
        Task Update (ReadinessResponse response);
    }
    public class ReadinessResponsesRepository : IReadinessResponsesRepository
    {
        private readonly PgDbContext context;

        public ReadinessResponsesRepository(PgDbContext context)
        {
            this.context = context;
        }

        public async Task<ReadinessResponse> GetReadinessResponse(int id)
        {
            return await context.ReadinessResponses.FirstAsync(x => x.Id == id);
        }

        public async Task Save(ReadinessResponse response)
        {
            await context.ReadinessResponses.AddAsync(response);
            await context.SaveChangesAsync();
        }

        public async Task Update(ReadinessResponse response)
        {
            context.ReadinessResponses.Update(response);
            await context.SaveChangesAsync();
        }
    }
}
