using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace LargestNumber
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

                var fileInts = new List<int>();

                // Convert each string into an integer and store in `fileInts[]`
                foreach (string s in fileStrings)
                {
                    try
                    {
                        fileInts.Add(int.Parse(s));
                    }
                    catch (Exception) { } // Ignore non integers
                }

                // Display the maximum integer in fileInts
                displayMaxInt(fileInts);
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
            success.Visibility = Visibility.Hidden;
            MessageBox.Show(message, null, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void displayMaxInt(IEnumerable<int> fileInts)
        {
            // If no integers found return an info popup
            if (fileInts.Count() == 0)
            {
                success.Visibility = Visibility.Hidden;
                MessageBox.Show("No integers found!", null, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            maxInteger.Text = fileInts.Max().ToString(); // Set the text to the maximum integer
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
