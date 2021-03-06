﻿using System.IO;
using System.Windows;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using Application = Microsoft.Office.Interop.Excel.Application;
using Window = System.Windows.Window;
using System;

namespace HW6
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly FilePickerContext _filePickerContext = new FilePickerContext();
        private readonly ResultsContext _resultsContext = new ResultsContext();

        public MainWindow()
        {
            InitializeComponent();

            // Set data contexts
            FilePicker.DataContext = _filePickerContext;
            Results.DataContext = _resultsContext;
        }

        public void ReadFile_Click(object sender, RoutedEventArgs e)
        {
            _resultsContext.Clear(); // Clear existing data

            // Check if file exists
            if (_filePickerContext.FileName == null || !File.Exists(_filePickerContext.FileName))
            {
                DisplayError($"File does not exist: \"{_filePickerContext.FileName}\"");
            }
            else
            {
                Application excelApp = null;
                Workbook workbook = null;
                try
                {
                    excelApp = new Application { Visible = false };
                    workbook = excelApp.Workbooks.Open(_filePickerContext.FileName, ReadOnly: true);
                    Worksheet worksheet = workbook.Sheets[2]; // Second sheet

                    _resultsContext.ReadSheet(worksheet);
                }
                catch (Exception err)
                {
                    DisplayError($"Unable to open file: \"{_filePickerContext.FileName}\"\n{err.Message}");
                }
                finally
                {
                    workbook?.Close(false);
                    excelApp?.Quit();
                }
            }
        }

        public void PickFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                DefaultExt = ".xlsx",
                Filter = "Excel Documents (.xlsx) | *.xlsx"
            };
            if (openFileDialog.ShowDialog() == true) // If a file is selected
                _filePickerContext.FileName = openFileDialog.FileName; // Set the text box to the file name selected
        }

        private void DisplayError(string message)
        {
            Success.Visibility = Visibility.Hidden;
            MessageBox.Show(message, null, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}