using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AruCikkOlvaso
{
    public class Items : INotifyPropertyChanged
    {       

        private string _name;
        private string _id;
        private string _barCode;
        private string _unit;

        public Items()
        {
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id == value) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        public string BarCode
        {
            get
            {
                return _barCode;
            }
            set
            {
                if (_barCode == value) return;
                _barCode = value;
                OnPropertyChanged();
            }
        }

        public string Unit
        {
            get
            {
                return _unit;
            }
            set
            {
                if (_unit == value) return;
                _unit = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
