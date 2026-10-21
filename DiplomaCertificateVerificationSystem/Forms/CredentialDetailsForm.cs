using DiplomaCertificateVerificationSystem.Models;
using DiplomaCertificateVerificationSystem.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DiplomaCertificateVerificationSystem.Forms
{
    public partial class CredentialDetailsForm : Form
    {
        private Label lblCredentialIdValue = null!;
        private Label lblCredentialTypeValue = null!;
        private Label lblCredentialTitleValue = null!;
        private Label lblStudentValue = null!;
        private Label lblUniversityValue = null!;
        private Label lblIssueDateValue = null!;
        private Label lblTransactionIdValue = null!;
        private Label lblStatusValue = null!;

        private TextBox txtCredentialHash = null!;
        private Label lblHashIntegrityValue = null!;

        public CredentialDetailsForm()
        {
            InitializeComponent();
            BuildLayout();
        }


        public CredentialDetailsForm(
            DigitalCredential credential,
            string studentName,
            string universityName,
            string? transactionId = null) : this()
        {
            DisplayCredential(
                credential,
                studentName,
                universityName,
                transactionId);
        }

        public void DisplayCredential(
            DigitalCredential credential,
            string studentName,
            string universityName,
            string? transactionId = null)
        {
            ArgumentNullException.ThrowIfNull(credential);

            lblCredentialIdValue.Text =
                credential.CredentialId.ToString();

            lblCredentialTypeValue.Text =
                credential.GetCredentialType();

            lblCredentialTitleValue.Text =
                credential.Title;

            lblStudentValue.Text =
                studentName;

            lblUniversityValue.Text =
                universityName;

            lblIssueDateValue.Text =
                credential.IssueDate.ToString("dd.MM.yyyy");

            lblTransactionIdValue.Text =
                string.IsNullOrWhiteSpace(transactionId)
                ? "Not registered"
                : transactionId;

            lblStatusValue.Text =
                credential.Status;

            txtCredentialHash.Text =
                string.IsNullOrWhiteSpace(credential.DocumentHash)
                ? "Not generated"
                : credential.DocumentHash;

            // Local hash integrity is separate from blockchain verification.
            if (string.IsNullOrWhiteSpace(credential.DocumentHash))
            {
                lblHashIntegrityValue.Text = "Integrity: No hash generated";
                lblHashIntegrityValue.ForeColor = Color.DarkOrange;
            }
            else if (CredentialHashService.VerifyHash(credential))
            {
                lblHashIntegrityValue.Text = "Integrity: Hash matches document data (local check)";
                lblHashIntegrityValue.ForeColor = Color.DarkGreen;
            }
            else
            {
                lblHashIntegrityValue.Text = "Integrity: Hash mismatch - document may have changed";
                lblHashIntegrityValue.ForeColor = Color.DarkRed;
            }
        }


        private void BuildLayout()
        {
            Text = "Credential Details";
            ClientSize = new Size(960, 710);
            MinimumSize = new Size(760, 640);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(245, 247, 251);
            Font = new Font("Segoe UI", 10);

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                Padding = new Padding(30),
                BackColor = BackColor
            };

            root.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));

            root.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 90));
            root.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 120));
            root.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 75));

            Controls.Add(root);

            // Header
            var header = new Panel
            {
                Dock = DockStyle.Fill
            };

            var title = new Label
            {
                Text = "Credential Details",
                Font = new Font("Segoe UI", 23, FontStyle.Bold),
                ForeColor = Color.FromArgb(24, 39, 67),
                Location = new Point(0, 2),
                AutoSize = true
            };

            var subtitle = new Label
            {
                Text = "Diploma and Certificate Verification System",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Gray,
                Location = new Point(3, 53),
                AutoSize = true
            };

            header.Controls.Add(title);
            header.Controls.Add(subtitle);
            root.Controls.Add(header, 0, 0);

            // Credential information
            var details = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                ColumnCount = 2,
                RowCount = 4,
                Padding = new Padding(12)
            };

            details.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));
            details.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50));

            for (int i = 0; i < 4; i++)
            {
                details.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 25));
            }

            details.Controls.Add(
                CreateField("Credential ID",
                    out lblCredentialIdValue), 0, 0);

            details.Controls.Add(
                CreateField("Credential Type",
                    out lblCredentialTypeValue), 1, 0);

            details.Controls.Add(
                CreateField("Credential Title",
                    out lblCredentialTitleValue), 0, 1);

            details.Controls.Add(
                CreateField("Student",
                    out lblStudentValue), 1, 1);

            details.Controls.Add(
                CreateField("University",
                    out lblUniversityValue), 0, 2);

            details.Controls.Add(
                CreateField("Issue Date",
                    out lblIssueDateValue), 1, 2);

            details.Controls.Add(
                CreateField("Blockchain Transaction ID",
                    out lblTransactionIdValue), 0, 3);

            details.Controls.Add(
                CreateField("Credential Status",
                    out lblStatusValue), 1, 3);

            lblCredentialIdValue.Font = new Font("Consolas", 9);
            lblCredentialIdValue.AutoEllipsis = false;
            root.Controls.Add(details, 0, 1);

            // SHA-256 section
            var hashPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = Color.White,
                Padding = new Padding(18),
                Margin = new Padding(0, 12, 0, 0)
            };

            hashPanel.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 28));
            hashPanel.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 24));
            hashPanel.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            var hashTitle = new Label
            {
                Text = "Credential Hash (SHA-256)",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(70, 85, 110),
                Dock = DockStyle.Fill
            };

            lblHashIntegrityValue = new Label
            {
                Text = "Integrity: Not checked",
                Font = new Font("Segoe UI", 9),
                Dock = DockStyle.Fill,
                ForeColor = Color.Gray
            };

            txtCredentialHash = new TextBox
            {
                Name = "txtCredentialHash",
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                Text = "",
                BackColor = Color.FromArgb(247, 249, 252),
                ForeColor = Color.FromArgb(35, 50, 75),
                Font = new Font("Consolas", 10),
                BorderStyle = BorderStyle.FixedSingle
            };

            hashPanel.Controls.Add(hashTitle, 0, 0);
            hashPanel.Controls.Add(lblHashIntegrityValue, 0, 1);
            hashPanel.Controls.Add(txtCredentialHash, 0, 2);

            root.Controls.Add(hashPanel, 0, 2);

            // Buttons
            var footer = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(0, 16, 0, 0)
            };

            var btnClose = new Button
            {
                Name = "btnClose",
                Text = "Close",
                Width = 120,
                Height = 42,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(35, 50, 75)
            };

            btnClose.FlatAppearance.BorderColor =
                Color.FromArgb(210, 218, 230);

            btnClose.Click += (sender, e) => Close();

            var btnViewBlockchain = new Button
            {
                Name = "btnViewBlockchain",
                Text = "View on Blockchain",
                Width = 200,
                Height = 42,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(38, 93, 190),
                ForeColor = Color.White
            };

            btnViewBlockchain.FlatAppearance.BorderSize = 0;

            btnViewBlockchain.Click += (sender, e) =>
            {
                MessageBox.Show(
                    "Blockchain integration is not available yet.",
                    "Integration Pending",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            };

            footer.Controls.Add(btnClose);
            footer.Controls.Add(btnViewBlockchain);

            root.Controls.Add(footer, 0, 3);
        }

        private Control CreateField(
            string caption,
            out Label valueLabel)
        {
            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Margin = new Padding(12, 8, 12, 8)
            };

            panel.RowStyles.Add(
                new RowStyle(SizeType.Absolute, 26));
            panel.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100));

            var label = new Label
            {
                Text = caption,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(110, 120, 140)
            };

            valueLabel = new Label
            {
                Text = "-",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(25, 40, 65),
                AutoEllipsis = true
            };

            panel.Controls.Add(label, 0, 0);
            panel.Controls.Add(valueLabel, 0, 1);

            return panel;
        }
    }
}
