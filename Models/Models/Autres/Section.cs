using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Models
{
    public class Section : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public int Id { get; set; }
        public string Nom { get; set; }
        public string Type { get; set; }
        private string _currentIcon;

        public string CurrentIcon
        {
            get { return _currentIcon; }
            set 
            {
                if (_currentIcon == value)
                    return;
                _currentIcon = value;
                OnPropertyChanged();
            }
        }

        private string _black_icon;

        public string Black_Icon
        {
            get { return _black_icon; }
            set 
            {
                if (_black_icon == value)
                    return;
                _black_icon = value;
                OnPropertyChanged();
            }
        }

        private string _color = "Secondary";

        public string Color
        {
            get { return _color; }
            set 
            {
                if (_color == value)
                    return;
                _color = value;
                OnPropertyChanged();
            }
        }


        private string _icon;

        public string Icon
        {
            get { return _icon; }
            set 
            {
                if (_icon == value)
                    return;
                _icon = value;
                OnPropertyChanged();
            }
        }


    }
}
