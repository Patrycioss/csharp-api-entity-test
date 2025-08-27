using System.ComponentModel.DataAnnotations.Schema;

namespace workshop.wwwapi.Models
{
    [Table("appointments")]
    public class Appointment
    {
        [Column("id", TypeName = "int")]
        public int Id { get; set; }

        [Column("booking", TypeName = "date")]
        public DateTime Booking { get; set; }

        [Column("doctor_id_fk", TypeName = "serial")]
        public int DoctorId { get; set; }

        [Column("patient_id_fk", TypeName = "serial")]
        public int PatientId { get; set; }

        public Appointment(DateTime booking, int doctorId, int patientId)
        {
            Booking = booking;
            DoctorId = doctorId;
            PatientId = patientId;
        }
    }
}