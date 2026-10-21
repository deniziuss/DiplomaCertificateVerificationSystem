using System;
using System.Text.Json.Serialization;

namespace DiplomaCertificateVerificationSystem.Models
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(Diploma), "diploma")]
    [JsonDerivedType(typeof(Certificate), "certificate")]
    public abstract class DigitalCredential
    {
        public Guid CredentialId { get; private set; }
        public Guid StudentId { get; private set; }
        public Guid UniversityId { get; private set; }

        public string Title { get; set; }
        public string ProgramOrCourse { get; set; }
        public string AdditionalInformation { get; set; }
        public DateTime IssueDate { get; set; }

        public string DocumentHash { get; set; } = "";
        public string DigitalSignature { get; set; } = "";
        public string Status { get; set; } = "Draft";
        public string CredentialType => GetCredentialType();

        // Used when issuing a new credential.
        protected DigitalCredential(
            Guid studentId,
            Guid universityId,
            string title,
            string programOrCourse,
            string additionalInformation,
            DateTime issueDate)
            : this(Guid.NewGuid(), studentId, universityId, title,
                programOrCourse, additionalInformation, issueDate)
        {
        }

        // Used by JSON deserialization to preserve the original credential ID.
        protected DigitalCredential(
            Guid credentialId,
            Guid studentId,
            Guid universityId,
            string title,
            string programOrCourse,
            string additionalInformation,
            DateTime issueDate)
        {
            if (credentialId == Guid.Empty)
            {
                throw new ArgumentException(
                    "Credential ID cannot be empty.", nameof(credentialId));
            }

            CredentialId = credentialId;
            StudentId = studentId;
            UniversityId = universityId;
            Title = title;
            ProgramOrCourse = programOrCourse;
            AdditionalInformation = additionalInformation;
            IssueDate = issueDate;
        }

        public abstract string GetCredentialType();

        public virtual string GetDisplayTitle()
        {
            return Title;
        }
    }
}
