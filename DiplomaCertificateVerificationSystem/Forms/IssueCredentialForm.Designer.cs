namespace DiplomaCertificateVerificationSystem.Forms
{
    partial class IssueCredentialForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cmbStudent = new ComboBox();
            cmbUniversity = new ComboBox();
            cmbCredentialType = new ComboBox();
            btnGenerateHash = new Button();
            btnIssueCredential = new Button();
            btnRegisterBlockchain = new Button();
            btnClear = new Button();
            txtTitle = new TextBox();
            txtProgram = new TextBox();
            txtAdditionalInfo = new TextBox();
            dtpIssueDate = new DateTimePicker();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            SuspendLayout();
            // 
            // cmbStudent
            // 
            cmbStudent.FormattingEnabled = true;
            cmbStudent.Location = new Point(35, 49);
            cmbStudent.Name = "cmbStudent";
            cmbStudent.Size = new Size(151, 28);
            cmbStudent.TabIndex = 0;
            cmbStudent.SelectedIndexChanged += cmbStudent_SelectedIndexChanged;
            // 
            // cmbUniversity
            // 
            cmbUniversity.FormattingEnabled = true;
            cmbUniversity.Location = new Point(35, 132);
            cmbUniversity.Name = "cmbUniversity";
            cmbUniversity.Size = new Size(151, 28);
            cmbUniversity.TabIndex = 1;
            cmbUniversity.SelectedIndexChanged += cmbUniversity_SelectedIndexChanged;
            // 
            // cmbCredentialType
            // 
            cmbCredentialType.FormattingEnabled = true;
            cmbCredentialType.Location = new Point(35, 211);
            cmbCredentialType.Name = "cmbCredentialType";
            cmbCredentialType.Size = new Size(151, 28);
            cmbCredentialType.TabIndex = 2;
            cmbCredentialType.SelectedIndexChanged += cmbCredentialType_SelectedIndexChanged;
            // 
            // btnGenerateHash
            // 
            btnGenerateHash.Location = new Point(26, 288);
            btnGenerateHash.Name = "btnGenerateHash";
            btnGenerateHash.Size = new Size(174, 29);
            btnGenerateHash.TabIndex = 3;
            btnGenerateHash.Text = "GenerateHash";
            btnGenerateHash.UseVisualStyleBackColor = true;
            btnGenerateHash.Click += btnGenerateHash_Click;
            // 
            // btnIssueCredential
            // 
            btnIssueCredential.Location = new Point(206, 288);
            btnIssueCredential.Name = "btnIssueCredential";
            btnIssueCredential.Size = new Size(165, 29);
            btnIssueCredential.TabIndex = 4;
            btnIssueCredential.Text = "IssueCredential";
            btnIssueCredential.UseVisualStyleBackColor = true;
            btnIssueCredential.Click += btnIssueCredential_Click;
            // 
            // btnRegisterBlockchain
            // 
            btnRegisterBlockchain.Location = new Point(378, 288);
            btnRegisterBlockchain.Name = "btnRegisterBlockchain";
            btnRegisterBlockchain.Size = new Size(173, 29);
            btnRegisterBlockchain.TabIndex = 5;
            btnRegisterBlockchain.Text = "RegisterBlockchain";
            btnRegisterBlockchain.UseVisualStyleBackColor = true;
            btnRegisterBlockchain.Click += btnRegisterBlockchain_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(557, 288);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(179, 29);
            btnClear.TabIndex = 6;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(378, 52);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(174, 27);
            txtTitle.TabIndex = 7;
            txtTitle.TextChanged += txtTitle_TextChanged;
            // 
            // txtProgram
            // 
            txtProgram.Location = new Point(378, 133);
            txtProgram.Name = "txtProgram";
            txtProgram.Size = new Size(173, 27);
            txtProgram.TabIndex = 8;
            txtProgram.TextChanged += txtProgram_TextChanged;
            // 
            // txtAdditionalInfo
            // 
            txtAdditionalInfo.Location = new Point(378, 211);
            txtAdditionalInfo.Name = "txtAdditionalInfo";
            txtAdditionalInfo.Size = new Size(173, 27);
            txtAdditionalInfo.TabIndex = 9;
            txtAdditionalInfo.TextChanged += txtAdditionalInfo_TextChanged;
            // 
            // dtpIssueDate
            // 
            dtpIssueDate.Location = new Point(678, 50);
            dtpIssueDate.Name = "dtpIssueDate";
            dtpIssueDate.Size = new Size(250, 27);
            dtpIssueDate.TabIndex = 10;
            dtpIssueDate.ValueChanged += dtpIssueDate_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(35, 26);
            label1.Name = "label1";
            label1.Size = new Size(112, 20);
            label1.TabIndex = 11;
            label1.Text = "Credential Type";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(35, 109);
            label2.Name = "label2";
            label2.Size = new Size(117, 20);
            label2.TabIndex = 12;
            label2.Text = "University Name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(35, 188);
            label3.Name = "label3";
            label3.Size = new Size(104, 20);
            label3.TabIndex = 13;
            label3.Text = "Student Name";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(377, 29);
            label4.Name = "label4";
            label4.Size = new Size(110, 20);
            label4.TabIndex = 14;
            label4.Text = "Credential Title";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(378, 110);
            label5.Name = "label5";
            label5.Size = new Size(125, 20);
            label5.TabIndex = 15;
            label5.Text = "Program / Course";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(378, 188);
            label6.Name = "label6";
            label6.Size = new Size(109, 20);
            label6.TabIndex = 16;
            label6.Text = "Additional Info";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(680, 30);
            label7.Name = "label7";
            label7.Size = new Size(77, 20);
            label7.TabIndex = 17;
            label7.Text = "Issue Date";
            // 
            // IssueCredentialForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(979, 450);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dtpIssueDate);
            Controls.Add(txtAdditionalInfo);
            Controls.Add(txtProgram);
            Controls.Add(txtTitle);
            Controls.Add(btnClear);
            Controls.Add(btnRegisterBlockchain);
            Controls.Add(btnIssueCredential);
            Controls.Add(btnGenerateHash);
            Controls.Add(cmbCredentialType);
            Controls.Add(cmbUniversity);
            Controls.Add(cmbStudent);
            Name = "IssueCredentialForm";
            Text = "IssueCredentialForm";
            Load += IssueCredentialForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbStudent;
        private ComboBox cmbUniversity;
        private ComboBox cmbCredentialType;
        private Button btnGenerateHash;
        private Button btnIssueCredential;
        private Button btnRegisterBlockchain;
        private Button btnClear;
        private TextBox txtTitle;
        private TextBox txtProgram;
        private TextBox txtAdditionalInfo;
        private DateTimePicker dtpIssueDate;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
    }
}