using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HospitalMS.Models;
using HospitalMS.Repositry.Interface;
using HospitalMS.Utilities;

namespace HospitalMS.Repositry
{
    public class HospitalServiceImpl : IHospitalService
    {
        private readonly string connectionString;

        public HospitalServiceImpl()
        {
            connectionString = DbConnUtil.GetConnString(); 
        }

        
        public Appointment GetAppointmentById(int appointmentId)
        {
            Appointment appointment = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM appointment WHERE appointmentid = @appointmentId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@appointmentId", appointmentId);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        appointment = new Appointment
                        {
                            AppointmentId = (int)reader["appointmentid"],
                            PatientId = (int)reader["patientid"],
                            DoctorId = (int)reader["doctorid"],
                            AppointmentDate = (DateTime)reader["appointmentdate"],
                            Description = reader["description"].ToString()
                        };
                    }
                }
            }
            return appointment;
        }

        public List<Doctor> GetAllDoctors()
          {
           List<Doctor> doctors = new List<Doctor>();
           using (SqlConnection connection = new SqlConnection(connectionString))
           {
             string query = "SELECT * FROM doctor";
             SqlCommand command = new SqlCommand(query, connection);
             connection.Open();
             using (SqlDataReader reader = command.ExecuteReader())
              {
               while (reader.Read())
                {
                  Doctor doctor = new Doctor
                 {
                    DoctorId = (int)reader["doctorid"],
                    FirstName = reader["firstname"].ToString(),
                    LastName = reader["lastname"].ToString(),
                    Specialization = reader["specialization"].ToString(),
                    ContactNumber = reader["contactnumber"].ToString()
                };
                     doctors.Add(doctor);
                      }
                   }
                 }
               return doctors;
            }

        
        public List<Appointment> GetAppointmentsForPatient(int patientId)
        {
            List<Appointment> appointments = new List<Appointment>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM appointment WHERE patientid = @patientId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@patientId", patientId);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Appointment appointment = new Appointment
                        {
                            AppointmentId = (int)reader["appointmentid"],
                            PatientId = (int)reader["patientid"],
                            DoctorId = (int)reader["doctorid"],
                            AppointmentDate = (DateTime)reader["appointmentdate"],
                            Description = reader["description"].ToString()
                        };
                        appointments.Add(appointment);
                    }
                }
            }
            return appointments;
        }

        
        public List<Appointment> GetAppointmentsForDoctor(int doctorId)
        {
            List<Appointment> appointments = new List<Appointment>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM appointment WHERE doctorid = @doctorId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@doctorId", doctorId);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Appointment appointment = new Appointment
                        {
                            AppointmentId = (int)reader["appointmentid"],
                            PatientId = (int)reader["patientid"],
                            DoctorId = (int)reader["doctorid"],
                            AppointmentDate = (DateTime)reader["appointmentdate"],
                            Description = reader["description"].ToString()
                        };
                        appointments.Add(appointment);
                    }
                }
            }
            return appointments;
        }

        
        public bool ScheduleAppointment(Appointment appointment)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO appointment (patientid, doctorid, appointmentdate, description) VALUES (@patientId, @doctorId, @appointmentDate, @description)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@patientId", appointment.PatientId);
                command.Parameters.AddWithValue("@doctorId", appointment.DoctorId);
                command.Parameters.AddWithValue("@appointmentDate", appointment.AppointmentDate);
                command.Parameters.AddWithValue("@description", appointment.Description);

                connection.Open();
                int result = command.ExecuteNonQuery();
                return result > 0; 
            }
        }
        public List<Appointment> GetAllAppointments()
        {
            List<Appointment> appointments = new List<Appointment>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM appointment"; 
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Appointment appointment = new Appointment
                            {
                                AppointmentId = (int)reader["appointmentid"],
                                PatientId = (int)reader["patientid"],
                                DoctorId = (int)reader["doctorid"],
                                AppointmentDate = (DateTime)reader["appointmentdate"],
                                Description = reader["description"].ToString()
                            };
                            appointments.Add(appointment);
                        }
                    }
                }
            }
            return appointments;
        }
    
        public bool UpdateAppointment(Appointment appointment)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE appointment SET patientid = @patientId, doctorid = @doctorId, appointmentdate = @appointmentDate, description = @description WHERE appointmentid = @appointmentId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@appointmentId", appointment.AppointmentId);
                command.Parameters.AddWithValue("@patientId", appointment.PatientId);
                command.Parameters.AddWithValue("@doctorId", appointment.DoctorId);
                command.Parameters.AddWithValue("@appointmentDate", appointment.AppointmentDate);
                command.Parameters.AddWithValue("@description", appointment.Description);

                connection.Open();
                int result = command.ExecuteNonQuery();
                return result > 0; 
            }
        }
        

        
        public bool CancelAppointment(int appointmentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM appointment WHERE appointmentid = @appointmentId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@appointmentId", appointmentId);

                connection.Open();
                int result = command.ExecuteNonQuery();
                return result > 0; 
            }
        }
    }
}
