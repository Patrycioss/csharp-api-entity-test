using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace workshop.wwwapi.Models
{
    [Table("patients")]
    public class Patient
    {   
        [Column("id", TypeName = "int")]
        public int Id { get; set; }
        
        [Column("name", TypeName = "varchar(100)")]
        public string FullName { get; set; }

        public Patient(string fullName)
        {
            FullName = fullName;
        }
    }
}
