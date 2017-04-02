using System;
using System.ComponentModel;
using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Linq;

namespace HW6
{
    public class ResultsContext : INotifyPropertyChanged
    {
        public ResultsContext()
        {
            Clear();
        }

        public string MostCommonItem { get; private set; }

        public string MostCommonItemUnits { get; private set; }

        public string HighestIncomeItem { get; private set; }

        public string HighestIncomeItemTotal { get; private set; }

        public void Clear()
        {
            MostCommonItem = "Not Yet Loaded";
            MostCommonItemUnits = null;
            HighestIncomeItem = "Not Yet Loaded";
            HighestIncomeItemTotal = null;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MostCommonItem"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MostCommonItemUnits"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HighestIncomeItem"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HighestIncomeItemTotal"));
        }

        public void ReadSheet(Worksheet worksheet)
        {
            Range worksheetRange = worksheet.UsedRange;
            int numColumns = worksheetRange.Columns.Count;
            int numRows = worksheetRange.Rows.Count;

            var items = new Dictionary<string, Tuple<int, double>>();

            for (int row = 2; row <= numRows; ++row)
            {
                string item = worksheet.Cells[row, 4].Value;
                int units = Convert.ToInt32(worksheet.Cells[row, 5].Value);
                double total = worksheet.Cells[row, 7].Value;

                if (!items.ContainsKey(item))
                {
                    items.Add(item, Tuple.Create(units, total));
                }
                else
                {
                    Tuple<int, double> currentItem = items[item];
                    items[item] = Tuple.Create(currentItem.Item1 + units, currentItem.Item2 + total);
                }
            }

            var mostCommon = items.OrderByDescending(item => item.Value.Item1).FirstOrDefault();
            MostCommonItem = mostCommon.Key;
            MostCommonItemUnits = mostCommon.Value.Item1.ToString();

            var highestIncome = items.OrderByDescending(item => item.Value.Item2).FirstOrDefault();
            HighestIncomeItem = highestIncome.Key;
            HighestIncomeItemTotal = highestIncome.Value.Item2.ToString();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MostCommonItem"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MostCommonItemUnits"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HighestIncomeItem"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HighestIncomeItemTotal"));
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}