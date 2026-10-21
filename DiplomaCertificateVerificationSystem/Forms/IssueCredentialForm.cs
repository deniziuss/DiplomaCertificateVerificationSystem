using System;
using System.Windows.Forms;
using DiplomaCertificateVerificationSystem.Models;
using DiplomaCertificateVerificationSystem.Services;

namespace DiplomaCertificateVerificationSystem.Forms
{
    public partial class IssueCredentialForm : Form
    {
        private readonly CredentialService credentialService;
        private DigitalCredential? draftCredential;
        private DigitalCredential? lastCreatedCredential;

        public IssueCredentialForm()
        {
            InitializeComponent();
            credentialService = new CredentialService();
            LoadComboBoxData();
            Text = "Issue Diploma or Certificate";
        }

        private void LoadComboBoxData()
        {
            cmbCredentialType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStudent.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUniversity.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbCredentialType.Items.Add("Diploma");
            cmbCredentialType.Items.Add("Certificate");

            // Temporary demo records. Replace these with Team 1 and Team 2 data.
            cmbUniversity.Items.Add(new SelectionItem(
                "50000000-0000-4000-8000-000000000001", "Rīgas Tehniskā universitāte"));
            cmbUniversity.Items.Add(new SelectionItem(
                "50000000-0000-4000-8000-000000000002", "Afyon Kocatepe Üniversitesi"));

            cmbStudent.Items.Add(new SelectionItem(
                "60000000-0000-4000-8000-000000000001", "Enes Taş"));
            cmbStudent.Items.Add(new SelectionItem(
                "60000000-0000-4000-8000-000000000002", "Nevzat Deniz Fersiz"));
        }

        private DigitalCredential CreateDraftFromForm()
        {
            if (cmbCredentialType.SelectedItem is not string type ||
                cmbStudent.SelectedItem is not SelectionItem student ||
                cmbUniversity.SelectedItem is not SelectionItem university)
            {
                throw new ArgumentException("Select a credential type, student, and university.");
            }

            return credentialService.PrepareCredential(
                type,
                student.Id,
                university.Id,
                txtTitle.Text,
                txtProgram.Text,
                dtpIssueDate.Value.Date,
                txtAdditionalInfo.Text);
        }

        private void btnGenerateHash_Click(object sender, EventArgs e)
        {
            try
            {
                draftCredential = CreateDraftFromForm();
                MessageBox.Show(
                    $"Draft credential hash:\n{draftCredential.DocumentHash}\n\n" +
                    "This hash will be used when the credential is issued unless the details change.",
                    "SHA-256 Preview", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Missing Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnIssueCredential_Click(object sender, EventArgs e)
        {
            try
            {
                draftCredential ??= CreateDraftFromForm();
                lastCreatedCredential = credentialService.IssueCredential(draftCredential);
                draftCredential = null;

                MessageBox.Show(
                    $"Credential created and saved.\nID: {lastCreatedCredential.CredentialId}",
                    "Credential Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var student = (SelectionItem)cmbStudent.SelectedItem!;
                var university = (SelectionItem)cmbUniversity.SelectedItem!;
                using var detailsForm = new CredentialDetailsForm(
                    lastCreatedCredential, student.Name, university.Name);
                detailsForm.ShowDialog(this);
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException or System.IO.IOException)
            {
                MessageBox.Show(ex.Message, "Cannot Issue Credential",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegisterBlockchain_Click(object sender, EventArgs e)
        {
            if (lastCreatedCredential == null)
            {
                MessageBox.Show("Issue a credential first.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show(
                "The credential was saved locally. Blockchain registration is not available yet.",
                "Integration Pending", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtTitle.Clear();
            txtProgram.Clear();
            txtAdditionalInfo.Clear();
            cmbStudent.SelectedIndex = -1;
            cmbUniversity.SelectedIndex = -1;
            cmbCredentialType.SelectedIndex = -1;
            dtpIssueDate.Value = DateTime.Today;
            ResetDraft();
        }

        private void ResetDraft()
        {
            draftCredential = null;
            lastCreatedCredential = null;
        }

        // The hash preview is invalidated whenever its input changes.
        private void dtpIssueDate_ValueChanged(object sender, EventArgs e) => ResetDraft();
        private void cmbStudent_SelectedIndexChanged(object sender, EventArgs e) => ResetDraft();
        private void cmbUniversity_SelectedIndexChanged(object sender, EventArgs e) => ResetDraft();
        private void cmbCredentialType_SelectedIndexChanged(object sender, EventArgs e) => ResetDraft();
        private void txtTitle_TextChanged(object sender, EventArgs e) => ResetDraft();
        private void txtProgram_TextChanged(object sender, EventArgs e) => ResetDraft();
        private void txtAdditionalInfo_TextChanged(object sender, EventArgs e) => ResetDraft();

        private void IssueCredentialForm_Load(object sender, EventArgs e)
        {
        }

        private sealed class SelectionItem
        {
            public Guid Id { get; }
            public string Name { get; }

            public SelectionItem(string id, string name)
            {
                Id = Guid.Parse(id);
                Name = name;
            }

            public override string ToString() => Name;
        }
    }
}
