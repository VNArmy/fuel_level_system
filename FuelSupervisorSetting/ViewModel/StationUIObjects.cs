using FLS.Business;
using FuelSupervisorSetting.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupervisorSetting.ViewModel
{
    public class StationUIObjects : ObservableCollection<StationUIObject>
    {
        protected override void InsertItem(int index, StationUIObject item)
        {
            base.InsertItem(index, item);

            // handle any EndEdit events relating to this item
            item.StationListItemEndEdit += new StationListItemEndEditEventHandler(StationListItemEndEditHandler);
        }

        void StationListItemEndEditHandler(IEditableObject sender)
        {
            // simply forward any EndEdit events
            if (StationListItemEndEdit != null)
            {
                StationListItemEndEdit(sender);
            }
        }

        #region events

        public event StationListItemEndEditEventHandler StationListItemEndEdit;

        #endregion
    }
    public delegate void StationListItemEndEditEventHandler(IEditableObject sender);
    public class StationUIObject : IEditableObject, INotifyPropertyChanged
    {
        private StationListRecord _StationList;
        public StationUIObject()
        {
            _StationList = new StationListRecord();
        }

        public StationUIObject(StationListRecord stationlist)
        {
            _StationList = stationlist;
        }
        public StationListRecord GetDataObject()
        {
            return _StationList;
        }

        public long StationId {
            get { return _StationList.StationId; }
            set { _StationList.StationId = value; RaisePropertyChanged("StationId"); }
        }
        public String StationName {
            get { return _StationList.StationName; }
            set { _StationList.StationName = value; RaisePropertyChanged("StationName"); }
        
        }
        public String StationDesc {
            get { return _StationList.StationDesc; }
            set { _StationList.StationDesc = value; RaisePropertyChanged("StationDesc"); }
        }
        public String StationAddress {
            get { return _StationList.StationAddress; }
            set { _StationList.StationAddress = value; RaisePropertyChanged("StationAddress"); }
        }
        public String TaxCode
        {
            get { return _StationList.TaxCode; }
            set { _StationList.TaxCode = value; RaisePropertyChanged("TaxCode"); }
        }


        #region events

        public event StationListItemEndEditEventHandler StationListItemEndEdit;

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
            if (StationListItemEndEdit != null)
            {
                StationListItemEndEdit(this);
            }
        }
        public void NotifyIdChange()
        {
            RaisePropertyChanged("StationId");
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

