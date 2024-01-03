using AOMTEST.Entity;
using AOMTEST.Exceptions;
using AOMTEST.HttpRequest.Interfaces;
using AOMTEST.Logger.Interfaces;
using AOMTEST.Repository.Interfaces;
using AOMTEST.Services.Interfaces;
using AOMTEST.Validator;
using FluentValidation;

namespace AOMTEST.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly ISecurityPriceHttp _securityPriceHttp;
        private readonly ISecurityRepository _secutiryRepository;
        private readonly ILoggerSecurityService _loggerSecurityService;
        private readonly SecurityPriceValidator _secutiryPriceValidator;

        public SecurityService(ISecurityPriceHttp securityPriceHttp, ISecurityRepository secutiryRepository, ILoggerSecurityService loggerSecurityService, SecurityPriceValidator secutiryPriceValidator)
        {
            this._securityPriceHttp = securityPriceHttp;
            this._secutiryRepository = secutiryRepository;
            this._loggerSecurityService = loggerSecurityService;
            this._secutiryPriceValidator = secutiryPriceValidator;
        }

        public async Task InsertListSecutiryService(List<string> lstIsin)
        {
            List<PriceData> lstPriceData = new List<PriceData>();
            
            try
            {
                foreach (var item in lstIsin)
                {
                    var result = await _securityPriceHttp.GetSecurityResponse(item);

                    if (result != null)
                    {
                        var priceData = new PriceData()
                        {
                            ISIN = item,
                            Price = result.Price
                        };

                        var validateResult = _secutiryPriceValidator.Validate(priceData);

                        if (!validateResult.IsValid)
                        {
                            throw new ValidationException(validateResult.Errors);
                        }
                        
                        lstPriceData.Add(priceData);                        
                        await _loggerSecurityService.LogAsync("Success");

                    }
                    else
                    {
                        await _loggerSecurityService.LogAsync("No data");
                    }
                }

                await _secutiryRepository.InsertSecutiryPriceAsync(lstPriceData);
            }
            catch (DatabaseException databaseException)
            {
                await _loggerSecurityService.LogAsync("Database failure");
                
                throw databaseException;
            }
            catch (Exception ex)
            {
                throw ex;
            }           

        }
    }
}
