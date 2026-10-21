using System;
using System.Text.Json.Serialization;

[JsonDerivedType(typeof(Diploma), typeDiscriminator: "diploma")]
[JsonDerivedType(typeof(Certificate), typeDiscriminator: "certificate")]
public abstract class DigitalCredential
{
    public string CredentialID { get; set; }
    public string Title { get; set; }
    public string StudentName { get; set; }
    public string UniversityName { get; set; }
    public DateTime IssueDate { get; set; }
    public string AdditionalInfo { get; set; }
    public string CredentialHash { get; set; }
    public string Status { get; set; }
}