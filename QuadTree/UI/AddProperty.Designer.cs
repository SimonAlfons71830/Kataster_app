namespace QuadTree.UI
{
    partial class AddProperty
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
            label4 = new Label();
            registerNumberNum = new NumericUpDown();
            descriptionText = new TextBox();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            button1 = new Button();
            startX = new NumericUpDown();
            startY = new NumericUpDown();
            endY = new NumericUpDown();
            endX = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)registerNumberNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)startX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)startY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)endY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)endX).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(159, 9);
            label1.Name = "label1";
            label1.Size = new Size(114, 20);
            label1.TabIndex = 0;
            label1.Text = "NEW PROPERTY";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 73);
            label2.Name = "label2";
            label2.Size = new Size(114, 20);
            label2.TabIndex = 1;
            label2.Text = "register number";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 125);
            label3.Name = "label3";
            label3.Size = new Size(83, 20);
            label3.TabIndex = 2;
            label3.Text = "description";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 188);
            label4.Name = "label4";
            label4.Size = new Size(87, 20);
            label4.TabIndex = 3;
            label4.Text = "coordinates";
            // 
            // registerNumberNum
            // 
            registerNumberNum.Location = new Point(148, 66);
            registerNumberNum.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            registerNumberNum.Name = "registerNumberNum";
            registerNumberNum.Size = new Size(150, 27);
            registerNumberNum.TabIndex = 4;
            // 
            // descriptionText
            // 
            descriptionText.Location = new Point(148, 122);
            descriptionText.Name = "descriptionText";
            descriptionText.Size = new Size(150, 27);
            descriptionText.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 227);
            label5.Name = "label5";
            label5.Size = new Size(50, 20);
            label5.TabIndex = 6;
            label5.Text = "START";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(221, 227);
            label6.Name = "label6";
            label6.Size = new Size(39, 20);
            label6.TabIndex = 7;
            label6.Text = "END";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 286);
            label7.Name = "label7";
            label7.Size = new Size(73, 20);
            label7.TabIndex = 8;
            label7.Text = "longitude";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(12, 326);
            label8.Name = "label8";
            label8.Size = new Size(60, 20);
            label8.TabIndex = 9;
            label8.Text = "latitude";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(221, 326);
            label9.Name = "label9";
            label9.Size = new Size(60, 20);
            label9.TabIndex = 15;
            label9.Text = "latitude";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(221, 286);
            label10.Name = "label10";
            label10.Size = new Size(73, 20);
            label10.TabIndex = 14;
            label10.Text = "longitude";
            // 
            // button1
            // 
            button1.Location = new Point(159, 402);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 18;
            button1.Text = "add";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // startX
            // 
            startX.Location = new Point(91, 284);
            startX.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            startX.Name = "startX";
            startX.Size = new Size(117, 27);
            startX.TabIndex = 19;
            // 
            // startY
            // 
            startY.Location = new Point(91, 319);
            startY.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            startY.Name = "startY";
            startY.Size = new Size(117, 27);
            startY.TabIndex = 20;
            // 
            // endY
            // 
            endY.Location = new Point(305, 319);
            endY.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            endY.Name = "endY";
            endY.Size = new Size(117, 27);
            endY.TabIndex = 22;
            // 
            // endX
            // 
            endX.Location = new Point(305, 284);
            endX.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            endX.Name = "endX";
            endX.Size = new Size(117, 27);
            endX.TabIndex = 21;
            // 
            // AddProperty
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 466);
            Controls.Add(endY);
            Controls.Add(endX);
            Controls.Add(startY);
            Controls.Add(startX);
            Controls.Add(button1);
            Controls.Add(label9);
            Controls.Add(label10);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(descriptionText);
            Controls.Add(registerNumberNum);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "AddProperty";
            Text = "AddProperty";
            Load += AddProperty_Load;
            ((System.ComponentModel.ISupportInitialize)registerNumberNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)startX).EndInit();
            ((System.ComponentModel.ISupportInitialize)startY).EndInit();
            ((System.ComponentModel.ISupportInitialize)endY).EndInit();
            ((System.ComponentModel.ISupportInitialize)endX).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private NumericUpDown registerNumberNum;
        private TextBox descriptionText;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Button button1;
        private NumericUpDown startX;
        private NumericUpDown startY;
        private NumericUpDown endY;
        private NumericUpDown endX;
    }
}