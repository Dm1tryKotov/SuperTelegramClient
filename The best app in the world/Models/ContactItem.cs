using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_best_app_in_the_world.Domain;

namespace The_best_app_in_the_world.Models
{
    public class ContactItem : INotifyPropertyChanged
    {
        private long _id;

        public ContactItem(long id)
        {
            this.Id = id;
        }

        public long Id
        {
            get { return _id; }
            set { this.MutateVerbose(ref _id, value, RaisePropertyChanged()); }
        }

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
