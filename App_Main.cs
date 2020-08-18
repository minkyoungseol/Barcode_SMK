using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Barcode
{
    public class App_Main
    {
        // 서버 접속을 위한 정보 저장
        public static string ADDRESS;
        public static string DATABASE;
        public static string S_UID;
        public static string S_PWD;
        public static string QUERYTIME;

        public static void Read_INI()
        {
            // DB서버 접속 정보 App.Config에서 가져오기
            ADDRESS = ConfigurationManager.AppSettings["ADDRESS"];
            DATABASE = ConfigurationManager.AppSettings["DATABASE"];
            S_UID = ConfigurationManager.AppSettings["S_UID"];
            S_PWD = ConfigurationManager.AppSettings["S_PWD"];
            QUERYTIME = ConfigurationManager.AppSettings["QUERYTIME"];

            // DB서버 연결 주소
            string strDBCon = "Server=" + ADDRESS + ";Database=" + DATABASE + ";User ID=" + S_UID + ";Password=" + S_PWD;

            try
            {
                // App.Config에서 받은 주소, 데이터베이스, 아이디, 패스워드 사용하여 DB서버 연결
                SqlConnection sqlConnection = new SqlConnection(strDBCon);
                if (sqlConnection != null && sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("프로그램 실행을 위한 조건이 불충분합니다!!" + "\r\n" + ex.Message);
                Application.Exit();
            }
        }


    }
}