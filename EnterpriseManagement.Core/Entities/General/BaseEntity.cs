using System.ComponentModel.DataAnnotations;

namespace EnterpriseManagement.Core.Entities.General
{
    public class BaseEntity<T>
    {
        [Key]
        public int Id { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
