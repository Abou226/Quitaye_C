using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Models
{
    public class PaymentModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the payment token from client.
        /// </summary>

        private string token;
        public string Token
        {
            get => token;
            set
            {
                if (token == value)
                    return;

                token = value;
                OnPropertyChanged();
            }
        }

        private long amount;
        public long Amount
        {
            get => amount;
            set
            {
                if (amount == value)
                    return;
                amount = value;
                OnPropertyChanged();
            }
        }

        public string Description { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
