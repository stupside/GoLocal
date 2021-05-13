namespace GoLocal.Shared.Mailing.Commons.Models
{
    public class EmailMessage
    {
        public string UserName { get; init; }

        public string Recipient { get; init; }
        public string Object { get; init; }
        public string Content { get; init; }

        public EmailMessage(string recipient, string @object, string content)
        {
            Recipient = recipient;
            Object = @object;
            Content = content;
        }
        
    }
}