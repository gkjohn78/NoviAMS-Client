using System;
namespace NoviAMS.Domain.Models
{
    public class Member
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? CustomerType { get; set; }
        public bool IsActive { get; set; }
    }
}

