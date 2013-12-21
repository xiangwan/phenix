using System;

namespace Phenix.Core.Domain
{
    public class UserValidation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Guid { get; set; }
        public int Type { get; set; }
        public DateTime CreateOn { get; set; }

        public virtual User User { get; set; } 
    }
}