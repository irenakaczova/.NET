
namespace Instagram
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime Date { get; set; }

        public int SenderId { get; set; }
        public virtual User Sender { get; set; } = null!;

        public int RecipientId { get; set; }
        public virtual User Recipient { get; set; } = null!;
    }
}