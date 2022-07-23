namespace Entities.Models
{
    public class BaseEntity
    {
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public BaseEntity()
        {
            CreatedBy = string.Empty;
            CreatedDate = DateTime.UtcNow;
            ModifiedBy = string.Empty;
            ModifiedDate = DateTime.UtcNow;
        }

    }
}
