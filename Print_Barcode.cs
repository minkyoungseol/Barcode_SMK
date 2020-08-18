using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DBLIB;
using OnBarcode.Barcode;
using QRCoder;

namespace Barcode
{
    public partial class Print_Barcode : Form
    {        
        private Graphics g;
        // DB사용
        clsDBLib db = new clsDBLib();
        // 바코드 번호
        List<string> barcode_List = new List<string>();
        string MB;
        string SB1;
        string SB2;
        // 발행일
        string conDate;
        // 메모
        string remark;
        // 발행인
        string createUser;

        public Print_Barcode()
        {
            InitializeComponent();
            App_Main.Read_INI(); // App.config 설정 파일 읽어서 DB 열기
        }       

        private void Form1_Load(object sender, EventArgs e)
        {
            string defaultPrinter = ConfigurationManager.AppSettings["DEFAULTPRINTER"];
            
            conDate = System.DateTime.Now.ToString("yyyyMMdd");
            txt_ConDate.Text = conDate;
            PrinterSettings ps = new PrinterSettings();
            foreach(string printer in PrinterSettings.InstalledPrinters)
            {
                ps.PrinterName = printer;
                combo_Printer.Items.Add(printer);
                
                combo_Printer.SelectedItem = defaultPrinter;
                /*
                if (ps.IsDefaultPrinter)
                    defaultPrinter = printer;*/
            }

        }

        private void btn_Print_Click(object sender, EventArgs e)
        {   
            // 인쇄버튼 누르면 바로 실행(생성일 DB전달해서 채번, 바코드리스트에 담기 / 발행인, 메모 저장)
            conDate = txt_ConDate.Text.Replace("-", "");
            MB = Create_BarcodeNum("MB", conDate);
            SB1 = Create_BarcodeNum("SB", conDate);
            SB2 = Create_BarcodeNum("SB", conDate);

            barcode_List = new List<string>();
            barcode_List.Add(SB1);
            barcode_List.Add(SB2);
            
            remark = txt_Remark.Text;
            createUser = txt_CreateUser.Text;

            /// 메모, 발행인 빈칸 검증
            if(remark.Trim().Length == 0)
            {
                MessageBox.Show("메모를 입력해주세요!", "알림");
                return;
            }else if (createUser.Trim().Length == 0)
            {
                MessageBox.Show("발행인을 입력해주세요!", "알림");
                return;
            }
            
            string printer = combo_Printer.Text;
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["DEFAULTPRINTER"].Value = printer;
                config.Save(ConfigurationSaveMode.Modified);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            PrintDocument pd = new PrintDocument();


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

            pd.Print();

            //DialogResult result = printDialog.ShowDialog();
            //if(result == DialogResult.OK)
            //{
            //    pd.Print();
            //}
        }

        private void pd_BeginPrint(object sender, EventArgs e)
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
                img1 = RenderQrCode(SB1);
                ga1.DrawImage(img1, 0, 0);
                pictureBox1.Image = img1;

                // DataMatrix 이미지 생성
                System.Drawing.Graphics ga2;
                ga2 = Graphics.FromImage(img2);
                img2 = RenderDataMatrix(SB2);
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

        /// <summary>
        ///  QR코드 데이터 받아서 이미지로 변환
        /// </summary>
        /// <param name="data"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        public Image RenderQrCode(string data, Bitmap icon = null)
        {
            Image img;
            string level = "M";
            int iconsize = 25;
            QRCodeGenerator.ECCLevel eccLevel = (QRCodeGenerator.ECCLevel)(level == "L" ? 0 : level == "M" ? 1 : level == "Q" ? 2 : 3);
            using (QRCodeGenerator qrGenerater = new QRCodeGenerator())
            {
                using (QRCodeData qrCodeData = qrGenerater.CreateQrCode(data, eccLevel))
                {
                    using (QRCoder.QRCode qrCode = new QRCoder.QRCode(qrCodeData))
                    {
                        img = qrCode.GetGraphic(18, Color.Black, Color.White, icon, iconsize, 6, false);
                    }
                }
            }
            return img;
        }

        /// <summary>
        /// Datamatrix 데이터 받아서 이미지로 변환
        /// </summary>
        /// <param name="data"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        public Image RenderDataMatrix(string data, Bitmap icon = null)
        {
            Image img2;
            DataMatrix datamatrix = new DataMatrix();
            // Barcode data to encode
            datamatrix.Data = data;
            // Data Matrix data mode
            datamatrix.DataMode = DataMatrixDataMode.ASCII;
            // Data Matrix format mode
            datamatrix.FormatMode = DataMatrixFormatMode.Format_16X16;
            /*
             * Barcode Image Related Settings
            */
            // Unit of meature for all size related setting in the library. 
            datamatrix.UOM = UnitOfMeasure.PIXEL;
            // Bar module size (X), default is 3 pixel;
            datamatrix.X = 3;
            // Barcode image left, right, top, bottom margins. Defaults are 0.
            datamatrix.LeftMargin = 0;
            datamatrix.RightMargin = 0;
            datamatrix.TopMargin = 0;
            datamatrix.BottomMargin = 0;
            // Image resolution in dpi, default is 72 dpi.
            datamatrix.Resolution = 72;
            // Created barcode orientation. 
            //4 options are: facing left, facing right, facing bottom, and facing top
            datamatrix.Rotate = Rotate.Rotate0;

            // Generate data matrix and encode barcode to gif format
            // datamatrix.ImageFormat = System.Drawing.Imaging.ImageFormat.Gif;
            img2 = datamatrix.drawBarcode();

            return img2;
        }

        private void pd_EndPrint(object sender, EventArgs e)
        {
            listBox1.Items.Add("출력시간 : " + DateTime.Now.ToString());
            listBox1.Items.Add("바코드 NO : " + MB);                                                   // 리스트 박스에 메인 바코드 정보 출력
            Insert_MBarcode_Info(MB, remark, conDate, createUser);                                     // DB에 메인 바코드 정보 입력
            for (int i = 0; i<barcode_List.Count; i++)
            {
                listBox1.Items.Add("바코드 NO : " + barcode_List[i]);                                  // 리스트 박스에 서브 바코드 정보 출력
                Insert_SBarcode_Info(MB, barcode_List[i].ToString(), remark, conDate, createUser);     // DB에 서브 바코드 정보 입력
            }
            listBox1.Items.Add("발행인 : " + createUser);
            listBox1.Items.Add("메  모 : " + remark + "\r\n");

            txt_CreateUser.Text = string.Empty;
            txt_Remark.Text = string.Empty;
        }


        #region >>>>> DB 채번Table에서 채번해오기
        private string Create_BarcodeNum(string gubun, string conDate)
        {
            string BARCODENUM = "";
            try
            {
                string sQuery = "";

                sQuery = "EXEC SP_MD_GETNO_R @HEADER, @SEQLENGTH, @CONDATE"; // 채번 프로시저 실행

                SqlParameter[] sPrm = new SqlParameter[3]
                {
                      new SqlParameter("@HEADER", gubun)
                    , new SqlParameter("@SEQLENGTH", 5)
                    , new SqlParameter("@CONDATE", conDate)
                };

                BARCODENUM = db.selectParmQuery(sQuery, sPrm);
            
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return BARCODENUM;
        }
        #endregion

        #region >>>>> DB에 출력한 바코드 정보 넣기

        /// <summary>
        /// 메인 바코드 정보 넣기
        /// </summary>
        /// <param name="mBarcode"></param>     메인바코드 번호
        /// <param name="remark"></param>       메모
        /// <param name="conDate"></param>      사용자 지정 발행 기준일
        /// <param name="createUser"></param>   발행인
        private void Insert_MBarcode_Info(string mBarcode, string remark, string conDate, string createUser)
        {
            try
            {
                string sQuery = "";

                sQuery = "EXEC SP_MBARCODE_C @MBARCODEINFO, @REMARK, @CONDATE, @CREATUSER";

                SqlParameter[] sPrm = new SqlParameter[4]
                {
                      new SqlParameter("@MBARCODEINFO", mBarcode)
                    , new SqlParameter("@REMARK", remark)
                    , new SqlParameter("@CONDATE", conDate)
                    , new SqlParameter("@CREATUSER", createUser)
                };

                db.NonQueryParams(sQuery, sPrm);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// 서브 바코드 정보 넣기
        /// </summary>
        /// <param name="mBarcode"></param>     메인바코드 번호
        /// <param name="sBarcode"></param>     서브바코드 번호
        /// <param name="remark"></param>       메모
        /// <param name="conDate"></param>      사용자 지정 발행 기준일
        /// <param name="createUser"></param>   발행인
        private void Insert_SBarcode_Info(string mBarcode, string sBarcode, string remark, string conDate, string createUser)
        {
            try
            {
                string sQuery = "";

                sQuery = "EXEC SP_SBARCODE_C @MBARCODEINFO, @SBARCODEINFO, @REMARK, @CONDATE, @CREATEUSER";

                SqlParameter[] sPrm = new SqlParameter[5]
                {
                      new SqlParameter("@MBARCODEINFO", mBarcode)
                    , new SqlParameter("@SBARCODEINFO", sBarcode)
                    , new SqlParameter("@REMARK", remark)
                    , new SqlParameter("@CONDATE", conDate)
                    , new SqlParameter("@CREATEUSER", createUser)
                };

                db.NonQueryParams(sQuery, sPrm);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        #endregion

        private void btn_Status_Click(object sender, EventArgs e)
        {
            Status_Board status_Board = new Status_Board();
            status_Board.ShowDialog();
        }
    }
}
