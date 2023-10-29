namespace QuadTree.UI
{
    partial class App
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
            numericUpDown1 = new NumericUpDown();
            numericUpDown2 = new NumericUpDown();
            label1 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            label2 = new Label();
            gpsLabel = new Label();
            listView1 = new ListView();
            QuadPanel = new Panel();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            SuspendLayout();
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(12, 65);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(150, 27);
            numericUpDown1.TabIndex = 0;
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(12, 109);
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(150, 27);
            numericUpDown2.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 28);
            label1.Name = "label1";
            label1.Size = new Size(115, 20);
            label1.TabIndex = 2;
            label1.Text = "Size of the Land";
            // 
            // button1
            // 
            button1.Location = new Point(12, 170);
            button1.Name = "button1";
            button1.Size = new Size(150, 29);
            button1.TabIndex = 3;
            button1.Text = "Seed";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(12, 256);
            button2.Name = "button2";
            button2.Size = new Size(150, 34);
            button2.TabIndex = 4;
            button2.Text = "Search for Property";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(12, 327);
            button3.Name = "button3";
            button3.Size = new Size(150, 34);
            button3.TabIndex = 5;
            button3.Text = "Spatial search";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(12, 399);
            button4.Name = "button4";
            button4.Size = new Size(150, 34);
            button4.TabIndex = 6;
            button4.Text = "Add Property";
            button4.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(260, 28);
            label2.Name = "label2";
            label2.Size = new Size(106, 20);
            label2.TabIndex = 7;
            label2.Text = "Objects from : ";
            // 
            // gpsLabel
            // 
            gpsLabel.AutoSize = true;
            gpsLabel.Location = new Point(260, 65);
            gpsLabel.Name = "gpsLabel";
            gpsLabel.Size = new Size(50, 20);
            gpsLabel.TabIndex = 8;
            gpsLabel.Text = "label3";
            // 
            // listView1
            // 
            listView1.Location = new Point(260, 109);
            listView1.Name = "listView1";
            listView1.Size = new Size(162, 324);
            listView1.TabIndex = 10;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // QuadPanel
            // 
            QuadPanel.Location = new Point(686, 28);
            QuadPanel.Name = "QuadPanel";
            QuadPanel.Size = new Size(535, 516);
            QuadPanel.TabIndex = 112;
            // 
            // App
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1258, 679);
            Controls.Add(QuadPanel);
            Controls.Add(listView1);
            Controls.Add(gpsLabel);
            Controls.Add(label2);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(numericUpDown2);
            Controls.Add(numericUpDown1);
            Name = "App";
            Text = "App";
            Load += App_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown numericUpDown1;
        private NumericUpDown numericUpDown2;
        private Label label1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Label label2;
        private Label gpsLabel;
        private ListView listView1;
        private Panel QuadPanel;
    }
}