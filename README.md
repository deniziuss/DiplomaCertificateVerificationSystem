# Diploma and Certificate Verification System

C# Windows Forms project for Team 3: Diploma and Certificate Issuance.

## Requirements
- Visual Studio with .NET 10 and Windows Forms support

Open `DiplomaCertificateVerificationSystem.slnx` and run the application.

## Features
- Create diploma and certificate records
- Generate unique credential IDs
- Calculate and check SHA-256 hashes
- Save and load credentials in JSON
- Display credential details

## Main files
- `Models/` — DigitalCredential, Diploma, Certificate
- `Services/` — credential issuance and hashing
- `Data/CredentialRepository.cs` — JSON storage
- `Forms/` — issuance and details forms

## Tests
From the repository folder, run:

```powershell
dotnet run --project CredentialModelSmokeTests/CredentialModelSmokeTests.csproj
```

The form currently uses sample students and universities. Data from the Student and University modules and blockchain registration will be connected during system integration.

## Branches
- `master` — combined project
- `deniz` — credential models and details form work
- `enes` — original issuance form and service work
