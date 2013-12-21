using System;

namespace Phenix.Core.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string EMail { get; set; }
        public bool EMailIsValid { get; set; }
        public string PassWord { get; set; } 
        public bool IsBanned { get; set; }
        public string BandnedReason { get; set; }
        public string LastIp { get; set; }
        public DateTime LastLoginOn { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime EMailVerifyOn { get; set; } 
    }
}