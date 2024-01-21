using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace employee_crud.Models.Entities
{
    public class Department
    {
        [Key]
        [Column("id")]
        [DisplayName("Department ID")]
        public int Id { get; set; }

        [Column("name"), MaxLength(20)]
        [DisplayName("Department Name")]
        public required string Name { get; set; }
    }
}
