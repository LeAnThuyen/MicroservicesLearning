using Contracts.Services;
using Shared.Services.Email;

namespace Ordering.Infrastructure.Services;

public interface ISmtpEmailService:IEmailServices<MailRequest>
{
    
}