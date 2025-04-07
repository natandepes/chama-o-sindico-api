using System.ComponentModel.DataAnnotations;

namespace ChamaOSindico.Domain.Entities
{
    public class BlacklistedToken
    {
        public string? Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
    }
}
