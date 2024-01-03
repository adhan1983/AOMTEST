using AOMTEST.HttpRequest.Interfaces;
using AOMTEST.Response;
using System.Text.Json;

namespace AOMTEST.HttpRequest
{
    public class SecurityPriceHttp : ISecurityPriceHttp
    {
        public async Task<SecutiryResponse> GetSecurityResponse(string isin)
        {
            HttpClient client = new HttpClient();
            var response = new SecutiryResponse();
            HttpResponseMessage result = await client.GetAsync($"https://securities.dataprovider.com/securityprice/{isin}");

            if (result.IsSuccessStatusCode)
            {
                var strResult = await result.Content.ReadAsStringAsync();
                response = JsonSerializer.Deserialize<SecutiryResponse>(result.Content.ReadAsStream());            
            }

            return response;

        }
    }
}
