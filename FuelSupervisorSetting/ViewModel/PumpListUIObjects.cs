using FLS.Business;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupervisorSetting.ViewModel
{
    public class PumpListUIObjects : ObservableCollection<PumpUIObject>
    {
        protected override void InsertItem(int index, PumpUIObject item)
        {
            base.InsertItem(index, item);

            // handle any EndEdit events relating to this item
            item.PumpListItemEndEdit += new PumpListItemEndEditEventHandler(PumpListItemEndEditHandler);
        }

        void PumpListItemEndEditHandler(IEditableObject sender)
        {
            // simply forward any EndEdit events
            if (PumpListItemEndEdit != null)
            {
                PumpListItemEndEdit(sender);
            }
        }

        #region events

        public event PumpListItemEndEditEventHandler PumpListItemEndEdit;

        #endregion
    }
    public delegate void PumpListItemEndEditEventHandler(IEditableObject sender);
    public class PumpUIObject : IEditableObject, INotifyPropertyChanged
    {
        private PumpListRecord _PumpList;
        public PumpUIObject()
        {
            _PumpList = new PumpListRecord();
        }

        public PumpUIObject(PumpListRecord tankList)
        {
            _PumpList = tankList;
        }
        public PumpListRecord GetDataObject()
        {
            return _PumpList;
        }
        

        public long PumpId
        {
            get { return _PumpList.PumpId; }
            set { _PumpList.PumpId = value; RaisePropertyChanged("PumpId"); }
        }

        public long TankId
        {
            get { return _PumpList.TankId; }
            set { _PumpList.TankId = value; RaisePropertyChanged("TankId"); }
        }
        public long StationId
        {
            get { return _PumpList.StationId; }
            set { _PumpList.StationId = value; RaisePropertyChanged("StationId"); }
        }

        public String PumpName
        {
            get { return _PumpList.PumpName; }
            set { _PumpList.PumpName = value; RaisePropertyChanged("PumpName"); }

        }
        public String PumpDesc
        {
            get { return _PumpList.PumpDesc; }
            set { _PumpList.PumpDesc = value; RaisePropertyChanged("PumpDesc"); }

        }
        public String PumpIpAddress
        {
            get { return _PumpList.PumpIp; }
            set { _PumpList.PumpIp = value; RaisePropertyChanged("PumpIp"); }

        }
        public int PumpPort
        {
            get { return _PumpList.PumpPort; }
            set { _PumpList.PumpPort = value; RaisePropertyChanged("PumpPort"); }
        }

        public int PumpDelayTime
        {
            get { return _PumpList.PumpDelayTime; }
            set { _PumpList.PumpDelayTime = value; RaisePropertyChanged("PumpDelayTime"); }
        }

        public int PumpStatus
        {
            get { return _PumpList.PumpStatus; }
            set { _PumpList.PumpStatus = value; RaisePropertyChanged("PumpStatus"); }
        }

        public long PumpPickUp
        {
            get { return _PumpList.PumpPickUp; }
            set { _PumpList.PumpPickUp = value; RaisePropertyChanged("PumpPickUp"); }
        } 
       
        
        #region events

        public event PumpListItemEndEditEventHandler PumpListItemEndEdit;

        #endregion

        #region IEditableObject Members

        public void BeginEdit()
        {
        }

        public void CancelEdit()
        {
        }

        public void EndEdit()
        {
            if (PumpListItemEndEdit != null)
            {
                PumpListItemEndEdit(this);
            }
        }
        public void NotifyIdChange()
        {
            RaisePropertyChanged("PumpId");
        }
        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        #endregion
    }
}
