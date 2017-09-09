namespace DGVC_UI
{
    partial class FormWelcome
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.sign_in = new System.Windows.Forms.Button();
            this.label_UsersCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // sign_in
            // 
            this.sign_in.AutoSize = true;
            this.sign_in.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sign_in.Location = new System.Drawing.Point(537, 12);
            this.sign_in.Name = "sign_in";
            this.sign_in.Size = new System.Drawing.Size(75, 34);
            this.sign_in.TabIndex = 0;
            this.sign_in.Text = "Войти";
            this.sign_in.UseVisualStyleBackColor = true;
            // 
            // label_UsersCount
            // 
            this.label_UsersCount.AutoSize = true;
            this.label_UsersCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_UsersCount.ForeColor = System.Drawing.Color.LightGray;
            this.label_UsersCount.Location = new System.Drawing.Point(12, 49);
            this.label_UsersCount.Name = "label_UsersCount";
            this.label_UsersCount.Size = new System.Drawing.Size(442, 24);
            this.label_UsersCount.TabIndex = 5;
            this.label_UsersCount.Text = "Зарегестрированно пользователей: Загрузка...";
            // 
            // FormWelcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.label_UsersCount);
            this.Controls.Add(this.sign_in);
            this.Name = "FormWelcome";
            this.Text = "Welcome to the DGVC!!!";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sign_in;
        private System.Windows.Forms.Label label_UsersCount;
    }
}

