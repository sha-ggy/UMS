namespace Portal
{
    partial class ForgetPass
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label lblOtp;
        private System.Windows.Forms.TextBox txtOtp;
        private System.Windows.Forms.Button btnVerifyOtp;

        // Clean up any resources being used.
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // Required method for Designer support
        private void InitializeComponent()
        {
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.lblOtp = new System.Windows.Forms.Label();
            this.txtOtp = new System.Windows.Forms.TextBox();
            this.btnVerifyOtp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(50, 30);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(85, 13);
            this.lblEmail.TabIndex = 0;
            this.lblEmail.Text = "Enter your email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(150, 27);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 20);
            this.txtEmail.TabIndex = 1;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(150, 60);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(100, 23);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Generate OTP";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // lblOtp
            // 
            this.lblOtp.AutoSize = true;
            this.lblOtp.Location = new System.Drawing.Point(50, 100);
            this.lblOtp.Name = "lblOtp";
            this.lblOtp.Size = new System.Drawing.Size(110, 13);
            this.lblOtp.TabIndex = 3;
            this.lblOtp.Text = "Enter your OTP code:";
            // 
            // txtOtp
            // 
            this.txtOtp.Location = new System.Drawing.Point(150, 97);
            this.txtOtp.Name = "txtOtp";
            this.txtOtp.Size = new System.Drawing.Size(200, 20);
            this.txtOtp.TabIndex = 4;
            // 
            // btnVerifyOtp
            // 
            this.btnVerifyOtp.Location = new System.Drawing.Point(150, 130);
            this.btnVerifyOtp.Name = "btnVerifyOtp";
            this.btnVerifyOtp.Size = new System.Drawing.Size(100, 23);
            this.btnVerifyOtp.TabIndex = 5;
            this.btnVerifyOtp.Text = "Verify OTP";
            this.btnVerifyOtp.UseVisualStyleBackColor = true;
            this.btnVerifyOtp.Click += new System.EventHandler(this.btnVerifyOtp_Click);
            // 
            // ForgetPass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.btnVerifyOtp);
            this.Controls.Add(this.txtOtp);
            this.Controls.Add(this.lblOtp);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Name = "ForgetPass";
            this.Text = "Forget Password";
            this.Load += new System.EventHandler(this.ForgetPass_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
