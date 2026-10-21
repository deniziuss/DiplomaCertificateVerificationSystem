
using System;

namespace DiplomaCertificateVerificationSystem.Models
{
    public class Diploma : DigitalCredential
    {
        public Diploma(
            Guid studentId,
            Guid universityId,
            string title,
            string programOrCourse,
            string additionalInformation,
            DateTime issueDate)
            : base(
                studentId,
                universityId,
                title,
                programOrCourse,
                additionalInformation,
                issueDate)
        {
        }

        public override string GetCredentialType()
        {
            return "Diploma";
        }

        public override string GetDisplayTitle()
        {
            return "Diploma: " + Title;
        }
    }
}
