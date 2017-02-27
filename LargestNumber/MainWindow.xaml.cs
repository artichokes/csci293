using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;

namespace LargestNumber
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

                var fileInts = new List<int>();

                // Convert each string into an integer and store in `fileInts[]`
                foreach (var s in fileStrings)
                    try
                    {
                        fileInts.Add(int.Parse(s));
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                // Display the maximum integer in fileInts
                DisplayMaxInt(fileInts);
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
            success.Visibility = Visibility.Hidden;
            MessageBox.Show(message, null, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void DisplayMaxInt(List<int> fileInts)
        {
            // If no integers found return an info popup
            if (!fileInts.Any())
            {
                success.Visibility = Visibility.Hidden;
                MessageBox.Show("No integers found!", null, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            maxInteger.Text = fileInts.Max().ToString(); // Set the text to the maximum integer
            success.Visibility = Visibility.Visible; // Show the success message box
        }
    }
}