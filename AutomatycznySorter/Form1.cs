using ExcelDataReader;
using System.Data.SQLite;
using System.Text;

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
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(1);
            
            //repeat below code every 5 minutes
            if (File.Exists(@"C:\Users\Mkzz\Desktop\doki\AutoAssigment.csv"))
            {
                var timer = new System.Threading.Timer((e) =>
                {
                    //save every column of spreadsheet into separate text files in same folder
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    string filePath1 = @"C:\Users\Mkzz\Desktop\doki\AutoAssigment.csv";
                    using (var stream = File.Open(filePath1, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateCsvReader(stream))
                        {
                            //every column of spreadsheet is saved into separate text file. Like column 1 is file1, column 2 is file2 etc.
                            while (reader.Read())
                            {
                                string filePath2 = @"C:\Users\Mkzz\Desktop\doki\" + reader.GetValue(0) + ".txt";
                                using (StreamWriter sw = new StreamWriter(filePath2))
                                {
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        sw.WriteLine(reader.GetValue(i));
                                    }
                                }
                            }
                        }
                    }
                    //remove first line of every .txt file in folder. Move all other lines to first line.
                    string[] filePaths = Directory.GetFiles(@"C:\Users\Mkzz\Desktop\doki\", "*.txt");
                    foreach (string filePath in filePaths)
                    {
                        string[] lines = File.ReadAllLines(filePath);
                        if (lines.Length > 1)
                        {
                            File.WriteAllLines(filePath, lines.Skip(1));
                        }
                    }
                    //move files
                    string[] files = Directory.GetFiles(@"C:\Users\Mkzz\Desktop\doki\");
                    string[] txtFiles = Directory.GetFiles(@"C:\Users\Mkzz\Desktop\doki\", "*.txt");
                    foreach (string txtFile in txtFiles)
                    {
                        string txtFileName = Path.GetFileName(txtFile);
                        string txtFileNameWithoutExtension = Path.GetFileNameWithoutExtension(txtFile);
                        string[] txtFileContent = File.ReadAllLines(txtFile);
                        //find FolderLocation in database.mdf file
                        string connectionString = @"Data Source=C:\Users\Mkzz\Desktop\doki\database.mdf;Version=3;";
                        string query = "SELECT FolderLocation FROM Users WHERE Name = '" + txtFileNameWithoutExtension + "'";
                        //get query result as string
                        string folderLocation = "";
                        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                        {
                            connection.Open();
                            using (SQLiteCommand command = new SQLiteCommand(query, connection))
                            {
                                using (SQLiteDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        folderLocation = reader.GetString(0);
                                    }
                                }
                            }
                            string[] filesInFolder = Directory.GetFiles(@"C:\Users\Mkzz\Desktop\doki");
                            foreach (string file in filesInFolder)
                            {
                                string fileName = Path.GetFileName(file);
                                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                                string[] fileContent = File.ReadAllLines(file);
                                foreach (string line in fileContent)
                                {
                                    foreach (string txtLine in txtFileContent)
                                    {
                                        if (fileName.Contains(txtLine))
                                        {
                                            File.Move(file, Path.Combine(folderLocation, fileName));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }, null, startTimeSpan, periodTimeSpan);
            }
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