using Entities.Context;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Nest;
using SyllabusManagementAPI.Contracts;


namespace SyllabusManagementAPI.Repository
{
    public class DeliveryTypeRepository : RepositoryBase<DeliveryType>, IDeliveryTypeRepository
    {
        private readonly FamsContext _context;

        public DeliveryTypeRepository(FamsContext context) : base(context)
        {
            _context = context;
        }

        public void CreateDeliveryTypeAsync(DeliveryType deliveryType)
        {
            Create(deliveryType);
        }

        public async Task<List<DeliveryType?>> GetAllDeliveryTypeAsync()
        {
            return await FindAll()
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<DeliveryType?> GetDeliveryByIdAsync(string deliveryTypeId)
        {
            return await FindByCondition(x => x.DeliveryTypeId == deliveryTypeId).FirstOrDefaultAsync();
        }
        public async Task<DeliveryType?> GetDeliveryByIdAsyncV2(string deliveryTypeId)
        {
            return await FindByConditionV2(x => x.DeliveryTypeId == deliveryTypeId).FirstOrDefaultAsync();
        }
    }
}