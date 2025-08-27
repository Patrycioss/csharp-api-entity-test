using workshop.wwwapi.Models;

namespace workshop.wwwapi.Data;

public class Seeder
{
    public List<Doctor> Doctors { get; init; } = [];
    public List<Patient> Patients { get; init; } = [];
    public List<Appointment> Appointments { get; init; } = [];

    public Seeder()
    {
        Doctors.AddRange([
            new Doctor("John Doe"){Id = 1},
            new Doctor("Dr. Mario"){Id = 2},
            new Doctor("Karel Bibber"){Id = 3},
        ]);

        Patients.AddRange([
            new Patient("Henk Patient"){Id = 1},
            new Patient("Kees Kanon"){Id = 2},
            new Patient("Michael Mank"){Id = 3},
        ]);

        Appointments.AddRange([
            new Appointment(new DateTime(100, 5, 3, 15, 15, 0), 1,3){Id = 1},
            new Appointment(new DateTime(2025, 5, 5, 9, 15, 0), 3,3){Id = 2},
            new Appointment(new DateTime(2000, 5, 2, 16, 1, 0), 1,1){Id = 3},
            new Appointment(new DateTime(2025, 3, 20, 16, 15, 0), 3,1){Id = 4},
            new Appointment(new DateTime(2025, 5, 2, 16, 15, 0), 2,2){Id = 5},
            new Appointment(new DateTime(2025, 4, 3, 13, 0, 0), 1,2){Id = 6},
            new Appointment(new DateTime(2018, 6, 1, 9, 0, 0), 3,1){Id = 7},
            new Appointment(new DateTime(2025, 4, 3, 13, 0, 0), 2,3){Id = 8},
        ]);
    }
}