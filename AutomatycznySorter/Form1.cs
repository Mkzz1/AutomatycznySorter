using CsvHelper;
using System.Data.SQLite;
using System.Globalization;

namespace AutomatycznySorter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //start SZZSort method
            SZZSort();
            
        }

        private void SZZSort()
        {
            string folderLocation = textBox1.Text;

            //Start readers
            var reader = new StreamReader(folderLocation + "\\" + "file.csv");
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            //get the all usernames from the first row
            var usernames = csv.GetRecords<string>();

            //Create a new SQLite connection
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=database.mdf;Version=3;");
            m_dbConnection.Open();

            //find the folder location of the user in database.mdf
            foreach (var username in usernames)
            {
                string sql = "SELECT FolderLocation FROM Workers WHERE Username = '" + username + "'";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader2 = command.ExecuteReader();
                while (reader2.Read())
                {
                    string folderLocation2 = reader2["FolderLocation"].ToString();
                }
            }
            //Rest rows of the CSV file are case names. Move all files into the user folder that contain the case name in their name.
            foreach (var username in usernames)
            {
                string sql = "SELECT FolderLocation FROM Workers WHERE Username = '" + username + "'";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader2 = command.ExecuteReader();
                while (reader2.Read())
                {
                    string folderLocation2 = reader2["FolderLocation"].ToString();
                    string[] files = System.IO.Directory.GetFiles(folderLocation + "\\" + username);
                    foreach (var file in files)
                    {
                        string fileName = System.IO.Path.GetFileName(file);
                        if (fileName.Contains(username))
                        {
                            System.IO.File.Move(file, folderLocation2 + "\\" + fileName);
                        }
                    }
                }
            }
            //Close the connection
            m_dbConnection.Close();
        }
        
        private void WAWSort()
        {
            
        }
        
        private void POZSort()
        {
            
        }
        
        private void KTWSort()
        {
            
        }
        
        private void GDNSort()
        {
            
        }

        private void WROSort()
        {
            
        }
    }
}