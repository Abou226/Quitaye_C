using Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Models
{
    public class CardModel : INotifyPropertyChanged
    {
        private string number;

        public string Number
        {
            get { return number; }
            set
            {
                if (number == value)
                    return;

                number = value;
                OnPropertyChanged();
            }
        }
        private long expMonth;
        public long ExpMonth
        {
            get => expMonth;
            set
            {
                if (expMonth == value)
                    return;

                expMonth = value;
                OnPropertyChanged();
            }
        }

        private long expYear;
        public long ExpYear
        {
            get => expYear;
            set
            {
                if (expYear == value)
                    return;

                expYear = value;
                OnPropertyChanged();
            }
        }

        private string cvc;
        public string Cvc
        {
            get => cvc;
            set
            {
                if (cvc == value)
                    return;

                cvc = value;
                OnPropertyChanged();
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (name == value)
                    return;
                name = value;
                OnPropertyChanged();
            }
        }

        private string adresseCity;

        public string AddressCity
        {
            get { return adresseCity; }
            set
            {
                if (adresseCity == value)
                    return;

                adresseCity = value;
                OnPropertyChanged();
            }
        }

        private string addressZip;

        public string AddressZip
        {
            get { return addressZip; }
            set
            {
                if (addressZip == value)
                    return;

                addressZip = value;
                OnPropertyChanged();
            }
        }

        private string currency;

        public string Currency
        {
            get { return currency; }
            set
            {
                if (currency == value)
                    return;

                currency = value;
                OnPropertyChanged();
            }
        }

        private string addressLine1;

        public string AddressLine1
        {
            get { return addressLine1; }
            set
            {
                if (addressLine1 == value)
                    return;

                addressLine1 = value;
                OnPropertyChanged();
            }
        }

        private string addressCountry;

        public string AddressCountry
        {
            get { return addressCountry; }
            set
            {
                if (addressCountry == value)
                    return;

                addressCountry = value;
                OnPropertyChanged();
            }
        }

        private string stripeApiKey;

        public string StripeApiKey
        {
            get { return stripeApiKey; }
            set
            {
                if (stripeApiKey == value)
                    return;

                stripeApiKey = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}