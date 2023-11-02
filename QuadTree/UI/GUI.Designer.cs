namespace QuadTree.UI
{
    partial class GUI
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
            sidePanel = new Panel();
            pnlApp = new Panel();
            pnlTest = new Panel();
            appButton = new Button();
            testButton = new Button();
            upperPanel = new Panel();
            testPanel = new Panel();
            numecricMaxObj = new NumericUpDown();
            numericMaxDepth = new NumericUpDown();
            label7 = new Label();
            label8 = new Label();
            numericLength = new NumericUpDown();
            numericWidth = new NumericUpDown();
            label5 = new Label();
            label6 = new Label();
            panel1 = new Panel();
            richTextBox1 = new RichTextBox();
            goButton = new Button();
            groupBox3 = new GroupBox();
            checkBoxDrawing = new CheckBox();
            checkBoxSeed = new CheckBox();
            checkBoxInterference = new CheckBox();
            groupBox2 = new GroupBox();
            rButtonBoth = new RadioButton();
            rButtonRectangles = new RadioButton();
            rButtonPoints = new RadioButton();
            groupBox1 = new GroupBox();
            rButtonMyQTree = new RadioButton();
            rButtonQtree = new RadioButton();
            deleteNumeric = new NumericUpDown();
            findNumeric = new NumericUpDown();
            insertNumeric = new NumericUpDown();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            sidePanel.SuspendLayout();
            testPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numecricMaxObj).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericMaxDepth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericLength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericWidth).BeginInit();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)deleteNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)findNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)insertNumeric).BeginInit();
            SuspendLayout();
            // 
            // sidePanel
            // 
            sidePanel.BackColor = Color.RosyBrown;
            sidePanel.Controls.Add(pnlApp);
            sidePanel.Controls.Add(pnlTest);
            sidePanel.Controls.Add(appButton);
            sidePanel.Controls.Add(testButton);
            sidePanel.Dock = DockStyle.Left;
            sidePanel.Location = new Point(0, 0);
            sidePanel.Name = "sidePanel";
            sidePanel.Size = new Size(174, 601);
            sidePanel.TabIndex = 0;
            // 
            // pnlApp
            // 
            pnlApp.BackColor = Color.DarkSeaGreen;
            pnlApp.Location = new Point(0, 207);
            pnlApp.Name = "pnlApp";
            pnlApp.Size = new Size(36, 60);
            pnlApp.TabIndex = 2;
            // 
            // pnlTest
            // 
            pnlTest.BackColor = Color.DarkSeaGreen;
            pnlTest.Location = new Point(0, 122);
            pnlTest.Name = "pnlTest";
            pnlTest.Size = new Size(36, 60);
            pnlTest.TabIndex = 0;
            // 
            // appButton
            // 
            appButton.FlatAppearance.BorderSize = 0;
            appButton.FlatStyle = FlatStyle.Flat;
            appButton.Location = new Point(0, 207);
            appButton.Name = "appButton";
            appButton.Size = new Size(174, 60);
            appButton.TabIndex = 1;
            appButton.Text = "A P P";
            appButton.UseVisualStyleBackColor = true;
            // 
            // testButton
            // 
            testButton.FlatAppearance.BorderSize = 0;
            testButton.FlatStyle = FlatStyle.Flat;
            testButton.Location = new Point(0, 122);
            testButton.Name = "testButton";
            testButton.Size = new Size(174, 60);
            testButton.TabIndex = 0;
            testButton.Text = "T E S T";
            testButton.UseVisualStyleBackColor = true;
            // 
            // upperPanel
            // 
            upperPanel.BackColor = Color.DarkSeaGreen;
            upperPanel.Dock = DockStyle.Top;
            upperPanel.Location = new Point(174, 0);
            upperPanel.Name = "upperPanel";
            upperPanel.Size = new Size(926, 55);
            upperPanel.TabIndex = 1;
            // 
            // testPanel
            // 
            testPanel.BackColor = SystemColors.ControlLight;
            testPanel.Controls.Add(numecricMaxObj);
            testPanel.Controls.Add(numericMaxDepth);
            testPanel.Controls.Add(label7);
            testPanel.Controls.Add(label8);
            testPanel.Controls.Add(numericLength);
            testPanel.Controls.Add(numericWidth);
            testPanel.Controls.Add(label5);
            testPanel.Controls.Add(label6);
            testPanel.Controls.Add(panel1);
            testPanel.Controls.Add(richTextBox1);
            testPanel.Controls.Add(goButton);
            testPanel.Controls.Add(groupBox3);
            testPanel.Controls.Add(groupBox2);
            testPanel.Controls.Add(groupBox1);
            testPanel.Controls.Add(deleteNumeric);
            testPanel.Controls.Add(findNumeric);
            testPanel.Controls.Add(insertNumeric);
            testPanel.Controls.Add(label4);
            testPanel.Controls.Add(label3);
            testPanel.Controls.Add(label2);
            testPanel.Controls.Add(label1);
            testPanel.Dock = DockStyle.Right;
            testPanel.Location = new Point(174, 55);
            testPanel.Name = "testPanel";
            testPanel.Size = new Size(926, 546);
            testPanel.TabIndex = 2;
            // 
            // numecricMaxObj
            // 
            numecricMaxObj.Location = new Point(293, 495);
            numecricMaxObj.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            numecricMaxObj.Name = "numecricMaxObj";
            numecricMaxObj.Size = new Size(82, 23);
            numecricMaxObj.TabIndex = 30;
            numecricMaxObj.TextAlign = HorizontalAlignment.Right;
            numecricMaxObj.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // numericMaxDepth
            // 
            numericMaxDepth.Location = new Point(293, 466);
            numericMaxDepth.Name = "numericMaxDepth";
            numericMaxDepth.Size = new Size(82, 23);
            numericMaxDepth.TabIndex = 29;
            numericMaxDepth.TextAlign = HorizontalAlignment.Right;
            numericMaxDepth.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(205, 503);
            label7.Name = "label7";
            label7.Size = new Size(82, 15);
            label7.TabIndex = 28;
            label7.Text = "MAX OBJECTS";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(205, 473);
            label8.Name = "label8";
            label8.Size = new Size(72, 15);
            label8.TabIndex = 27;
            label8.Text = "MAX DEPTH";
            // 
            // numericLength
            // 
            numericLength.Increment = new decimal(new int[] { 50, 0, 0, 0 });
            numericLength.Location = new Point(255, 410);
            numericLength.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numericLength.Name = "numericLength";
            numericLength.Size = new Size(120, 23);
            numericLength.TabIndex = 26;
            numericLength.TextAlign = HorizontalAlignment.Right;
            numericLength.Value = new decimal(new int[] { 500, 0, 0, 0 });
            numericLength.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // numericWidth
            // 
            numericWidth.Increment = new decimal(new int[] { 50, 0, 0, 0 });
            numericWidth.Location = new Point(255, 380);
            numericWidth.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numericWidth.Name = "numericWidth";
            numericWidth.Size = new Size(120, 23);
            numericWidth.TabIndex = 25;
            numericWidth.TextAlign = HorizontalAlignment.Right;
            numericWidth.Value = new decimal(new int[] { 500, 0, 0, 0 });
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(205, 418);
            label5.Name = "label5";
            label5.Size = new Size(51, 15);
            label5.TabIndex = 24;
            label5.Text = "LENGTH";
            label5.Click += label5_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(205, 388);
            label6.Name = "label6";
            label6.Size = new Size(43, 15);
            label6.TabIndex = 23;
            label6.Text = "WIDTH";
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Location = new Point(439, 23);
            panel1.Name = "panel1";
            panel1.Size = new Size(475, 520);
            panel1.TabIndex = 22;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(205, 19);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(216, 350);
            richTextBox1.TabIndex = 21;
            richTextBox1.Text = "";
            // 
            // goButton
            // 
            goButton.FlatAppearance.BorderSize = 0;
            goButton.FlatStyle = FlatStyle.Flat;
            goButton.Location = new Point(20, 495);
            goButton.Name = "goButton";
            goButton.Size = new Size(150, 48);
            goButton.TabIndex = 20;
            goButton.Text = "G O";
            goButton.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(checkBoxDrawing);
            groupBox3.Controls.Add(checkBoxSeed);
            groupBox3.Controls.Add(checkBoxInterference);
            groupBox3.Location = new Point(20, 379);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(150, 110);
            groupBox3.TabIndex = 19;
            groupBox3.TabStop = false;
            groupBox3.Text = "Settings";
            // 
            // checkBoxDrawing
            // 
            checkBoxDrawing.AutoSize = true;
            checkBoxDrawing.Location = new Point(6, 90);
            checkBoxDrawing.Name = "checkBoxDrawing";
            checkBoxDrawing.Size = new Size(69, 19);
            checkBoxDrawing.TabIndex = 18;
            checkBoxDrawing.Text = "drawing";
            checkBoxDrawing.UseVisualStyleBackColor = true;
            // 
            // checkBoxSeed
            // 
            checkBoxSeed.AutoSize = true;
            checkBoxSeed.Location = new Point(6, 60);
            checkBoxSeed.Name = "checkBoxSeed";
            checkBoxSeed.Size = new Size(79, 19);
            checkBoxSeed.TabIndex = 17;
            checkBoxSeed.Text = "fixed seed";
            checkBoxSeed.UseVisualStyleBackColor = true;
            // 
            // checkBoxInterference
            // 
            checkBoxInterference.AutoSize = true;
            checkBoxInterference.Location = new Point(6, 30);
            checkBoxInterference.Name = "checkBoxInterference";
            checkBoxInterference.Size = new Size(125, 19);
            checkBoxInterference.TabIndex = 16;
            checkBoxInterference.Text = "partial interference";
            checkBoxInterference.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(rButtonBoth);
            groupBox2.Controls.Add(rButtonRectangles);
            groupBox2.Controls.Add(rButtonPoints);
            groupBox2.Location = new Point(20, 259);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(150, 110);
            groupBox2.TabIndex = 15;
            groupBox2.TabStop = false;
            groupBox2.Text = "Objects";
            // 
            // rButtonBoth
            // 
            rButtonBoth.AutoSize = true;
            rButtonBoth.Location = new Point(17, 82);
            rButtonBoth.Name = "rButtonBoth";
            rButtonBoth.Size = new Size(50, 19);
            rButtonBoth.TabIndex = 13;
            rButtonBoth.TabStop = true;
            rButtonBoth.Text = "Both";
            rButtonBoth.UseVisualStyleBackColor = true;
            // 
            // rButtonRectangles
            // 
            rButtonRectangles.AutoSize = true;
            rButtonRectangles.Location = new Point(17, 52);
            rButtonRectangles.Name = "rButtonRectangles";
            rButtonRectangles.Size = new Size(82, 19);
            rButtonRectangles.TabIndex = 12;
            rButtonRectangles.TabStop = true;
            rButtonRectangles.Text = "Rectangles";
            rButtonRectangles.UseVisualStyleBackColor = true;
            // 
            // rButtonPoints
            // 
            rButtonPoints.AutoSize = true;
            rButtonPoints.Location = new Point(17, 24);
            rButtonPoints.Name = "rButtonPoints";
            rButtonPoints.Size = new Size(58, 19);
            rButtonPoints.TabIndex = 11;
            rButtonPoints.TabStop = true;
            rButtonPoints.Text = "Points";
            rButtonPoints.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rButtonMyQTree);
            groupBox1.Controls.Add(rButtonQtree);
            groupBox1.Location = new Point(20, 139);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(150, 110);
            groupBox1.TabIndex = 14;
            groupBox1.TabStop = false;
            groupBox1.Text = "Struct";
            // 
            // rButtonMyQTree
            // 
            rButtonMyQTree.AutoSize = true;
            rButtonMyQTree.Location = new Point(7, 56);
            rButtonMyQTree.Name = "rButtonMyQTree";
            rButtonMyQTree.Size = new Size(71, 19);
            rButtonMyQTree.TabIndex = 10;
            rButtonMyQTree.TabStop = true;
            rButtonMyQTree.Text = "MyQTree";
            rButtonMyQTree.UseVisualStyleBackColor = true;
            // 
            // rButtonQtree
            // 
            rButtonQtree.AutoSize = true;
            rButtonQtree.Location = new Point(7, 26);
            rButtonQtree.Name = "rButtonQtree";
            rButtonQtree.Size = new Size(54, 19);
            rButtonQtree.TabIndex = 9;
            rButtonQtree.TabStop = true;
            rButtonQtree.Text = "QTree";
            rButtonQtree.UseVisualStyleBackColor = true;
            // 
            // deleteNumeric
            // 
            deleteNumeric.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            deleteNumeric.Location = new Point(69, 101);
            deleteNumeric.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            deleteNumeric.Name = "deleteNumeric";
            deleteNumeric.Size = new Size(120, 23);
            deleteNumeric.TabIndex = 8;
            deleteNumeric.TextAlign = HorizontalAlignment.Right;
            deleteNumeric.Value = new decimal(new int[] { 10000, 0, 0, 0 });
            // 
            // findNumeric
            // 
            findNumeric.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            findNumeric.Location = new Point(69, 71);
            findNumeric.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            findNumeric.Name = "findNumeric";
            findNumeric.Size = new Size(120, 23);
            findNumeric.TabIndex = 7;
            findNumeric.TextAlign = HorizontalAlignment.Right;
            findNumeric.Value = new decimal(new int[] { 10000, 0, 0, 0 });
            // 
            // insertNumeric
            // 
            insertNumeric.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            insertNumeric.Location = new Point(69, 41);
            insertNumeric.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            insertNumeric.Name = "insertNumeric";
            insertNumeric.Size = new Size(120, 23);
            insertNumeric.TabIndex = 6;
            insertNumeric.TextAlign = HorizontalAlignment.Right;
            insertNumeric.Value = new decimal(new int[] { 10000, 0, 0, 0 });
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 109);
            label4.Name = "label4";
            label4.Size = new Size(45, 15);
            label4.TabIndex = 3;
            label4.Text = "DELETE";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 79);
            label3.Name = "label3";
            label3.Size = new Size(33, 15);
            label3.TabIndex = 2;
            label3.Text = "FIND";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 19);
            label2.Name = "label2";
            label2.Size = new Size(119, 15);
            label2.TabIndex = 1;
            label2.Text = "OPERATIONS COUNT";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 49);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 0;
            label1.Text = "INSERT";
            // 
            // GUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 601);
            Controls.Add(testPanel);
            Controls.Add(upperPanel);
            Controls.Add(sidePanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "GUI";
            Text = "GUI";
            sidePanel.ResumeLayout(false);
            testPanel.ResumeLayout(false);
            testPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numecricMaxObj).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericMaxDepth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericLength).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericWidth).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)deleteNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)findNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)insertNumeric).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel sidePanel;
        private Panel pnlApp;
        private Panel pnlTest;
        private Button appButton;
        private Button testButton;
        private Panel upperPanel;
        private Panel testPanel;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button goButton;
        private GroupBox groupBox3;
        private CheckBox checkBoxDrawing;
        private CheckBox checkBoxSeed;
        private CheckBox checkBoxInterference;
        private GroupBox groupBox2;
        private RadioButton rButtonBoth;
        private RadioButton rButtonRectangles;
        private RadioButton rButtonPoints;
        private GroupBox groupBox1;
        private RadioButton rButtonMyQTree;
        private RadioButton rButtonQtree;
        private NumericUpDown deleteNumeric;
        private NumericUpDown findNumeric;
        private NumericUpDown insertNumeric;
        private RichTextBox richTextBox1;
        private Panel panel1;
        private NumericUpDown numericLength;
        private NumericUpDown numericWidth;
        private Label label5;
        private Label label6;
        private NumericUpDown numecricMaxObj;
        private NumericUpDown numericMaxDepth;
        private Label label7;
        private Label label8;
    }
}