using System;
namespace NoviAMS.Domain.Models
{
    public class MemberDetail
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? CustomerType { get; set; }
        public bool IsActive { get; set; }
        public Address? BillingAddress { get; set; }
        public Address? ShippingAddress { get; set; }
    }
}

