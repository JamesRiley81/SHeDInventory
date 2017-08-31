namespace SHED_Inventory
{
    partial class Inventory
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
            this.inventoryListBox = new System.Windows.Forms.CheckedListBox();
            this.deviceLabel = new System.Windows.Forms.Label();
            this.modelLabel = new System.Windows.Forms.Label();
            this.deviceIdLabel = new System.Windows.Forms.Label();
            this.barcodeLabel = new System.Windows.Forms.Label();
            this.categoryComboBox = new System.Windows.Forms.ComboBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.generateButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.inOutLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.previousButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // inventoryListBox
            // 
            this.inventoryListBox.BackColor = System.Drawing.Color.LightGray;
            this.inventoryListBox.Font = new System.Drawing.Font("Arial", 11F);
            this.inventoryListBox.FormattingEnabled = true;
            this.inventoryListBox.Location = new System.Drawing.Point(0, 150);
            this.inventoryListBox.Name = "inventoryListBox";
            this.inventoryListBox.Size = new System.Drawing.Size(497, 270);
            this.inventoryListBox.TabIndex = 1;
            this.inventoryListBox.UseTabStops = false;
            // 
            // deviceLabel
            // 
            this.deviceLabel.AutoSize = true;
            this.deviceLabel.BackColor = System.Drawing.Color.DarkCyan;
            this.deviceLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.deviceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deviceLabel.ForeColor = System.Drawing.Color.Transparent;
            this.deviceLabel.Location = new System.Drawing.Point(3, 124);
            this.deviceLabel.Name = "deviceLabel";
            this.deviceLabel.Size = new System.Drawing.Size(116, 22);
            this.deviceLabel.TabIndex = 2;
            this.deviceLabel.Text = "Device Name";
            // 
            // modelLabel
            // 
            this.modelLabel.AutoSize = true;
            this.modelLabel.BackColor = System.Drawing.Color.DarkCyan;
            this.modelLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.modelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.modelLabel.ForeColor = System.Drawing.Color.Transparent;
            this.modelLabel.Location = new System.Drawing.Point(140, 124);
            this.modelLabel.Name = "modelLabel";
            this.modelLabel.Size = new System.Drawing.Size(74, 22);
            this.modelLabel.TabIndex = 3;
            this.modelLabel.Text = "Model #";
            // 
            // deviceIdLabel
            // 
            this.deviceIdLabel.AutoSize = true;
            this.deviceIdLabel.BackColor = System.Drawing.Color.DarkCyan;
            this.deviceIdLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.deviceIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.deviceIdLabel.ForeColor = System.Drawing.Color.Transparent;
            this.deviceIdLabel.Location = new System.Drawing.Point(240, 124);
            this.deviceIdLabel.Name = "deviceIdLabel";
            this.deviceIdLabel.Size = new System.Drawing.Size(80, 22);
            this.deviceIdLabel.TabIndex = 4;
            this.deviceIdLabel.Text = "Device #";
            // 
            // barcodeLabel
            // 
            this.barcodeLabel.AutoSize = true;
            this.barcodeLabel.BackColor = System.Drawing.Color.DarkCyan;
            this.barcodeLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.barcodeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.barcodeLabel.ForeColor = System.Drawing.Color.Transparent;
            this.barcodeLabel.Location = new System.Drawing.Point(354, 124);
            this.barcodeLabel.Name = "barcodeLabel";
            this.barcodeLabel.Size = new System.Drawing.Size(93, 22);
            this.barcodeLabel.TabIndex = 5;
            this.barcodeLabel.Text = "Barcode #";
            // 
            // categoryComboBox
            // 
            this.categoryComboBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.categoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryComboBox.Font = new System.Drawing.Font("Arial", 14F);
            this.categoryComboBox.FormattingEnabled = true;
            this.categoryComboBox.Items.AddRange(new object[] {
            "2 Hour Laptops",
            "Weekly Laptops",
            "Power Supplies",
            "Headphones",
            "Flash Memory"});
            this.categoryComboBox.Location = new System.Drawing.Point(110, 50);
            this.categoryComboBox.Name = "categoryComboBox";
            this.categoryComboBox.Size = new System.Drawing.Size(232, 30);
            this.categoryComboBox.TabIndex = 6;
            this.categoryComboBox.SelectedIndexChanged += new System.EventHandler(this.categoryComboBox_SelectedIndexChanged);
            this.categoryComboBox.Click += new System.EventHandler(this.categoryComboBox_Click);
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.Color.ForestGreen;
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.saveButton.ForeColor = System.Drawing.Color.Transparent;
            this.saveButton.Location = new System.Drawing.Point(25, 11);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(95, 51);
            this.saveButton.TabIndex = 7;
            this.saveButton.Text = "Save Entries";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // generateButton
            // 
            this.generateButton.BackColor = System.Drawing.Color.DarkCyan;
            this.generateButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.generateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.generateButton.ForeColor = System.Drawing.Color.Transparent;
            this.generateButton.Location = new System.Drawing.Point(191, 11);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(96, 51);
            this.generateButton.TabIndex = 8;
            this.generateButton.Text = "Generate Report";
            this.generateButton.UseVisualStyleBackColor = false;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.DarkCyan;
            this.exitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.exitButton.ForeColor = System.Drawing.Color.Transparent;
            this.exitButton.Location = new System.Drawing.Point(365, 11);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(98, 51);
            this.exitButton.TabIndex = 9;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.DarkCyan;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(82, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(298, 28);
            this.label1.TabIndex = 10;
            this.label1.Text = "Select category to inventory";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SkyBlue;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.inOutLabel);
            this.panel1.Controls.Add(this.deviceLabel);
            this.panel1.Controls.Add(this.categoryComboBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.modelLabel);
            this.panel1.Controls.Add(this.deviceIdLabel);
            this.panel1.Controls.Add(this.barcodeLabel);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(497, 156);
            this.panel1.TabIndex = 12;
            // 
            // inOutLabel
            // 
            this.inOutLabel.BackColor = System.Drawing.Color.DarkCyan;
            this.inOutLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.inOutLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.inOutLabel.ForeColor = System.Drawing.Color.Transparent;
            this.inOutLabel.Location = new System.Drawing.Point(3, 71);
            this.inOutLabel.Name = "inOutLabel";
            this.inOutLabel.Size = new System.Drawing.Size(65, 45);
            this.inOutLabel.TabIndex = 11;
            this.inOutLabel.Text = "In    ☑\r\nOut ☐";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SkyBlue;
            this.panel2.Controls.Add(this.previousButton);
            this.panel2.Controls.Add(this.exitButton);
            this.panel2.Controls.Add(this.generateButton);
            this.panel2.Controls.Add(this.saveButton);
            this.panel2.Location = new System.Drawing.Point(0, 417);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(497, 173);
            this.panel2.TabIndex = 13;
            // 
            // previousButton
            // 
            this.previousButton.BackColor = System.Drawing.Color.DarkCyan;
            this.previousButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.previousButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.previousButton.ForeColor = System.Drawing.Color.Transparent;
            this.previousButton.Location = new System.Drawing.Point(163, 87);
            this.previousButton.Name = "previousButton";
            this.previousButton.Size = new System.Drawing.Size(147, 64);
            this.previousButton.TabIndex = 10;
            this.previousButton.Text = "Generate Previous Report";
            this.previousButton.UseVisualStyleBackColor = false;
            this.previousButton.Click += new System.EventHandler(this.previousButton_Click);
            // 
            // Inventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(494, 580);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.inventoryListBox);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Inventory";
            this.Text = "SHED Inventory";
            this.Load += new System.EventHandler(this.Inventory_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox inventoryListBox;
        private System.Windows.Forms.Label deviceLabel;
        private System.Windows.Forms.Label modelLabel;
        private System.Windows.Forms.Label deviceIdLabel;
        private System.Windows.Forms.Label barcodeLabel;
        private System.Windows.Forms.ComboBox categoryComboBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label inOutLabel;
        private System.Windows.Forms.Button previousButton;
    }
}

