using System;
using ConsoleTables;
using HospitalMS.Exceptions;
using HospitalMS.Models;
using HospitalMS.Repositry;

namespace HospitalMS.MainMenu
{
    public class MainMenu
    {
        private HospitalServiceImpl hospitalService;

        public MainMenu()
        {
            hospitalService = new HospitalServiceImpl();
        }

        public void DisplayMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n=== Hospital Management System ===");
                Console.ResetColor();
                Console.WriteLine("1. Patient Menu");
                Console.WriteLine("2. Doctor Menu");
                Console.WriteLine("3. Exit");
                Console.Write("Please select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayPatientMenu();
                        break;
                    case "2":
                        DisplayDoctorMenu();
                        break;
                    case "3":
                        exit = true;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Exiting the program. Goodbye!");
                        Console.ResetColor();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.ResetColor();
                        break;
                }
            }
        }

        private void DisplayPatientMenu()
        {
            bool exit = false;

            while (!exit)
            {
             
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n=== Patient Menu ===");
                Console.ResetColor();
                Console.WriteLine("1. Schedule Appointment");
                Console.WriteLine("2. Get Appointment By ID");
                Console.WriteLine("3. Get Appointments");
                Console.WriteLine("4. Cancel Appointment");
                Console.WriteLine("5. Go Back");
                Console.Write("Please select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ScheduleAppointment();
                        break;
                    case "2":
                        GetAppointmentById();
                        break;
                    case "3":
                        GetAppointmentsForPatient();
                        break;
                    case "4":
                        CancelAppointment();
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.ResetColor();
                        break;
                }
            }
        }

        private void DisplayDoctorMenu()
        {
            bool exit = false;

            while (!exit)
            {
          
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n=== Doctor Menu ===");
                Console.ResetColor();
                Console.WriteLine("1. Get Appointments For Doctor");
                Console.WriteLine("2. Update Appointment");
                Console.WriteLine("3. Go Back");
                Console.Write("Please select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        GetAppointmentsForDoctor();
                        break;
                    case "2":
                        UpdateAppointment();
                        break;
                    case "3":
                        exit = true;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.ResetColor();
                        break;
                }
            }
        }

        private void ScheduleAppointment()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.White;

               
                DisplayDoctors();

                var appointmentToSchedule = new Appointment
                {
                    PatientId = GetIntegerInput("Enter Patient ID: "),
                    DoctorId = GetIntegerInput("Enter Doctor ID: "), 
                    AppointmentDate = GetDateInput("Enter Appointment Date (yyyy-mm-dd): "), 
                    Description = GetStringInput("Enter appointment description: ")
                };

                bool isScheduled = hospitalService.ScheduleAppointment(appointmentToSchedule);
                Console.ForegroundColor = isScheduled ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine(isScheduled ? "Appointment scheduled successfully." : "Failed to schedule appointment.");
                Console.ResetColor();
            }
            catch (PatientNumberNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Patient with this ID does not exist.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
        }

        private void DisplayDoctors()
        {
            var doctors = hospitalService.GetAllDoctors(); 
            var table = new ConsoleTable("Doctor ID", "Doctor Name", "Speciality", "Contact Number");

            foreach (var doctor in doctors)
            {
                table.AddRow(doctor.DoctorId, doctor.FirstName + " " + doctor.LastName,doctor.Specialization,doctor.ContactNumber);
            }

            table.Write(); 
            Console.WriteLine();
        }


        private void GetAppointmentById()
        {
            try
            {
                int appointmentId = GetIntegerInput("Enter Appointment ID: ");
                var appointment = hospitalService.GetAppointmentById(appointmentId);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Appointment found: {appointment.Description},\nDoctor Id: {appointment.DoctorId}");
                Console.ResetColor();
            }
            
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                Console.ResetColor();
            }
        }

        private void GetAppointmentsForPatient()
        {

            try
            {
                int patientId = GetIntegerInput("Enter Patient ID: ");
                var patientAppointments = hospitalService.GetAppointmentsForPatient(patientId);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Appointments for Patient:");
                foreach (var appt in patientAppointments)
                {
                    Console.WriteLine(appt.Description);
                }
                Console.ResetColor();
            }
            catch (PatientNumberNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Patient with this ID does not exist.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
        }

        private void GetAppointmentsForDoctor()
        {
            try
            {
                int doctorId = GetIntegerInput("Enter Doctor ID: ");
                var doctorAppointments = hospitalService.GetAppointmentsForDoctor(doctorId);

               
                var table = new ConsoleTable("Appointment ID", "Patient ID", "Description", "Date");

                foreach (var appt in doctorAppointments)
                {
                    table.AddRow(appt.AppointmentId, appt.PatientId, appt.Description, appt.AppointmentDate.ToShortDateString());
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Appointments for Doctor ID {doctorId}:");
                table.Write(); 
                Console.WriteLine();
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
        }

        private void UpdateAppointment()
        {
            try
            {
               
                var allAppointments = hospitalService.GetAllAppointments();

          
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("All Appointments:");
                var table = new ConsoleTable("Appointment ID", "Patient ID", "Doctor ID", "Description", "Date");
                foreach (var appointmentg in allAppointments)
                {
                    table.AddRow(appointmentg.AppointmentId, appointmentg.PatientId, appointmentg.DoctorId, appointmentg.Description, appointmentg.AppointmentDate.ToShortDateString());
                }
                table.Write();
                Console.WriteLine();
                Console.ResetColor();

               
                int appointmentId = GetIntegerInput("Enter Appointment ID to update: ");
                var appointment = hospitalService.GetAppointmentById(appointmentId);
                appointment.Description = GetStringInput("Enter new description: ");

                appointment.AppointmentDate = GetDateInput("Enter new appointment date (yyyy-mm-dd): ");

              
                bool isUpdated = hospitalService.UpdateAppointment(appointment);

              
                Console.ForegroundColor = isUpdated ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine(isUpdated ? "Appointment updated successfully." : "Failed to update appointment.");
                Console.ResetColor();
            }
            catch (PatientNumberNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                Console.ResetColor();
            }
        }

        private void CancelAppointment()
        {
            try
            {
                var allAppointments = hospitalService.GetAllAppointments();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("All Appointments:");
                var table = new ConsoleTable("Appointment ID", "Patient ID", "Doctor ID", "Description", "Date");
                foreach (var appointmentg in allAppointments)
                {
                    table.AddRow(appointmentg.AppointmentId, appointmentg.PatientId, appointmentg.DoctorId, appointmentg.Description, appointmentg.AppointmentDate.ToShortDateString());
                }
                table.Write(); 
                Console.WriteLine();
                Console.ResetColor();
                int appointmentId = GetIntegerInput("Enter Appointment ID to cancel: ");
                bool isCancelled = hospitalService.CancelAppointment(appointmentId);
                Console.ForegroundColor = isCancelled ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine(isCancelled ? "Appointment cancelled successfully." : "Failed to cancel appointment.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
        }

        private int GetIntegerInput(string message)
        {
            Console.Write(message);
            return int.Parse(Console.ReadLine());
        }

        private string GetStringInput(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        private DateTime GetDateInput(string message)
        {
            DateTime appointmentDate;
            while (true)
            {
                Console.Write(message);
                if (DateTime.TryParse(Console.ReadLine(), out appointmentDate))
                {
                    return appointmentDate;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid date format. Please try again.");
                    Console.ResetColor();
                }
            }
        }
    }
}
