using System.ComponentModel;

namespace HW6
{
    public class FilePickerContext : INotifyPropertyChanged
    {
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

        private string _fileName;

        #endregion
    }
}