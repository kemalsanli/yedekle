
namespace Yedekle
{
    partial class Yedekle
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Yedekle));
            this.backupButton = new System.Windows.Forms.Button();
            this.rollbackButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.clearButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // backupButton
            // 
            this.backupButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.backupButton.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.backupButton.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.backupButton.Location = new System.Drawing.Point(12, 12);
            this.backupButton.Name = "backupButton";
            this.backupButton.Size = new System.Drawing.Size(210, 40);
            this.backupButton.TabIndex = 1;
            this.backupButton.Text = "Yedekle";
            this.backupButton.UseVisualStyleBackColor = false;
            this.backupButton.Click += new System.EventHandler(this.backupButton_Click);
            // 
            // rollbackButton
            // 
            this.rollbackButton.Location = new System.Drawing.Point(229, 12);
            this.rollbackButton.Name = "rollbackButton";
            this.rollbackButton.Size = new System.Drawing.Size(40, 40);
            this.rollbackButton.TabIndex = 2;
            this.rollbackButton.Text = "R";
            this.rollbackButton.UseVisualStyleBackColor = true;
            this.rollbackButton.Click += new System.EventHandler(this.rollbackButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(275, 12);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(40, 40);
            this.clearButton.TabIndex = 4;
            this.clearButton.Text = "C";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // Yedekle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 61);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rollbackButton);
            this.Controls.Add(this.backupButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Yedekle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yedekle v0.1";
            this.Load += new System.EventHandler(this.Yedekle_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button rollbackButton;
        private System.Windows.Forms.Button backupButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button clearButton;
    }
}

