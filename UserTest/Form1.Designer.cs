namespace UserTest {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.word = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.start = new System.Windows.Forms.ToolStripButton();
            this.submit = new System.Windows.Forms.ToolStripButton();
            this.ans = new System.Windows.Forms.ToolStripLabel();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton4);
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.word);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(779, 596);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // word
            // 
            this.word.AutoSize = true;
            this.word.Dock = System.Windows.Forms.DockStyle.Top;
            this.word.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.word.Location = new System.Drawing.Point(3, 16);
            this.word.Name = "word";
            this.word.Padding = new System.Windows.Forms.Padding(16);
            this.word.Size = new System.Drawing.Size(132, 63);
            this.word.TabIndex = 5;
            this.word.Text = "Word : ";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.start,
            this.submit,
            this.ans});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(779, 26);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // start
            // 
            this.start.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.start.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.start.Image = ((System.Drawing.Image)(resources.GetObject("start.Image")));
            this.start.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(41, 23);
            this.start.Text = "start";
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // submit
            // 
            this.submit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.submit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.submit.Image = ((System.Drawing.Image)(resources.GetObject("submit.Image")));
            this.submit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(55, 23);
            this.submit.Text = "submit";
            this.submit.ToolTipText = "submit";
            this.submit.Click += new System.EventHandler(this.submit_Click);
            // 
            // ans
            // 
            this.ans.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ans.Name = "ans";
            this.ans.Size = new System.Drawing.Size(43, 23);
            this.ans.Text = "ready";
            // 
            // radioButton4
            // 
            this.radioButton4.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButton4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton4.Location = new System.Drawing.Point(3, 463);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Padding = new System.Windows.Forms.Padding(16);
            this.radioButton4.Size = new System.Drawing.Size(773, 128);
            this.radioButton4.TabIndex = 9;
            this.radioButton4.TabStop = true;
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton3.Location = new System.Drawing.Point(3, 335);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Padding = new System.Windows.Forms.Padding(16);
            this.radioButton3.Size = new System.Drawing.Size(773, 128);
            this.radioButton3.TabIndex = 6;
            this.radioButton3.TabStop = true;
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(3, 207);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Padding = new System.Windows.Forms.Padding(16);
            this.radioButton2.Size = new System.Drawing.Size(773, 128);
            this.radioButton2.TabIndex = 7;
            this.radioButton2.TabStop = true;
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(3, 79);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Padding = new System.Windows.Forms.Padding(16);
            this.radioButton1.Size = new System.Drawing.Size(773, 128);
            this.radioButton1.TabIndex = 8;
            this.radioButton1.TabStop = true;
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 596);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(0, 635);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton start;
        private System.Windows.Forms.ToolStripButton submit;
        private System.Windows.Forms.ToolStripLabel ans;
        private System.Windows.Forms.Label word;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}

