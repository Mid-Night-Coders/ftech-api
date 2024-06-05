using FTech.Domain.Entities.Auth;
using FTech.Domain.Entities.Chats;
using FTech.Domain.Entities.Common;

namespace FTech.Domain.Entities.Drivers
{
    public class Driver : Auditable<long>
    {
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? LicenseNumber { get; set; }
        public DateOnly? LicenseIssueDate { get; set; }
        public string? LicenseFrontImagePath { get; set; }
        public string? LicenseBackImagePath { get; set; }
        public string? DriverAvatarPath { get; set; }

        public long UserId { get; set; }
        public User? User { get; set; }
    }


}
