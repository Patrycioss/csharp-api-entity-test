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
            return await _databaseContext.Patients
                .Include(patient => patient.Appointments)
                .ThenInclude(appointment => appointment.Doctor)
                .ToListAsync();
        }

        public async Task<Patient?> GetPatient(int id)
        {
            return await _databaseContext.Patients
                .Include(patient => patient.Appointments)
                .ThenInclude(appointment => appointment.Doctor)
                .FirstAsync(patient => patient.Id == id);
        }

        public async Task<Patient> CreatePatient(Patient patient)
        {
            await _databaseContext.Patients.AddAsync(patient);
            await _databaseContext.SaveChangesAsync();
            return patient;
        }

        public async Task<IEnumerable<Doctor>> GetDoctors()
        {
            return await _databaseContext.Doctors
                .Include(doctor => doctor.Appointments)
                .ThenInclude(appointment => appointment.Patient)
                .ToListAsync();
        }

        public async Task<Doctor?> GetDoctor(int id)
        {
            return await _databaseContext.Doctors
                .Include(doctor => doctor.Appointments)
                .ThenInclude(appointment => appointment.Patient)
                .FirstAsync(doctor => doctor.Id == id);
        }

        public async Task<Doctor> CreateDoctor(Doctor doctor)
        {
            await _databaseContext.Doctors.AddAsync(doctor);
            await _databaseContext.SaveChangesAsync();
            return doctor;
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctor(int id)
        {
            return await _databaseContext.Appointments
                .Where(a => a.DoctorId == id)
                .Include(appointment => appointment.Patient)
                .Include(appointment => appointment.Doctor)
                .ToListAsync();
        }

        public int CreatePatientId()
        {
            return _databaseContext.Patients.Max(patient => patient.Id) + 1;
        }
    }
}