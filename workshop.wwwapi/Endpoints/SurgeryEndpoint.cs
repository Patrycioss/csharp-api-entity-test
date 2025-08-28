using Microsoft.AspNetCore.Mvc;
using workshop.wwwapi.DTOs.Appointment;
using workshop.wwwapi.DTOs.Doctor;
using workshop.wwwapi.DTOs.Patient;
using workshop.wwwapi.Repository;

namespace workshop.wwwapi.Endpoints
{
    public static class SurgeryEndpoint
    {
        public static void ConfigurePatientEndpoint(this WebApplication app)
        {
            var surgeryGroup = app.MapGroup("surgery");

            surgeryGroup.MapGet("/patients", GetPatients);
            surgeryGroup.MapGet("/patients/{id:int}", GetPatient);
            surgeryGroup.MapPut("/patients/create", CreatePatient);
            
            surgeryGroup.MapGet("/doctors", GetDoctors);
            surgeryGroup.MapGet("/doctors/{id:int}", GetDoctor);
            surgeryGroup.MapPut("/doctors/create", CreateDoctor);
            
            surgeryGroup.MapGet("/appointmentsbydoctor/{id:int}", GetAppointmentsByDoctor);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetPatients(IRepository repository)
        {
            var patients = await repository.GetPatients();
            return TypedResults.Ok(patients.Select(PatientPost.FromPatient));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetPatient(IRepository repository, int id)
        {
            var patient = await repository.GetPatient(id);

            if (patient == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(PatientPost.FromPatient(patient));
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> CreatePatient(IRepository repository, PatientPut patientPut)
        {
            var patient = patientPut.ToPatient();
            patient.Id = repository.CreatePatientId();
            await repository.CreatePatient(patient);
            return TypedResults.Created("/patients/create", PatientPost.FromPatient(patient));
        }
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetDoctors(IRepository repository)
        {
            var doctors = (await repository.GetDoctors()).ToList();
            if (doctors.Count == 0)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(doctors.Select(DoctorPost.FromDoctor));
        }
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetDoctor(IRepository repository, int id)
        {
            var doctor = await repository.GetDoctor(id);
            if (doctor == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(DoctorPost.FromDoctor(doctor));
        }
        
        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> CreateDoctor(IRepository repository, DoctorPut doctorPut)
        {
            var doctor = await repository.CreateDoctor(doctorPut.ToDoctor());
            return TypedResults.Created("/doctors/create", DoctorPost.FromDoctor(doctor));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetAppointmentsByDoctor(IRepository repository, int id)
        {
            var appointments = (await repository.GetAppointmentsByDoctor(id)).ToList();

            if (appointments.Count == 0)
            {
                return TypedResults.NotFound();
            }
            
            return TypedResults.Ok(appointments.Select(AppointmentPost.FromAppointment));
        }
    }
}