namespace Cloud.Forms
{
    partial class ViewSingleStudent
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_ReplaceStudent = new System.Windows.Forms.Button();
            this.richTextBox_SingleStudentDetails = new System.Windows.Forms.RichTextBox();
            this.btn_DeleteStudent = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.richTextBox_SingleStudentDetails);
            this.panel1.Location = new System.Drawing.Point(46, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(725, 287);
            this.panel1.TabIndex = 0;
            // 
            // btn_ReplaceStudent
            // 
            this.btn_ReplaceStudent.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ReplaceStudent.Location = new System.Drawing.Point(96, 355);
            this.btn_ReplaceStudent.Name = "btn_ReplaceStudent";
            this.btn_ReplaceStudent.Size = new System.Drawing.Size(149, 38);
            this.btn_ReplaceStudent.TabIndex = 2;
            this.btn_ReplaceStudent.Text = "Replace";
            this.btn_ReplaceStudent.UseVisualStyleBackColor = true;
            this.btn_ReplaceStudent.Click += new System.EventHandler(this.btn_ReplaceStudent_Click);
            // 
            // richTextBox_SingleStudentDetails
            // 
            this.richTextBox_SingleStudentDetails.Location = new System.Drawing.Point(35, 25);
            this.richTextBox_SingleStudentDetails.Name = "richTextBox_SingleStudentDetails";
            this.richTextBox_SingleStudentDetails.Size = new System.Drawing.Size(660, 239);
            this.richTextBox_SingleStudentDetails.TabIndex = 0;
            this.richTextBox_SingleStudentDetails.Text = "";
            // 
            // btn_DeleteStudent
            // 
            this.btn_DeleteStudent.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DeleteStudent.Location = new System.Drawing.Point(519, 355);
            this.btn_DeleteStudent.Name = "btn_DeleteStudent";
            this.btn_DeleteStudent.Size = new System.Drawing.Size(149, 38);
            this.btn_DeleteStudent.TabIndex = 3;
            this.btn_DeleteStudent.Text = "Delete";
            this.btn_DeleteStudent.UseVisualStyleBackColor = true;
            this.btn_DeleteStudent.Click += new System.EventHandler(this.btn_DeleteStudent_Click);
            // 
            // ViewSingleStudent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_DeleteStudent);
            this.Controls.Add(this.btn_ReplaceStudent);
            this.Controls.Add(this.panel1);
            this.Name = "ViewSingleStudent";
            this.Text = "ViewSingleStudent";
            this.Load += new System.EventHandler(this.ViewSingleStudent_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_ReplaceStudent;
        private System.Windows.Forms.RichTextBox richTextBox_SingleStudentDetails;
        private System.Windows.Forms.Button btn_DeleteStudent;
    }
}