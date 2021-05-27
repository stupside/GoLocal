namespace GoLocal.Core.Domain.ValueObjects
{
    public class Contact
    {
        public string Email { get; set; }
        public string Phone { get; set; }

        public Contact()
        {
        }

        public Contact(string email, string phone)
            : this()
        {
            Email = email;
            Phone = phone;
        }
    }
}