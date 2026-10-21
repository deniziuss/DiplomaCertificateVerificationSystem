using System;
using System.Windows.Forms;
using DiplomaCertificateVerificationSystem.Forms;
using DiplomaCertificateVerificationSystem.Models;
using DiplomaCertificateVerificationSystem.Services;

ApplicationConfiguration.Initialize();

var credential = new Diploma(
    Guid.NewGuid(),
    Guid.NewGuid(),
    "Demo Diploma",
    "Computer Science",
    "Sample credential for checking the details screen.",
    DateTime.Today);

credential.DocumentHash = CredentialHashService.CalculateHash(credential);
credential.Status = "Issued";

Application.Run(new CredentialDetailsForm(
    credential,
    "Example Student",
    "Example University"));
