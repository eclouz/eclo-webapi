using Eclo.Persistence.Dtos.Notifications;

namespace Eclo.Services.Interfaces.Notifications;

public interface ISmsSender
{
    public Task<bool> SendAsync(SmsMessage smsMessage);
}