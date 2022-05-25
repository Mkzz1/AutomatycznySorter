using ExcelDataReader;
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
            //if file doesnt exist, do nothing and wait for file to be created
            if (!File.Exists(@"C:\Users\Mkzz\Desktop\doki\AutoAssigment.csv"))
            {
                Thread.Sleep(1000);
            }
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
                                string filePath2 = @"C:\Users\Mkzz\Desktop\doki\AutoAssigment" + reader.GetValue(0) + ".txt";
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