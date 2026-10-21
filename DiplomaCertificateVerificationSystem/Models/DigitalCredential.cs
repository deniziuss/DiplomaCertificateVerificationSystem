
using System;

namespace DiplomaCertificateVerificationSystem.Models
{
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

        protected DigitalCredential(
            Guid studentId,
            Guid universityId,
            string title,
            string programOrCourse,
            string additionalInformation,
            DateTime issueDate)
        {
            CredentialId = Guid.NewGuid();
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
