namespace Barcode
{
    partial class Print_Barcode
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Print = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_CreateUser = new System.Windows.Forms.TextBox();
            this.txt_Remark = new System.Windows.Forms.TextBox();
            this.txt_ConDate = new System.Windows.Forms.MaskedTextBox();
            this.btn_Status = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.combo_Printer = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btn_Print
            // 
            this.btn_Print.Location = new System.Drawing.Point(116, 302);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(102, 27);
            this.btn_Print.TabIndex = 4;
            this.btn_Print.Text = "print";
            this.btn_Print.UseVisualStyleBackColor = true;
            this.btn_Print.Click += new System.EventHandler(this.btn_Print_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(388, 27);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(268, 412);
            this.listBox1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "발행일";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 202);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "발행인";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(65, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "메모";
            // 
            // txt_CreateUser
            // 
            this.txt_CreateUser.Location = new System.Drawing.Point(154, 199);
            this.txt_CreateUser.Name = "txt_CreateUser";
            this.txt_CreateUser.Size = new System.Drawing.Size(121, 21);
            this.txt_CreateUser.TabIndex = 10;
            // 
            // txt_Remark
            // 
            this.txt_Remark.Location = new System.Drawing.Point(154, 142);
            this.txt_Remark.Name = "txt_Remark";
            this.txt_Remark.Size = new System.Drawing.Size(121, 21);
            this.txt_Remark.TabIndex = 11;
            // 
            // txt_ConDate
            // 
            this.txt_ConDate.Location = new System.Drawing.Point(154, 85);
            this.txt_ConDate.Mask = "0000-00-00";
            this.txt_ConDate.Name = "txt_ConDate";
            this.txt_ConDate.ResetOnSpace = false;
            this.txt_ConDate.Size = new System.Drawing.Size(121, 21);
            this.txt_ConDate.TabIndex = 12;
            this.txt_ConDate.ValidatingType = typeof(System.DateTime);
            // 
            // btn_Status
            // 
            this.btn_Status.Location = new System.Drawing.Point(12, 403);
            this.btn_Status.Name = "btn_Status";
            this.btn_Status.Size = new System.Drawing.Size(362, 36);
            this.btn_Status.TabIndex = 13;
            this.btn_Status.Text = "출력 현황 조회";
            this.btn_Status.UseVisualStyleBackColor = true;
            this.btn_Status.Click += new System.EventHandler(this.btn_Status_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(65, 258);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "프린터 선택";
            // 
            // combo_Printer
            // 
            this.combo_Printer.FormattingEnabled = true;
            this.combo_Printer.Location = new System.Drawing.Point(154, 255);
            this.combo_Printer.Name = "combo_Printer";
            this.combo_Printer.Size = new System.Drawing.Size(121, 20);
            this.combo_Printer.TabIndex = 15;
            // 
            // Print_Barcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 463);
            this.Controls.Add(this.combo_Printer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_Status);
            this.Controls.Add(this.txt_ConDate);
            this.Controls.Add(this.txt_Remark);
            this.Controls.Add(this.txt_CreateUser);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btn_Print);
            this.Name = "Print_Barcode";
            this.Text = "바코드";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_Print;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_CreateUser;
        private System.Windows.Forms.TextBox txt_Remark;
        private System.Windows.Forms.MaskedTextBox txt_ConDate;
        private System.Windows.Forms.Button btn_Status;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox combo_Printer;
    }
}

