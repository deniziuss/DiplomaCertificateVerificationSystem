using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using DiplomaCertificateVerificationSystem.Models;

namespace DiplomaCertificateVerificationSystem.Data
{
    public class CredentialRepository
    {
        private readonly string filePath;

        public CredentialRepository(string? filePath = null)
        {
            this.filePath = filePath ?? Path.Combine(AppContext.BaseDirectory, "credentials.json");
        }

        public void Save(List<DigitalCredential> credentials)
        {
            ArgumentNullException.ThrowIfNull(credentials);

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(credentials, options);

            string? folder = Path.GetDirectoryName(Path.GetFullPath(filePath));
            if (folder != null)
                Directory.CreateDirectory(folder);

            // Write to a temporary file before replacing the saved credentials.
            string tempFile = filePath + ".tmp";
            File.WriteAllText(tempFile, json);
            File.Move(tempFile, filePath, overwrite: true);
        }

        public List<DigitalCredential> Load()
        {
            if (!File.Exists(filePath))
                return new List<DigitalCredential>();

            string json = File.ReadAllText(filePath);
            if (string.IsNullOrWhiteSpace(json))
                return new List<DigitalCredential>();

            try
            {
                return JsonSerializer.Deserialize<List<DigitalCredential>>(json)
                       ?? new List<DigitalCredential>();
            }
            catch (Exception ex) when (
                ex is JsonException or NotSupportedException or ArgumentException)
            {
                // Never overwrite data when the file cannot be read.
                throw new InvalidDataException(
                    "The credentials file could not be loaded. The original file was not changed.", ex);
            }
        }
    }
}
