namespace Recipes.Web.Infrastructure
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Recipes.Web.ViewModels;

    public class GoogleCaptchaService
    {
        private readonly IOptionsMonitor<GoogleCaptchaConfig> config;

        public GoogleCaptchaService(IOptionsMonitor<GoogleCaptchaConfig> config)
        {
            this.config = config;
        }

        public async Task<bool> VerifyToken(string token)
        {
            try
            {
                var url = $"https://www.google.com/recaptcha/api/siteverify?secret={this.config.CurrentValue.SecretKey}&response={token}";

                using (var client = new HttpClient())
                {
                    var httpResult = await client.GetAsync(url);
                    if (httpResult.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        return false;
                    }

                    var responseString = await httpResult.Content.ReadAsStringAsync();

                    var googleResult = JsonConvert.DeserializeObject<GoogleCaptchaResponse>(responseString);

                    return googleResult.Success && googleResult.Score >= 0.5;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
