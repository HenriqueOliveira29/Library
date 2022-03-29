using Supermarket.Data.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Data.Interfaces.Services
{
    public interface IEmailService
    {
        Task<MessageHelper> SendEmail(string emailTo, string templateName, object obj, string subject, string emailCC = null, string emailBCC = null, List<EmailFileDTO> files = null);
    }
}
