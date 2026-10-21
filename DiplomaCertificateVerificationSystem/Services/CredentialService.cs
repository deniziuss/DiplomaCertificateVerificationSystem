using System;
using System.Collections.Generic;
using System.Linq;

public class CredentialService
{
    private List<DigitalCredential> credentials;
    private readonly CredentialRepository repository;
    private readonly CredentialHashService hashService;

    public CredentialService()
    {
        repository = new CredentialRepository();
        hashService = new CredentialHashService();
        credentials = repository.Load(); // Uygulama açılınca kayıtlıları yükle
    }

    // Benzersiz ID ve Diploma/Sertifika Üretme
    public DigitalCredential IssueCredential(string type, string title, string studentName, string universityName, DateTime issueDate, string additionalInfo)
    {
        // 1. Benzersiz Credential ID üret
        string credentialId = "CRED-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

        DigitalCredential credential;

        // 2. Türüne göre nesne türet (Polimorfizm)
        if (type.Equals("Diploma", StringComparison.OrdinalIgnoreCase))
        {
            credential = new Diploma
            {
                CredentialID = credentialId,
                Title = title,
                StudentName = studentName,
                UniversityName = universityName,
                IssueDate = issueDate,
                AdditionalInfo = additionalInfo,
                Status = "Valid"
            };
        }
        else
        {
            credential = new Certificate
            {
                CredentialID = credentialId,
                Title = title,
                StudentName = studentName,
                UniversityName = universityName,
                IssueDate = issueDate,
                AdditionalInfo = additionalInfo,
                Status = "Valid"
            };
        }

        // 3. Hash Hesapla (Deniz'in servisi üzerinden)
        string rawData = $"{credential.CredentialID}-{credential.StudentName}-{credential.UniversityName}-{credential.Title}-{credential.IssueDate}";
        credential.CredentialHash = hashService.ComputeHash(rawData);

        // 4. Listeye ekle ve diske kaydet
        credentials.Add(credential);
        repository.Save(credentials);

        return credential;
    }

    public List<DigitalCredential> GetAllCredentials()
    {
        return credentials;
    }

    public DigitalCredential GetCredentialById(string credentialId)
    {
        return credentials.FirstOrDefault(c => c.CredentialID == credentialId);
    }
}