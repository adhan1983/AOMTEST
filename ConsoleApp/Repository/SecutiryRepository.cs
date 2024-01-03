using AOMTEST.Entity;
using AOMTEST.Exceptions;
using AOMTEST.Repository.Interfaces;

namespace AOMTEST.Repository
{
    public class SecutiryRepository : ISecurityRepository
    {
        private readonly SecutiryDataDbContext _context;
        public SecutiryRepository(SecutiryDataDbContext context)
        {
           _context = context;
        }
        public async Task<bool> InsertSecutiryPriceAsync(List<PriceData> lstPriceData)
        {
            try
            {
                await _context.PriceData.AddRangeAsync(lstPriceData);

                await _context.SaveChangesAsync();                
            }
            catch (DatabaseException ex)
            {
                throw ex;
            }

            return true;
        }
    }
}
