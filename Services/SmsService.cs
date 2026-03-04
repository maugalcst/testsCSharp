namespace DependencyInjection.Services
{
    public class SmsService : INotificationService
    {
        public void Send(string message)
        {
            Console.WriteLine($"Sending SMS: {message}");
        }
    }
}