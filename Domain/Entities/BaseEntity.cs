using System; 
using System.Text.Json.Serialization; 

namespace ComplyExchangeCMS.Domain
{
    public class BaseEntity
    {
        public BaseEntity()
        {
             CreatedDate = DateTime.UtcNow;
            IsDeleted = false;
            IsActive = true;
        }
      
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public DateTime? ModifiedDate { get; set; }
        [JsonIgnore]
        public Guid? CreatedBy { get; set; }
        [JsonIgnore]
        public Guid? ModifiedBy { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; }
        [JsonIgnore]
        public bool IsActive { get; set; }
         }
}
