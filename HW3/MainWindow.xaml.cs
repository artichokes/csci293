using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace HW3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            container.DataContext = this;
        }

        private void readFile_Click(object sender, RoutedEventArgs e)
        {
            // Check if file exists
            if (FileName == null || !System.IO.File.Exists(FileName))
            {
                displayError(string.Format("Unable to open file: \"{0}\"", FileName));
            }
            else
            {
                // Read file contents and split into strings at spaces, tabs, and newlines
                var fileContents = System.IO.File.ReadAllText(FileName);
                var fileStrings = fileContents.Split(new[] { ' ', '\t', '\r', '\n' },
                    StringSplitOptions.RemoveEmptyEntries);

                // Convert each string into an integer and store in `fileInts[]`
                var fileInts = Array.ConvertAll(fileStrings, int.Parse);

                displayMaxInt(fileInts.Max());
            }
        }

        private void pickFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                this.FileName = openFileDialog.FileName;
            }
        }

        private void displayError(string message)
        {
            success.Visibility = Visibility.Hidden;
            errorMessage.Text = message;
            error.Visibility = Visibility.Visible;
        }

        private void displayMaxInt(int maxInt)
        {
            error.Visibility = Visibility.Hidden;
            maxInteger.Text = maxInt.ToString();
            success.Visibility = Visibility.Visible;
        }

        private string _fileName;
        public string FileName
        {
            get { return this._fileName; }
            set
            {
                this._fileName = value;
                if (null != this.PropertyChanged)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("FileName"));
                }
            }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
