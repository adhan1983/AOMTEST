using AOMTEST.Entity;

namespace AOMTEST.Repository.Interfaces
{
    public interface ISecurityRepository
    {
        public Task<bool> InsertSecutiryPriceAsync(List<PriceData> lstPriceData);               
       
    }
}
