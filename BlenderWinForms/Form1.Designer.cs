namespace BlenderWinForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonSave = new Button();
            buttonRenderProject = new Button();
            treeView1 = new TreeView();
            textBox1 = new TextBox();
            buttonGetAll = new Button();
            dataGridView1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(767, 600);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(79, 32);
            buttonSave.TabIndex = 0;
            buttonSave.Text = "Save Project";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSaveProjectClick;
            // 
            // buttonRenderProject
            // 
            buttonRenderProject.Location = new Point(874, 600);
            buttonRenderProject.Name = "buttonRenderProject";
            buttonRenderProject.Size = new Size(140, 32);
            buttonRenderProject.TabIndex = 1;
            buttonRenderProject.Text = "Render Selected Project";
            buttonRenderProject.UseVisualStyleBackColor = true;
            buttonRenderProject.Click += buttonRender;
            // 
            // treeView1
            // 
            treeView1.Location = new Point(27, 31);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(276, 598);
            treeView1.TabIndex = 2;
            treeView1.AfterSelect += treeView1_AfterSelect;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(375, 39);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(508, 194);
            textBox1.TabIndex = 4;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // buttonGetAll
            // 
            buttonGetAll.Location = new Point(375, 600);
            buttonGetAll.Name = "buttonGetAll";
            buttonGetAll.Size = new Size(78, 29);
            buttonGetAll.TabIndex = 5;
            buttonGetAll.Text = "Get Projects";
            buttonGetAll.UseVisualStyleBackColor = true;
            buttonGetAll.Click += buttonGetProjects;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(375, 265);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(314, 295);
            dataGridView1.TabIndex = 6;
            dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1026, 658);
            Controls.Add(dataGridView1);
            Controls.Add(buttonGetAll);
            Controls.Add(textBox1);
            Controls.Add(treeView1);
            Controls.Add(buttonRenderProject);
            Controls.Add(buttonSave);
            Name = "Form1";
            Text = "l";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonSave;
        private Button buttonRenderProject;
        private TreeView treeView1;
        private TextBox textBox1;
        private Button buttonGetAll;
        private DataGridView dataGridView1;
    }
}
