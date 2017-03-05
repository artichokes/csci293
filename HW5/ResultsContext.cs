using System;
using System.ComponentModel;
using Microsoft.Office.Interop.Excel;

namespace HW5
{
    public class ResultsContext : INotifyPropertyChanged
    {
        public string A1 { get; private set; }

        public string B1 { get; private set; }

        public string C1 { get; private set; }

        public string D1 { get; private set; }

        public string E1 { get; private set; }

        public string F1 { get; private set; }

        public string G1 { get; private set; }

        public string A2 => _a2?.ToShortDateString();
        public string B2 { get; private set; }

        public string C2 { get; private set; }

        public string D2 { get; private set; }

        public string E2 => Convert.ToInt32(_e2).ToString();
        public string F2 => _f2?.ToString();
        public string G2 => _g2?.ToString();

        public void Clear()
        {
            A1 = B1 = C1 = D1 = E1 = F1 = G1 = null;
            _a2 = null;
            B2 = C2 = D2 = null;
            _e2 = _f2 = _g2 = null;

            foreach (var cell in _cells)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(cell));
        }

        public void ReadSheet(Worksheet worksheet)
        {
            A1 = worksheet.Cells[1, 1].Value;
            B1 = worksheet.Cells[1, 2].Value;
            C1 = worksheet.Cells[1, 3].Value;
            D1 = worksheet.Cells[1, 4].Value;
            E1 = worksheet.Cells[1, 5].Value;
            F1 = worksheet.Cells[1, 6].Value;
            G1 = worksheet.Cells[1, 7].Value;

            _a2 = worksheet.Cells[2, 1].Value;
            B2 = worksheet.Cells[2, 2].Value;
            C2 = worksheet.Cells[2, 3].Value;
            D2 = worksheet.Cells[2, 4].Value;
            _e2 = worksheet.Cells[2, 5].Value;
            _f2 = worksheet.Cells[2, 6].Value;
            _g2 = worksheet.Cells[2, 7].Value;

            foreach (var cell in _cells)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(cell));
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private DateTime? _a2;
        private double? _e2, _f2, _g2;

        private readonly string[] _cells =
        {
            "A1", "B1", "C1", "D1", "E1", "F1", "G1",
            "A2", "B2", "C2", "D2", "E2", "F2", "G2"
        };

        #endregion
    }
}