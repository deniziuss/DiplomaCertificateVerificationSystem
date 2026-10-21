# Diploma and Certificate Verification System

Team 3 module for issuing diploma and certificate records in a .NET 10 Windows Forms application.

## Project structure

- `Models/` — shared credential types (`DigitalCredential`, `Diploma`, `Certificate`).
- `Services/CredentialHashService.cs` — SHA-256 creation and local integrity checks.
- `Services/CredentialService.cs` — prepares and issues credentials.
- `Data/CredentialRepository.cs` — saves and loads credential records as JSON.
- `Forms/IssueCredentialForm.cs` — credential issuance screen.
- `Forms/CredentialDetailsForm.cs` — credential details and hash status.

The student and university selectors currently use **sample records** with stable GUIDs. They must eventually use the student and university data from Teams 1 and 2.

**Blockchain registration is not implemented yet.** The related buttons only explain that the integration is pending. A matching SHA-256 hash checks local data integrity; it is not a digital signature or independent proof of issuance.

## Run

Open `DiplomaCertificateVerificationSystem.slnx` in Visual Studio with the .NET 10 SDK and Windows Forms development tools installed. The application starts at `IssueCredentialForm`.

```powershell
dotnet build DiplomaCertificateVerificationSystem/DiplomaCertificateVerificationSystem.csproj
dotnet run --project DiplomaCertificateVerificationSystem/DiplomaCertificateVerificationSystem.csproj
```

Credential data is saved to `credentials.json` beside the running application's executable. Invalid or incompatible JSON is not automatically deleted.

## Checks

```powershell
dotnet run --project CredentialModelSmokeTests/CredentialModelSmokeTests.csproj
dotnet run --project CredentialFormSmokeTests/CredentialFormSmokeTests.csproj
```

The first command tests models, hashing, serialization and repository operations. The second opens a sample credential details screen on Windows. The CI workflow runs model tests and builds both Windows Forms projects; it does not click through the GUI.

## Branches

- `master` — shared integration branch.
- `feature/enes-issuance` — preserved original issuance implementation by Enes.
- `feature/deniz-models` — preserved original models, hashing and details view by Deniz.
- `feature/deniz-model-persistence` — Deniz's JSON persistence and integrity improvements.
- `backup/master-before-team3-integration-20261021` — snapshot of the original `master` before integration.

Continue changes in new feature branches and merge with pull requests. Avoid force pushing to `master`.
