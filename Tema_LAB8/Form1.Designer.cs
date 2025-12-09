namespace Tema_LAB8
{
    partial class Form1
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
            this.pictureBoxFern = new System.Windows.Forms.PictureBox();
            this.trackBarPoints = new System.Windows.Forms.TrackBar();
            this.labelPoints = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFern)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPoints)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxFern
            // 
            this.pictureBoxFern.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxFern.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxFern.Name = "pictureBoxFern";
            this.pictureBoxFern.Size = new System.Drawing.Size(1775, 746);
            this.pictureBoxFern.TabIndex = 0;
            this.pictureBoxFern.TabStop = false;
            // 
            // trackBarPoints
            // 
            this.trackBarPoints.Location = new System.Drawing.Point(1276, 35);
            this.trackBarPoints.Maximum = 100000;
            this.trackBarPoints.Minimum = 1000;
            this.trackBarPoints.Name = "trackBarPoints";
            this.trackBarPoints.Size = new System.Drawing.Size(463, 56);
            this.trackBarPoints.TabIndex = 1;
            this.trackBarPoints.TickFrequency = 5000;
            this.trackBarPoints.Value = 20000;
            // 
            // labelPoints
            // 
            this.labelPoints.AutoSize = true;
            this.labelPoints.Location = new System.Drawing.Point(1273, 146);
            this.labelPoints.Name = "labelPoints";
            this.labelPoints.Size = new System.Drawing.Size(89, 16);
            this.labelPoints.TabIndex = 2;
            this.labelPoints.Text = "Puncte: 20000";
            this.labelPoints.Click += new System.EventHandler(this.labelPoints_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1775, 746);
            this.Controls.Add(this.labelPoints);
            this.Controls.Add(this.trackBarPoints);
            this.Controls.Add(this.pictureBoxFern);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFern)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPoints)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxFern;
        private System.Windows.Forms.TrackBar trackBarPoints;
        private System.Windows.Forms.Label labelPoints;
    }
}

