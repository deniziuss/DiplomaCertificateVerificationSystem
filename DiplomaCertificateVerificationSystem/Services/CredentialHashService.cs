
using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using DiplomaCertificateVerificationSystem.Models;

namespace DiplomaCertificateVerificationSystem.Services
{
    public static class CredentialHashService
    {
        // Checks local credential data against its stored SHA-256 hash.
        // This is an integrity check, not blockchain or signature verification.
        public static bool VerifyHash(DigitalCredential credential)
        {
            ArgumentNullException.ThrowIfNull(credential);

            if (string.IsNullOrWhiteSpace(credential.DocumentHash))
            {
                return false;
            }

            string expectedHash = CalculateHash(credential);
            return string.Equals(
                expectedHash,
                credential.DocumentHash,
                StringComparison.OrdinalIgnoreCase);
        }

        public static string CalculateHash(DigitalCredential credential)
        {
            ArgumentNullException.ThrowIfNull(credential);

            string data = JsonSerializer.Serialize(new
            {
                CredentialId = credential.CredentialId,
                StudentId = credential.StudentId,
                UniversityId = credential.UniversityId,
                CredentialType = credential.GetCredentialType(),
                Title = credential.Title,
                ProgramOrCourse = credential.ProgramOrCourse,
                AdditionalInformation = credential.AdditionalInformation,
                IssueDate = credential.IssueDate.ToString(
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture)
            });

            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] hash = SHA256.HashData(bytes);

            return Convert.ToHexString(hash);
        }
    }
}
