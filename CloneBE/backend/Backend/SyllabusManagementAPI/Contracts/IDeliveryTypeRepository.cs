using Entities.Models;

namespace SyllabusManagementAPI.Contracts
{
    public interface IDeliveryTypeRepository : IRepositoryBase<DeliveryType>
    {
        void CreateDeliveryTypeAsync(DeliveryType deliveryType);
        Task<List<DeliveryType?>> GetAllDeliveryTypeAsync();
        Task<DeliveryType?> GetDeliveryByIdAsync(string deliveryTypeId);
        Task<DeliveryType?> GetDeliveryByIdAsyncV2(string deliveryTypeId);
    }
}