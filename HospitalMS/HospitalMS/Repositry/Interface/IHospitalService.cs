using System;
using HospitalMS.Models;

namespace HospitalMS.Repositry.Interface
{
    public interface IHospitalService
    {
        Appointment GetAppointmentById(int appointmentId);

        List<Appointment> GetAppointmentsForPatient(int patientId);

        List<Appointment> GetAppointmentsForDoctor(int doctorId);

        bool ScheduleAppointment(Appointment appointment);

        bool UpdateAppointment(Appointment appointment);

        bool CancelAppointment(int appointmentId);
        List<Doctor> GetAllDoctors();
    }
}

