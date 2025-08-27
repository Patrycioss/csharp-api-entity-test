using Microsoft.EntityFrameworkCore;
using workshop.wwwapi.Data;
using workshop.wwwapi.Models;

namespace workshop.wwwapi.Repository
{
    public class Repository : IRepository
    {
        private readonly DatabaseContext _databaseContext;
        public Repository(DatabaseContext db)
        {
            _databaseContext = db;
        }
        public async Task<IEnumerable<Patient>> GetPatients()
        {
            return await _databaseContext.Patients.ToListAsync();
        }

        public async Task<Patient?> GetPatient(int id)
        {
            return await _databaseContext.Patients.FindAsync(id);
        }

        public async Task CreatePatient(Patient patient)
        {
            await _databaseContext.Patients.AddAsync(patient);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Doctor>> GetDoctors()
        {
            return await _databaseContext.Doctors.ToListAsync();
        }
        public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctor(int id)
        {
            return await _databaseContext.Appointments.Where(a => a.DoctorId==id).ToListAsync();
        }

        public int CreatePatientId()
        {
            return _databaseContext.Patients.Max(patient => patient.Id) + 1;
        }
    }
}
