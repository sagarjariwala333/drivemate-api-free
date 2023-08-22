using System.ComponentModel.DataAnnotations;

namespace DriveMate.Entities
{
    public class BaseClass : IdEntity
    {
        public BaseClass()
        {
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
        }

        public Guid CreatedBy { get ; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Boolean IsDeleted { get; set; }
    }

    public class IdEntity
    {
        [Key]
        public Guid? Id { get; set; }
    }
}
