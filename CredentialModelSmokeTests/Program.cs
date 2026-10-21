using System;
using System.Collections.Generic;
using System.Text.Json;
using DiplomaCertificateVerificationSystem.Models;
using DiplomaCertificateVerificationSystem.Services;

static void Check(bool condition, string message)
{
    if (!condition)
    {
        throw new Exception("TEST FAILED: " + message);
    }

    Console.WriteLine("PASS: " + message);
}

Guid studentId = Guid.NewGuid();
Guid universityId = Guid.NewGuid();
DateTime issueDate = new DateTime(2026, 10, 21);

var diploma = new Diploma(
    studentId, universityId, "Bachelor Diploma",
    "Information Systems", "Graduated", issueDate);
var certificate = new Certificate(
    studentId, universityId, "C# Certificate",
    "C# Fundamentals", "Completed", issueDate);

diploma.DocumentHash = CredentialHashService.CalculateHash(diploma);
certificate.DocumentHash = CredentialHashService.CalculateHash(certificate);

Check(diploma.DocumentHash.Length == 64, "SHA-256 hash is 64 hex characters");
Check(CredentialHashService.VerifyHash(diploma), "Diploma hash is valid");
Check(CredentialHashService.VerifyHash(certificate), "Certificate hash is valid");

var original = new List<DigitalCredential> { diploma, certificate };
string json = JsonSerializer.Serialize(original);
Check(json.Contains("\"$type\""), "JSON contains a credential type discriminator");

List<DigitalCredential> restored =
    JsonSerializer.Deserialize<List<DigitalCredential>>(json)
    ?? throw new Exception("Deserialization returned null");

Check(restored.Count == 2, "Two credentials restored");
Check(restored[0] is Diploma, "Diploma type preserved");
Check(restored[1] is Certificate, "Certificate type preserved");
Check(restored[0].CredentialId == diploma.CredentialId, "Diploma ID preserved");
Check(restored[1].CredentialId == certificate.CredentialId, "Certificate ID preserved");
Check(restored[0].StudentId == studentId, "Student ID preserved");
Check(restored[0].UniversityId == universityId, "University ID preserved");
Check(CredentialHashService.VerifyHash(restored[0]), "Restored diploma hash is valid");
Check(CredentialHashService.VerifyHash(restored[1]), "Restored certificate hash is valid");

restored[0].Title = "Changed Diploma Title";
Check(!CredentialHashService.VerifyHash(restored[0]), "Changed title is detected");

restored[1].DocumentHash = "";
Check(!CredentialHashService.VerifyHash(restored[1]), "Missing hash is rejected");


// The service and repository should work together without changing the credential ID.
string testFile = Path.Combine(Path.GetTempPath(), "credential-test-" + Guid.NewGuid() + ".json");

try
{
    var repository = new DiplomaCertificateVerificationSystem.Data.CredentialRepository(testFile);
    var service = new DiplomaCertificateVerificationSystem.Services.CredentialService(repository);
    var draft = service.PrepareCredential(
        "Diploma", studentId, universityId, "Test Diploma",
        "Information Systems", issueDate, "Graduated");
    Guid issuedId = draft.CredentialId;

    Check(CredentialHashService.VerifyHash(draft), "Prepared credential hash is valid");
    service.IssueCredential(draft);
    Check(File.Exists(testFile), "Issued credential saved to disk");

    var reloadedService = new DiplomaCertificateVerificationSystem.Services.CredentialService(repository);
    var saved = reloadedService.GetCredentialById(issuedId);
    Check(saved is Diploma, "Issued diploma reloaded from JSON");
    Check(saved?.CredentialId == issuedId, "Issued credential ID preserved");
    Check(saved != null && CredentialHashService.VerifyHash(saved), "Saved credential hash is valid");

    bool duplicateRejected = false;
    try
    {
        service.IssueCredential(draft);
    }
    catch (InvalidOperationException)
    {
        duplicateRejected = true;
    }
    Check(duplicateRejected, "Duplicate issue rejected");

    File.WriteAllText(testFile, "{ invalid json");
    bool invalidFileRejected = false;
    try
    {
        repository.Load();
    }
    catch (InvalidDataException)
    {
        invalidFileRejected = true;
    }

    Check(invalidFileRejected, "Invalid JSON produces an error");
    Check(File.ReadAllText(testFile) == "{ invalid json", "Invalid JSON file is not overwritten");
}
finally
{
    if (File.Exists(testFile))
        File.Delete(testFile);
    if (File.Exists(testFile + ".tmp"))
        File.Delete(testFile + ".tmp");
}

Console.WriteLine("All credential model checks passed.");
