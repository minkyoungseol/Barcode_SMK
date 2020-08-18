namespace Barcode
{
    partial class Status_Board
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
            this.txt_StartDate = new System.Windows.Forms.MaskedTextBox();
            this.txt_EndDate = new System.Windows.Forms.MaskedTextBox();
            this.btn_New = new System.Windows.Forms.Button();
            this.btn_LookUp = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_MB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CREATE_DATE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MBARCODEINFO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SBARCODEINFO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.REMARK = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CONDATE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CREATE_USER = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CREATE_TIME = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // txt_StartDate
            // 
            this.txt_StartDate.Location = new System.Drawing.Point(71, 25);
            this.txt_StartDate.Mask = "0000-00-00";
            this.txt_StartDate.Name = "txt_StartDate";
            this.txt_StartDate.Size = new System.Drawing.Size(100, 21);
            this.txt_StartDate.TabIndex = 1;
            this.txt_StartDate.ValidatingType = typeof(System.DateTime);
            // 
            // txt_EndDate
            // 
            this.txt_EndDate.Location = new System.Drawing.Point(197, 25);
            this.txt_EndDate.Mask = "0000-00-00";
            this.txt_EndDate.Name = "txt_EndDate";
            this.txt_EndDate.Size = new System.Drawing.Size(100, 21);
            this.txt_EndDate.TabIndex = 2;
            this.txt_EndDate.ValidatingType = typeof(System.DateTime);
            // 
            // btn_New
            // 
            this.btn_New.Location = new System.Drawing.Point(670, 23);
            this.btn_New.Name = "btn_New";
            this.btn_New.Size = new System.Drawing.Size(75, 23);
            this.btn_New.TabIndex = 3;
            this.btn_New.Text = "신규";
            this.btn_New.UseVisualStyleBackColor = true;
            this.btn_New.Click += new System.EventHandler(this.btn_New_Click_1);
            // 
            // btn_LookUp
            // 
            this.btn_LookUp.Location = new System.Drawing.Point(751, 23);
            this.btn_LookUp.Name = "btn_LookUp";
            this.btn_LookUp.Size = new System.Drawing.Size(75, 23);
            this.btn_LookUp.TabIndex = 4;
            this.btn_LookUp.Text = "조회";
            this.btn_LookUp.UseVisualStyleBackColor = true;
            this.btn_LookUp.Click += new System.EventHandler(this.btn_LookUp_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(177, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "~";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "발행일";
            // 
            // txt_MB
            // 
            this.txt_MB.Location = new System.Drawing.Point(423, 25);
            this.txt_MB.Name = "txt_MB";
            this.txt_MB.Size = new System.Drawing.Size(221, 21);
            this.txt_MB.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(324, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "메인바코드 번호";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.CREATE_DATE,
            this.MBARCODEINFO,
            this.SBARCODEINFO,
            this.REMARK,
            this.CONDATE,
            this.CREATE_USER,
            this.CREATE_TIME});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.HoverSelection = true;
            this.listView1.Location = new System.Drawing.Point(15, 63);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(811, 375);
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "*";
            this.columnHeader1.Width = 30;
            // 
            // CREATE_DATE
            // 
            this.CREATE_DATE.Text = "CREATE_DATE";
            this.CREATE_DATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CREATE_DATE.Width = 100;
            // 
            // MBARCODEINFO
            // 
            this.MBARCODEINFO.Text = "MBARCODEINFO";
            this.MBARCODEINFO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MBARCODEINFO.Width = 120;
            // 
            // SBARCODEINFO
            // 
            this.SBARCODEINFO.Text = "SBARCODEINFO";
            this.SBARCODEINFO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.SBARCODEINFO.Width = 120;
            // 
            // REMARK
            // 
            this.REMARK.Text = "REMARK";
            this.REMARK.Width = 100;
            // 
            // CONDATE
            // 
            this.CONDATE.Text = "CONDATE";
            this.CONDATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CONDATE.Width = 100;
            // 
            // CREATE_USER
            // 
            this.CREATE_USER.Text = "CREATE_USER";
            this.CREATE_USER.Width = 120;
            // 
            // CREATE_TIME
            // 
            this.CREATE_TIME.Text = "CREATE_TIME";
            this.CREATE_TIME.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CREATE_TIME.Width = 120;
            // 
            // Status_Board
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 450);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_MB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_LookUp);
            this.Controls.Add(this.btn_New);
            this.Controls.Add(this.txt_EndDate);
            this.Controls.Add(this.txt_StartDate);
            this.Name = "Status_Board";
            this.Text = "발행이력";
            this.Load += new System.EventHandler(this.Status_Board_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MaskedTextBox txt_StartDate;
        private System.Windows.Forms.MaskedTextBox txt_EndDate;
        private System.Windows.Forms.Button btn_New;
        private System.Windows.Forms.Button btn_LookUp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_MB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader CREATE_DATE;
        private System.Windows.Forms.ColumnHeader MBARCODEINFO;
        private System.Windows.Forms.ColumnHeader SBARCODEINFO;
        private System.Windows.Forms.ColumnHeader REMARK;
        private System.Windows.Forms.ColumnHeader CONDATE;
        private System.Windows.Forms.ColumnHeader CREATE_USER;
        private System.Windows.Forms.ColumnHeader CREATE_TIME;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}