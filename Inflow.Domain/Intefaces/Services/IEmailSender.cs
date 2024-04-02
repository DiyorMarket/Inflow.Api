namespace Inflow.Domain.Intefaces.Services
{
    public interface IEmailSender
    {
        Task SendEmail(string email, string subject, string massege);
    }
}
