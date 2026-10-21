
using System;
using System.Text.Json.Serialization;

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

        // Restores an existing credential without generating a new ID.
        [JsonConstructor]
        public Diploma(
            Guid credentialId,
            Guid studentId,
            Guid universityId,
            string title,
            string programOrCourse,
            string additionalInformation,
            DateTime issueDate)
            : base(
                credentialId,
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
