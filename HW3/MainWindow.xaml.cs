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

                // Display the maximum integer in fileInts
                displayMaxInt(fileInts.Max());
            }
        }

        private void pickFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true) // If a file is selected
            {
                this.FileName = openFileDialog.FileName; // Set the text box to the file name selected
            }
        }

        private void displayError(string message)
        {
            success.Visibility = Visibility.Hidden; // Hide the success message box
            errorMessage.Text = message; // Set the error text
            error.Visibility = Visibility.Visible; // Show the error message box
        }

        private void displayMaxInt(int maxInt)
        {
            error.Visibility = Visibility.Hidden; // Hide the error messsage box
            maxInteger.Text = maxInt.ToString(); // Set the text to the maximum integer
            success.Visibility = Visibility.Visible; // Show the success message box
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
                    // Send a property changed event to update the text box
                    PropertyChanged(this, new PropertyChangedEventArgs("FileName"));
                }
            }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
