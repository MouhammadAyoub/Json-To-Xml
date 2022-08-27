using Microsoft.Win32;
using Newtonsoft.Json;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;

namespace JsonToXml
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChooseFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = "C:\\",
                Filter = "JSON files | *.json"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                myText.Text = File.ReadAllText(openFileDialog.FileName);
                myFileName.Content = Path.GetFileName(openFileDialog.FileName);
                convert.IsEnabled = true;
                convert.Foreground = Brushes.White;
            }
        }

        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            XNode node = JsonConvert.DeserializeXNode(myText.Text, "Root");
            myText.Text = node.ToString();
            export.IsEnabled = true;
            export.Foreground = Brushes.White;
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(0xFF, 0x7B, 0xA4, 0x98));

            var sfd = new SaveFileDialog
            {
                Filter = "XML Files | *.xml"
            };

            if (sfd.ShowDialog() == true)
            {
                string filename = sfd.FileName;
                File.WriteAllText(filename, (string)myText.Text);
            }

            myFileName.Content = "";
            myText.Text = "";

            convert.IsEnabled = false;
            convert.Foreground = brush;

            export.IsEnabled = false;
            export.Foreground = brush;
        }

    }
}
