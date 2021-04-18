using System.Text.Json;

namespace EntityFrameworkWithJson.Entity
{
    public class Invoice
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public bool IsPayed { get; set; }
        public JsonElement WebhookData { get; set; }
    }
}
