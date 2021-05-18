using System.Collections.Generic;

namespace GoLocal.Shared.Mailing.Commons.Models
{
    public class EmailMessage
    {
        public string UserName { get; init; }
        public HashSet<string> Recipients { get; init; }
        public string Object { get; init; }
        public string Content { get; init; }

        public EmailMessage(HashSet<string> recipients, string @object, string content)
        {
            Recipients = recipients;
            Object = @object;
            Content = content;
        }

        public EmailMessage(string recipient, string @object, string content)
            : this(new HashSet<string>{recipient},@object, content)
        {
        }
    }
}