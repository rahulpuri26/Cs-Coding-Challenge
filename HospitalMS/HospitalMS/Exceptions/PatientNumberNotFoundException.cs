using System;
namespace HospitalMS.Exceptions
{
    public class PatientNumberNotFoundException : Exception
    {
        public PatientNumberNotFoundException() { }

        public PatientNumberNotFoundException(string message) : base(message) { }

        public PatientNumberNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}

