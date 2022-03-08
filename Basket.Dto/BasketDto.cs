using System;

namespace Basket.Dto
{
    public class BasketDto
    {
        public Guid BasketId { get; set; }
        public DateTime CreateDate { get; set; }
        
        public string Message { get; set; }
        
        public Guid PaymentId { get; set; }
    }
}
