namespace QuadTree.UI
{
    partial class SearchForProperty
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            numericUpDown1 = new NumericUpDown();
            numericUpDown2 = new NumericUpDown();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(98, 9);
            label1.Name = "label1";
            label1.Size = new Size(127, 20);
            label1.TabIndex = 0;
            label1.Text = "Enter Coordinates";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(37, 91);
            label2.Name = "label2";
            label2.Size = new Size(68, 20);
            label2.TabIndex = 1;
            label2.Text = "Logitude";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(37, 167);
            label3.Name = "label3";
            label3.Size = new Size(63, 20);
            label3.TabIndex = 2;
            label3.Text = "Latitude";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(134, 84);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(150, 27);
            numericUpDown1.TabIndex = 3;
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(134, 160);
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(150, 27);
            numericUpDown2.TabIndex = 4;
            // 
            // button1
            // 
            button1.Location = new Point(109, 245);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 5;
            button1.Text = "Find";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // SearchForProperty
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(326, 338);
            Controls.Add(button1);
            Controls.Add(numericUpDown2);
            Controls.Add(numericUpDown1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "SearchForProperty";
            Text = "SearchForProperty";
            Load += SearchForProperty_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private NumericUpDown numericUpDown1;
        private NumericUpDown numericUpDown2;
        private Button button1;
    }
}