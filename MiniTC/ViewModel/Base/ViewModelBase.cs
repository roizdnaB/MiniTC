using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MiniTC.ViewModel.Base
{
    class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void onPropertyChanged(params string[] namesOfProperties) //zmiana właściwości
        {
            if (PropertyChanged != null)
            {
                foreach (var prop in namesOfProperties)
                { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
            }
        }
    }
}
