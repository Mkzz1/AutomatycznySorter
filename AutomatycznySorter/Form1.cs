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
            if (File.Exists(@"C:\Users\mikoz\Desktop\doki\abc.csv"))
            {
                var timer = new System.Threading.Timer((e) =>
                {
                    //save every column of spreadsheet into separate text files in same folder
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    string filePath1 = @"C:\Users\mikoz\Desktop\doki\abc.csv";
                    using (var stream = File.Open(filePath1, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateCsvReader(stream))
                        {
                            //every column of spreadsheet is saved into separate text file. Like column 1 is file1, column 2 is file2 etc.
                            while (reader.Read())
                            {
                                string filePath2 = @"C:\Users\mikoz\Desktop\doki\" + reader.GetValue(0) + ".txt";
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
                    //Search for folder location in database based on user login. The user's login is the first line of each .txt file in the folder. Move all files that begin their name with the string of characters below the lines of the text file to the user's folder. Do for every txt file in folder.


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