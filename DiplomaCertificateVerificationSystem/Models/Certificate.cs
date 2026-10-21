
using System;

namespace DiplomaCertificateVerificationSystem.Models
{
    public class Certificate : DigitalCredential
    {
        public Certificate(
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
            return "Certificate";
        }

        public override string GetDisplayTitle()
        {
            return "Certificate: " + Title;
        }
    }
}
