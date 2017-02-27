using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;

namespace HW3
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _fileName;

        public MainWindow()
        {
            InitializeComponent();
            container.DataContext = this;
        }

        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FileName"));
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void readFile_Click(object sender, RoutedEventArgs e)
        {
            // Check if file exists
            if (FileName == null || !File.Exists(FileName))
            {
                DisplayError($"Unable to open file: \"{FileName}\"");
            }
            else
            {
                // Read file contents and split into strings at spaces, tabs, and newlines
                var fileContents = File.ReadAllText(FileName);
                var fileStrings = fileContents.Split(new[] {' ', '\t', '\r', '\n'},
                    StringSplitOptions.RemoveEmptyEntries);

                // Convert each string into an integer and store in `fileInts[]`
                var fileInts = Array.ConvertAll(fileStrings, int.Parse);

                // Display the maximum integer in fileInts
                DisplayMaxInt(fileInts.Max());
            }
        }

        private void pickFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true) // If a file is selected
                FileName = openFileDialog.FileName; // Set the text box to the file name selected
        }

        private void DisplayError(string message)
        {
            success.Visibility = Visibility.Hidden; // Hide the success message box
            errorMessage.Text = message; // Set the error text
            error.Visibility = Visibility.Visible; // Show the error message box
        }

        private void DisplayMaxInt(int maxInt)
        {
            error.Visibility = Visibility.Hidden; // Hide the error messsage box
            maxInteger.Text = maxInt.ToString(); // Set the text to the maximum integer
            success.Visibility = Visibility.Visible; // Show the success message box
        }
    }
}
