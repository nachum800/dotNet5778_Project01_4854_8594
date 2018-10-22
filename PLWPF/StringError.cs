using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PLWPF.Annotations;

namespace PLWPF
{
    class StringError:IDataErrorInfo,INotifyPropertyChanged
    {
        private string fieldContent;

        public string FieldContent
        {
            get { return fieldContent; }
            set
            {
                fieldContent = value;
                NotifyPropertyChanged("FieldContent");
            }
        }



        #region IDataErrorInfoMembers

        public string this[string columnName]
        {
            get { throw new NotImplementedException(); }
        }

        public string Error { get; }

        #endregion

        #region INotifyPropertyChangedMember
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
