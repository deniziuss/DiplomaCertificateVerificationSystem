
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
