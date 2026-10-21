using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DiplomaCertificateVerificationSystem.Forms
{
    public partial class IssueCredentialForm : Form
    {
        private CredentialService credentialService;
        private DigitalCredential lastCreatedCredential;

        public IssueCredentialForm()
        {
            InitializeComponent();
            credentialService = new CredentialService();
            LoadComboBoxData();
        }

        private void LoadComboBoxData()
        {
            // Populate ComboBoxes with sample data
            cmbCredentialType.Items.Add("Diploma");
            cmbCredentialType.Items.Add("Certificate");

            cmbUniversity.Items.Add("Rīgas Tehniskā universitāte");
            cmbUniversity.Items.Add("Afyon Kocatepe Üniversitesi");

            cmbStudent.Items.Add("Enes Taş");
            cmbStudent.Items.Add("Nevzat Deniz Fersiz");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtTitle.Clear();
            txtProgram.Clear();
            txtAdditionalInfo.Clear();
            cmbStudent.SelectedIndex = -1;
            cmbUniversity.SelectedIndex = -1;
            cmbCredentialType.SelectedIndex = -1;
            lastCreatedCredential = null;
        }

        private void btnGenerateHash_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text) || cmbStudent.SelectedItem == null)
            {
                MessageBox.Show("Please fill in the required fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Generate hash preview
            var hashService = new CredentialHashService();
            string rawData = $"{cmbStudent.Text}-{cmbUniversity.Text}-{txtTitle.Text}-{dtpIssueDate.Value}";
            string previewHash = hashService.ComputeHash(rawData);

            MessageBox.Show($"Temporary Credential Hash Value:\n{previewHash}", "Hash Preview", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnIssueCredential_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCredentialType.SelectedItem == null || cmbStudent.SelectedItem == null || cmbUniversity.SelectedItem == null)
                {
                    MessageBox.Show("Please make all required selections!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string type = cmbCredentialType.SelectedItem.ToString();
                string student = cmbStudent.SelectedItem.ToString();
                string university = cmbUniversity.SelectedItem.ToString();
                string title = txtTitle.Text;
                string program = txtProgram.Text;
                DateTime issueDate = dtpIssueDate.Value;
                string info = txtAdditionalInfo.Text;

                // Create credential via service and save to disk
                lastCreatedCredential = credentialService.IssueCredential(type, title, student, university, issueDate, info);

                MessageBox.Show($"Credential created successfully!\nID: {lastCreatedCredential.CredentialID}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegisterBlockchain_Click(object sender, EventArgs e)
        {
            if (lastCreatedCredential == null)
            {
                MessageBox.Show("You must create a credential first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Blockchain registration simulation
            MessageBox.Show($"Credential ({lastCreatedCredential.CredentialID}) successfully registered to the blockchain network!", "Blockchain Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dtpIssueDate_ValueChanged(object sender, EventArgs e)
        {
        }

        private void cmbStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cmbUniversity_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cmbCredentialType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtProgram_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtAdditionalInfo_TextChanged(object sender, EventArgs e)
        {
        }

        private void IssueCredentialForm_Load(object sender, EventArgs e)
        {

        }
    }
}