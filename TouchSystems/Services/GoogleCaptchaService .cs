using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using TouchSystems.ViewModel;

namespace TouchSystems.Services
{
    public class GoogleCaptchaService : IGoogleCaptchaService
    {
        private readonly GoogleReCaptchaSettings _googleRECaptchaSettings;
        private readonly IHttpContextAccessor _httpContext;

        public GoogleCaptchaService(IOptions<GoogleReCaptchaSettings> googleCaptchaSettings, IHttpContextAccessor httpContext)
        {
            _googleRECaptchaSettings = googleCaptchaSettings.Value;
            _httpContext = httpContext;
        }

        public async Task<bool> VerifyToken(string token, double threshold)
        {
            try
            {
                // Post to: https://www.google.com/recaptcha/api/siteverify
                // Parameters: secret, response, remoteip

                var remoteIp = _httpContext.HttpContext.Connection.RemoteIpAddress;
                var url = $"https://www.google.com/recaptcha/api/siteverify?secret={_googleRECaptchaSettings.SiteKey}&response={token}&remoteip={remoteIp}";

                using (var client = new HttpClient())
                {
                    var httpResult = await client.GetAsync(url);
                    if (!httpResult.IsSuccessStatusCode)
                        return false;

                    var responseString = await httpResult.Content.ReadAsStringAsync();
                    var googleResult = JsonConvert.DeserializeObject<GoogleReCaptchaSettings>(responseString);

                    return googleResult.Success && googleResult.Score >= threshold;
                }
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
