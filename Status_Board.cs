using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DBLIB;
using System.Configuration;

namespace Barcode
{
    public partial class Status_Board : Form
    {
        clsDBLib db = new clsDBLib();
        Print_Barcode pb = new Print_Barcode();
        string startDate;
        string endDate;
        string mainBarcodeInfo;
        DataTable dt;
        DataSet ds = new DataSet();
        string today;
        // 바코드 번호
        string MB;
        string SB1;
        string SB2;
        List<string> barcode_List = new List<string>();
        // 메모
        string remark;
        // 발행인
        string createUser;

        public Status_Board()
        {
            InitializeComponent();
        }

        private void Status_Board_Load(object sender, EventArgs e)
        {
            today = System.DateTime.Now.ToString("yyyyMMdd");
            txt_StartDate.Text = today;
            txt_EndDate.Text = today;
            txt_MB.Text = string.Empty;
        }

        /// <summary>
        /// 신규 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_New_Click_1(object sender, EventArgs e)
        {
            // 날짜 텍스트박스 값 오늘 날짜로 초기화
            txt_StartDate.Text = today;
            txt_EndDate.Text = today;
            // 메인바코드 번호 텍스트박스 값 초기화
            txt_MB.Text = string.Empty;
            // 리스트 초기화
            listView1.Items.Clear();
        }

        private void btn_LookUp_Click(object sender, EventArgs e)
        {
            // 날짜 텍스트박스 값 가져오기
            startDate = txt_StartDate.Text.Replace("-", "");
            endDate = txt_EndDate.Text.Replace("-", "");
            // 메인바코드번호 텍스트박스 값 가져오기
            mainBarcodeInfo = txt_MB.Text;
            ds = LookUP_Barcode_Info(startDate, endDate, mainBarcodeInfo);
            dt = ds.Tables["DATA"];
            // 리스트뷰 초기화
            listView1.Items.Clear();

            /* 이걸로 써도 됨(dataTable에 데이터를 한줄로 만들어서 2중배열로 만들어줘도 됨)
             * string[] List;
            
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                string new_Line = Convert.ToString(listView1.Items.Count + 1);
                new_Line += "^" + dt.Rows[i]["CREATE_DATE"].ToString();
                new_Line += "^" + dt.Rows[i]["MBARCODEINFO"].ToString();
                new_Line += "^" + dt.Rows[i]["SBARCODEINFO"].ToString();
                new_Line += "^" + dt.Rows[i]["REMARK"].ToString();
                new_Line += "^" + dt.Rows[i]["CONDATE"].ToString();
                new_Line += "^" + dt.Rows[i]["CREATE_USER"].ToString();
                new_Line += "^" + dt.Rows[i]["CREATE_TIME"].ToString();
                MessageBox.Show(new_Line);
                List = new_Line.Split('^');
                
                listView1.Items.Add(new ListViewItem(List));
            }*/

            // 위에꺼 대신 사용 한줄씩 리스트뷰에 데이터만큼 반복 추가
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.Text = (listView1.Items.Count + 1).ToString();
                string MB = dt.Rows[i]["MBARCODEINFO"].ToString();
                string SB = dt.Rows[i]["SBARCODEINFO"].ToString();
                string CD = dt.Rows[i]["CREATE_DATE"].ToString();

                item.SubItems.Add(dt.Rows[i]["CREATE_DATE"].ToString());
                item.SubItems.Add(dt.Rows[i]["MBARCODEINFO"].ToString());
                item.SubItems.Add(dt.Rows[i]["SBARCODEINFO"].ToString());
                item.SubItems.Add(dt.Rows[i]["REMARK"].ToString());
                item.SubItems.Add(dt.Rows[i]["CONDATE"].ToString());
                item.SubItems.Add(dt.Rows[i]["CREATE_USER"].ToString());
                item.SubItems.Add(dt.Rows[i]["CREATE_TIME"].ToString());

                listView1.Items.Add(item);
            }
            
        }

        private DataSet LookUP_Barcode_Info(string startDate, string endDate, string mainBarcodeInfo)
        {
            string sQuery = "";

            sQuery = "EXEC SP_BARCODE_R @STARTDATE, @ENDDATE, @MAINBARCODEINFO";

            SqlParameter[] sPrm = new SqlParameter[3]
            {
                  new SqlParameter("@STARTDATE", startDate)
                , new SqlParameter("@ENDDATE", endDate)
                , new SqlParameter("@MAINBARCODEINFO", mainBarcodeInfo)
            };

            ds = db.SelectQueryDataset(sQuery, sPrm);

            return ds;
        }

        /// <summary>
        /// 리스트에서 줄 선택 시 재발행 정보 확인 및 재발행
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                MB = listView1.Items[listView1.SelectedItems[0].Index].SubItems[2].Text;
                remark = listView1.Items[listView1.SelectedItems[0].Index].SubItems[4].Text;
                createUser = listView1.Items[listView1.SelectedItems[0].Index].SubItems[6].Text;
                string SB = listView1.Items[listView1.SelectedItems[0].Index].SubItems[3].Text;
                barcode_List = new List<string>();
                barcode_List.Add(SB);
                
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (MB == listView1.Items[i].SubItems[2].Text && SB != listView1.Items[i].SubItems[3].Text)
                    {
                        SB = listView1.Items[i].SubItems[3].Text;
                        barcode_List.Add(SB);
                    }
                }
                barcode_List.Sort();

                //barcode_List.Sort();

                SB1 = barcode_List[0];
                SB2 = barcode_List[1];

                if (MessageBox.Show("바코드 정보" +
                                    "\r\nMB : " + MB + 
                                    "\r\nSB1 : " + SB1 + 
                                    "\r\nSB2 : " + SB2 +
                                    "\r\n메모 : " + remark + 
                                    "\r\n발행인 : " + createUser + 
                                    "\r\n\r\n재발행 하시겠습니까?", "알림", MessageBoxButtons.YesNo) == DialogResult.Yes)

                {
                    //MessageBox.Show("MB = " + MB + "\r\nSB1 = " + SB1 + "\r\nSB2  = " + SB2 + "\r\nremark = " + remark + "\r\ncreateUser = " + createUser);

                    PrintDocument pd = new PrintDocument();

                    string printer = ConfigurationManager.AppSettings["DEFAULTPRINTER"];
                    // 인쇄 페이지정보 팝업창(프린터 선택, 인쇄 매수, 인쇄범위 등 지정)
                    //PrintDialog printDialog = new PrintDialog();
                    //printDialog.AllowSomePages = true;
                    //printDialog.Document = pd;

                    // 프린터 선택
                    pd.PrinterSettings.PrinterName = printer;

                    // 인쇄 시 페이지정보 팝업창 뜨지않게 설정
                    pd.PrintController = new System.Drawing.Printing.StandardPrintController();
                    pd.PrinterSettings.Copies = Convert.ToInt16(1);

                    // Print() 메서드가 호출될 때 문서의 첫 페이지가 인쇄되기 전에 발생
                    pd.BeginPrint += new PrintEventHandler(pd_BeginPrint);
                    // 문서의 마지막 페이지가 인쇄되면 발생
                    pd.EndPrint += new PrintEventHandler(pd_EndPrint);
                    // 현재 페이지에 대해 인쇄할 출력이 필요할 때 발생
                    pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                    pd.DocumentName = "바코드 출력";

                    //DialogResult result = printDialog.ShowDialog();

                    //if (result == DialogResult.OK)
                    //{
                    pd.Print();
                    //}
                }
            }
            else
            {
                return;
            }
            
        }

        private void pd_BeginPrint(object sender, EventArgs ev)
        {

        }

        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            Brush brush = new SolidBrush(Color.Black);
            PictureBox pictureBox1 = new PictureBox();   // QRCode 이미지 넣을 컨트롤
            PictureBox pictureBox2 = new PictureBox();   // DataMatrix 이미지 넣을 컨트롤

            try
            {
                // Create rectangle for drawing.
                Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
                Image img1 = new Bitmap(100, 100);
                Image img2 = new Bitmap(100, 100);
                Image img3 = Image.FromFile("C:\\Users\\seol\\Desktop\\seol\\mini1.jpg");

                StringFormat drawFormat = new StringFormat();
                drawFormat.Alignment = StringAlignment.Center;

                StringFormat drawFormat_2 = new StringFormat();
                drawFormat_2.Alignment = StringAlignment.Near;

                // QR코드 이미지 생성
                System.Drawing.Graphics ga1;
                ga1 = Graphics.FromImage(img1);
                img1 = pb.RenderQrCode(SB1);
                ga1.DrawImage(img1, 0, 0);
                pictureBox1.Image = img1;

                // DataMatrix 이미지 생성
                System.Drawing.Graphics ga2;
                ga2 = Graphics.FromImage(img2);
                img2 = pb.RenderDataMatrix(SB2);
                ga2.DrawImage(img2, 0, 0);
                pictureBox2.Image = img2;

                ///// 가로줄 /////
                ev.Graphics.DrawLine(pen, 5, 10, 380, 10);
                ev.Graphics.DrawLine(pen, 5, 40, 380, 40);
                ev.Graphics.DrawLine(pen, 100, 100, 380, 100);
                ev.Graphics.DrawLine(pen, 5, 130, 380, 130);
                ev.Graphics.DrawLine(pen, 5, 220, 380, 220);

                ///// 세로줄 /////
                ev.Graphics.DrawLine(pen, 5, 10, 5, 220);
                ev.Graphics.DrawLine(pen, 100, 40, 100, 130);
                ev.Graphics.DrawLine(pen, 195, 130, 195, 220);
                ev.Graphics.DrawLine(pen, 380, 10, 380, 220);

                ///// LOT ID 타원형 테두리 /////
                ev.Graphics.DrawEllipse(pen, 10, 65, 85, 35);

                ///// 텍스트, 바코드 넣을 공간 만들기 /////
                RectangleF drawRect1 = new RectangleF(5, 13, 380, 20);      // RUN SHEET
                RectangleF drawRect2 = new RectangleF(5, 70, 95, 30);       // LOT ID
                RectangleF drawRect3 = new RectangleF(100, 45, 285, 50);    // 메인바코드 이미지
                RectangleF drawRect4 = new RectangleF(100, 103, 285, 20);   // 메인바코드 정보
                RectangleF drawRect5 = new RectangleF(75, 140, 50, 50);     // 서브 QR코드 이미지
                RectangleF drawRect6 = new RectangleF(5, 200, 190, 15);     // 서브 QR코드 정보
                RectangleF drawRect7 = new RectangleF(265, 140, 50, 50);   // 서브 DataMatrix 이미지
                RectangleF drawRect8 = new RectangleF(195, 200, 190, 15);   // 정보
                RectangleF drawRect9 = new RectangleF(5, 240, 200, 200);    // 이미지


                ///// 데이터 /////
                ev.Graphics.DrawString("RUN SHEET", new Font("Arial", 15, FontStyle.Bold), brush, drawRect1, drawFormat);
                ev.Graphics.DrawString("LOT ID", new Font("Arial", 15, FontStyle.Bold), brush, drawRect2, drawFormat);
                ev.Graphics.DrawString(MB, new Font("CODE 128", 30, FontStyle.Regular), brush, drawRect3, drawFormat);
                ev.Graphics.DrawString(MB, new Font("Arial", 15, FontStyle.Bold), brush, drawRect4, drawFormat);
                ev.Graphics.DrawImage(img1, drawRect5);
                ev.Graphics.DrawString(SB1, new Font("Arial", 10, FontStyle.Regular), brush, drawRect6, drawFormat);
                ev.Graphics.DrawImage(img2, drawRect7);
                ev.Graphics.DrawString(SB2, new Font("Arial", 10, FontStyle.Regular), brush, drawRect8, drawFormat);
                ev.Graphics.DrawImage(img3, drawRect9);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void pd_EndPrint(object sender, EventArgs ev)
        {

        }



    }
}
