using AOMTEST.Response;

namespace AOMTEST.HttpRequest.Interfaces
{
    public interface ISecurityPriceHttp
    {
        Task<SecutiryResponse> GetSecurityResponse(string isin);
    }
}
