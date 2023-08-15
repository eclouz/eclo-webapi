using Eclo.Persistence.Dtos.Notifications;
using Eclo.Services.Interfaces.Notifications;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;

namespace Eclo.Services.Services.Notifications;

public class SmsSender : ISmsSender
{
    private readonly string BASE_URL = "";
    private readonly string SENDER = "";
    private readonly string EMAIL = "";
    private readonly string PASSWORD = "";
    private string TOKEN = "";

    public SmsSender(IConfiguration config)
    {
        BASE_URL = config["Sms:BaseURL"]!;
        SENDER = config["Sms:Sender"]!;
        EMAIL = config["Sms:Email"]!;
        PASSWORD = config["Sms:Password"]!;
    }

    private async Task LoginAsync()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri(BASE_URL);
        var request = new HttpRequestMessage(HttpMethod.Post, "api/auth/login");

        var content = new MultipartFormDataContent();
        content.Add(new StringContent(EMAIL), "email");
        content.Add(new StringContent(PASSWORD), "password");
        request.Content = content;
        var response = await client.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            EskizLoginDto dto = JsonConvert.DeserializeObject<EskizLoginDto>(json)!;
            TOKEN = dto.Data.Token;
        }
    }

    public async Task<bool> SendAsync(SmsMessage smsMessage)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri(BASE_URL);
        var request = new HttpRequestMessage(HttpMethod.Post, "api/message/sms/send");
        request.Headers.Add("Authorization", $"Bearer {TOKEN}");

        var content = new MultipartFormDataContent();
        content.Add(new StringContent(smsMessage.Recipent), "mobile_phone");
        content.Add(new StringContent(smsMessage.Title + " " + smsMessage.Content), "message");
        content.Add(new StringContent(SENDER), "from");
        content.Add(new StringContent("http://0000.uz/test.php"), "callback_url");
        request.Content = content;
        var response = await client.SendAsync(request);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            await LoginAsync();
            return await SendAsync(smsMessage);
        }
        else if (response.IsSuccessStatusCode) return true;
        else return false;
    }

    public class EskizLoginDto
    {
        public string Message { get; set; } = String.Empty;

        public EskizToken Data { get; set; }

        public EskizLoginDto()
        {
            Data = new EskizToken();
        }
    }

    public class EskizToken
    {
        public string Token { get; set; } = String.Empty;
    }
}
