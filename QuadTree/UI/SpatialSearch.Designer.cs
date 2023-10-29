namespace QuadTree.UI
{
    partial class SpatialSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpatialSearch));
            StartLongitude = new NumericUpDown();
            StartLatitude = new NumericUpDown();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            findButton = new Button();
            label4 = new Label();
            label5 = new Label();
            EndLatitude = new NumericUpDown();
            EndLongitude = new NumericUpDown();
            label6 = new Label();
            label7 = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)StartLongitude).BeginInit();
            ((System.ComponentModel.ISupportInitialize)StartLatitude).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EndLatitude).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EndLongitude).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // StartLongitude
            // 
            StartLongitude.Location = new Point(14, 131);
            StartLongitude.Name = "StartLongitude";
            StartLongitude.Size = new Size(150, 27);
            StartLongitude.TabIndex = 0;
            // 
            // StartLatitude
            // 
            StartLatitude.Location = new Point(14, 223);
            StartLatitude.Name = "StartLatitude";
            StartLatitude.Size = new Size(150, 27);
            StartLatitude.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(118, 9);
            label1.Name = "label1";
            label1.Size = new Size(150, 20);
            label1.TabIndex = 2;
            label1.Text = "Enter the coordinates";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 108);
            label2.Name = "label2";
            label2.Size = new Size(76, 20);
            label2.TabIndex = 3;
            label2.Text = "Longitude";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 200);
            label3.Name = "label3";
            label3.Size = new Size(63, 20);
            label3.TabIndex = 4;
            label3.Text = "Latitude";
            // 
            // findButton
            // 
            findButton.Location = new Point(261, 356);
            findButton.Name = "findButton";
            findButton.Size = new Size(94, 29);
            findButton.TabIndex = 5;
            findButton.Text = "FIND";
            findButton.UseVisualStyleBackColor = true;
            findButton.Click += button1_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(205, 200);
            label4.Name = "label4";
            label4.Size = new Size(63, 20);
            label4.TabIndex = 9;
            label4.Text = "Latitude";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(205, 108);
            label5.Name = "label5";
            label5.Size = new Size(76, 20);
            label5.TabIndex = 8;
            label5.Text = "Longitude";
            // 
            // EndLatitude
            // 
            EndLatitude.Location = new Point(205, 223);
            EndLatitude.Name = "EndLatitude";
            EndLatitude.Size = new Size(150, 27);
            EndLatitude.TabIndex = 7;
            // 
            // EndLongitude
            // 
            EndLongitude.Location = new Point(205, 131);
            EndLongitude.Name = "EndLongitude";
            EndLongitude.Size = new Size(150, 27);
            EndLongitude.TabIndex = 6;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(14, 70);
            label6.Name = "label6";
            label6.Size = new Size(50, 20);
            label6.TabIndex = 10;
            label6.Text = "START";
            label6.Click += label6_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(205, 70);
            label7.Name = "label7";
            label7.Size = new Size(39, 20);
            label7.TabIndex = 11;
            label7.Text = "END";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(12, 269);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(208, 116);
            pictureBox1.TabIndex = 12;
            pictureBox1.TabStop = false;
            // 
            // SpatialSearch
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(376, 404);
            Controls.Add(pictureBox1);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label4);
            Controls.Add(label5);
            Controls.Add(EndLatitude);
            Controls.Add(EndLongitude);
            Controls.Add(findButton);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(StartLatitude);
            Controls.Add(StartLongitude);
            Location = new Point(170, 310);
            Name = "SpatialSearch";
            Text = "SpatialSearch";
            Load += SpatialSearch_Load;
            ((System.ComponentModel.ISupportInitialize)StartLongitude).EndInit();
            ((System.ComponentModel.ISupportInitialize)StartLatitude).EndInit();
            ((System.ComponentModel.ISupportInitialize)EndLatitude).EndInit();
            ((System.ComponentModel.ISupportInitialize)EndLongitude).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown StartLongitude;
        private NumericUpDown StartLatitude;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button findButton;
        private Label label4;
        private Label label5;
        private NumericUpDown EndLatitude;
        private NumericUpDown EndLongitude;
        private Label label6;
        private Label label7;
        private PictureBox pictureBox1;
    }
}