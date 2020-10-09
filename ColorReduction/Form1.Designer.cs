namespace ColorReduction
{
    partial class FormColorReduction
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
            this.pictureBoxSource = new System.Windows.Forms.PictureBox();
            this.pictureBoxSample = new System.Windows.Forms.PictureBox();
            this.pictureBoxResult = new System.Windows.Forms.PictureBox();
            this.buttonBrowseSample = new System.Windows.Forms.Button();
            this.buttonBrowseSource = new System.Windows.Forms.Button();
            this.buttonProcess = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxSourceInfo = new System.Windows.Forms.TextBox();
            this.textBoxSampleInfo = new System.Windows.Forms.TextBox();
            this.progressBarResult = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxSource
            // 
            this.pictureBoxSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxSource.Location = new System.Drawing.Point(13, 66);
            this.pictureBoxSource.Name = "pictureBoxSource";
            this.pictureBoxSource.Size = new System.Drawing.Size(360, 360);
            this.pictureBoxSource.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxSource.TabIndex = 0;
            this.pictureBoxSource.TabStop = false;
            // 
            // pictureBoxSample
            // 
            this.pictureBoxSample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxSample.Location = new System.Drawing.Point(380, 66);
            this.pictureBoxSample.Name = "pictureBoxSample";
            this.pictureBoxSample.Size = new System.Drawing.Size(360, 360);
            this.pictureBoxSample.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxSample.TabIndex = 1;
            this.pictureBoxSample.TabStop = false;
            // 
            // pictureBoxResult
            // 
            this.pictureBoxResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxResult.Location = new System.Drawing.Point(746, 66);
            this.pictureBoxResult.Name = "pictureBoxResult";
            this.pictureBoxResult.Size = new System.Drawing.Size(360, 360);
            this.pictureBoxResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxResult.TabIndex = 2;
            this.pictureBoxResult.TabStop = false;
            // 
            // buttonBrowseSample
            // 
            this.buttonBrowseSample.Location = new System.Drawing.Point(380, 433);
            this.buttonBrowseSample.Name = "buttonBrowseSample";
            this.buttonBrowseSample.Size = new System.Drawing.Size(360, 50);
            this.buttonBrowseSample.TabIndex = 3;
            this.buttonBrowseSample.Text = "Открыть конвертируемое изображение";
            this.buttonBrowseSample.UseVisualStyleBackColor = true;
            this.buttonBrowseSample.Click += new System.EventHandler(this.buttonBrowseSample_Click);
            // 
            // buttonBrowseSource
            // 
            this.buttonBrowseSource.Location = new System.Drawing.Point(13, 433);
            this.buttonBrowseSource.Name = "buttonBrowseSource";
            this.buttonBrowseSource.Size = new System.Drawing.Size(360, 50);
            this.buttonBrowseSource.TabIndex = 4;
            this.buttonBrowseSource.Text = "Открыть изображение - источник палитры цветов";
            this.buttonBrowseSource.UseVisualStyleBackColor = true;
            this.buttonBrowseSource.Click += new System.EventHandler(this.buttonBrowseSource_Click);
            // 
            // buttonProcess
            // 
            this.buttonProcess.Location = new System.Drawing.Point(746, 433);
            this.buttonProcess.Name = "buttonProcess";
            this.buttonProcess.Size = new System.Drawing.Size(360, 50);
            this.buttonProcess.TabIndex = 5;
            this.buttonProcess.Text = "Обработать";
            this.buttonProcess.UseVisualStyleBackColor = true;
            this.buttonProcess.Click += new System.EventHandler(this.buttonProcess_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(746, 489);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(360, 50);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxSourceInfo
            // 
            this.textBoxSourceInfo.Location = new System.Drawing.Point(13, 38);
            this.textBoxSourceInfo.Name = "textBoxSourceInfo";
            this.textBoxSourceInfo.Size = new System.Drawing.Size(360, 22);
            this.textBoxSourceInfo.TabIndex = 7;
            // 
            // textBoxSampleInfo
            // 
            this.textBoxSampleInfo.Location = new System.Drawing.Point(380, 38);
            this.textBoxSampleInfo.Name = "textBoxSampleInfo";
            this.textBoxSampleInfo.Size = new System.Drawing.Size(360, 22);
            this.textBoxSampleInfo.TabIndex = 8;
            // 
            // progressBarResult
            // 
            this.progressBarResult.Location = new System.Drawing.Point(747, 37);
            this.progressBarResult.Name = "progressBarResult";
            this.progressBarResult.Size = new System.Drawing.Size(359, 23);
            this.progressBarResult.TabIndex = 9;
            // 
            // FormColorReduction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1126, 546);
            this.Controls.Add(this.progressBarResult);
            this.Controls.Add(this.textBoxSampleInfo);
            this.Controls.Add(this.textBoxSourceInfo);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonProcess);
            this.Controls.Add(this.buttonBrowseSource);
            this.Controls.Add(this.buttonBrowseSample);
            this.Controls.Add(this.pictureBoxResult);
            this.Controls.Add(this.pictureBoxSample);
            this.Controls.Add(this.pictureBoxSource);
            this.Name = "FormColorReduction";
            this.Text = "ColorReduction";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxSource;
        private System.Windows.Forms.PictureBox pictureBoxSample;
        private System.Windows.Forms.PictureBox pictureBoxResult;
        private System.Windows.Forms.Button buttonBrowseSample;
        private System.Windows.Forms.Button buttonBrowseSource;
        private System.Windows.Forms.Button buttonProcess;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxSourceInfo;
        private System.Windows.Forms.TextBox textBoxSampleInfo;
        private System.Windows.Forms.ProgressBar progressBarResult;
    }
}

