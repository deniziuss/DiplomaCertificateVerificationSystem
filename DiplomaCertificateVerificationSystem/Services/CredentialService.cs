using System;
using System.Collections.Generic;
using System.Linq;
using DiplomaCertificateVerificationSystem.Data;
using DiplomaCertificateVerificationSystem.Models;

namespace DiplomaCertificateVerificationSystem.Services
{
    public class CredentialService
    {
        private readonly List<DigitalCredential> credentials;
        private readonly CredentialRepository repository;

        public CredentialService(CredentialRepository? repository = null)
        {
            this.repository = repository ?? new CredentialRepository();
            credentials = this.repository.Load();
        }

        public DigitalCredential PrepareCredential(
            string type,
            Guid studentId,
            Guid universityId,
            string title,
            string programOrCourse,
            DateTime issueDate,
            string additionalInformation)
        {
            if (studentId == Guid.Empty || universityId == Guid.Empty)
                throw new ArgumentException("Select a valid student and university.");

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(programOrCourse))
                throw new ArgumentException("Title and program/course are required.");

            DigitalCredential credential;

            if (string.Equals(type, "Diploma", StringComparison.OrdinalIgnoreCase))
            {
                credential = new Diploma(studentId, universityId, title.Trim(),
                    programOrCourse.Trim(), additionalInformation, issueDate);
            }
            else if (string.Equals(type, "Certificate", StringComparison.OrdinalIgnoreCase))
            {
                credential = new Certificate(studentId, universityId, title.Trim(),
                    programOrCourse.Trim(), additionalInformation, issueDate);
            }
            else
            {
                throw new ArgumentException("Select a credential type.", nameof(type));
            }

            credential.DocumentHash = CredentialHashService.CalculateHash(credential);
            return credential;
        }

        public DigitalCredential IssueCredential(DigitalCredential credential)
        {
            ArgumentNullException.ThrowIfNull(credential);

            if (credentials.Any(c => c.CredentialId == credential.CredentialId))
                throw new InvalidOperationException("This credential has already been issued.");

            if (!CredentialHashService.VerifyHash(credential))
                throw new InvalidOperationException("Credential data does not match its hash.");

            credential.Status = "Issued";

            // Save first so a failed disk write does not add an in-memory credential.
            var updatedCredentials = new List<DigitalCredential>(credentials) { credential };
            repository.Save(updatedCredentials);
            credentials.Add(credential);

            return credential;
        }

        public IReadOnlyList<DigitalCredential> GetAllCredentials()
        {
            return credentials.AsReadOnly();
        }

        public DigitalCredential? GetCredentialById(Guid credentialId)
        {
            return credentials.FirstOrDefault(c => c.CredentialId == credentialId);
        }
    }
}
