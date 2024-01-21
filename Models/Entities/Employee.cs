using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using employee_crud.Models.Validations;

namespace employee_crud.Models.Entities
{
    public class Employee
    {
        [Key, Column("id")]
        [DisplayName("Employee ID")]
        public int Id { get; set; }

        [Column("first_name"), MaxLength(20)]
        [DisplayName("First Name")]
        public required string FirstName { get; set; }

        [Column("last_name"), MaxLength(30)]
        [DisplayName("Last Name")]
        public required string LastName { get; set; }

        [Column("email"), MaxLength(50)]
        [DisplayName("Email Address")]
        [EmailAddress]
        public required string Email { get; set; }

        [Column("dob")]
        [DisplayName("Date of Birth")]
        public required DateOnly Dob { get; set; }

        [Column("salary")]
        [DisplayName("Salary")]
        [GreaterThan(0)]
        public required double Salary { get; set; }

        [Column("department_id")]
        [DisplayName("Department ID")]
        public required int DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        [DisplayName("Department")]
        public required Employee Department { get; set; }
    }
}
