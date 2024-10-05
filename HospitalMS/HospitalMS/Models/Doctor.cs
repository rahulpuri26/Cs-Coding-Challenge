using System;
namespace HospitalMS.Models
{
    public class Doctor
    {
       
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
        public string ContactNumber { get; set; }

        
        public Doctor() { }
        public Doctor(int doctorId, string firstName, string lastName, string specialization, string contactNumber)
        {
            DoctorId = doctorId;
            FirstName = firstName;
            LastName = lastName;
            Specialization = specialization;
            ContactNumber = contactNumber;
        }

        public void PrintDetails()
        {
            Console.WriteLine($"DoctorId: {DoctorId}, FirstName: {FirstName}, LastName: {LastName}, Specialization: {Specialization}, ContactNumber: {ContactNumber}");
        }
    }
}

