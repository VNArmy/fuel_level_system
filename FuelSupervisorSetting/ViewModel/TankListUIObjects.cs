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
    public class TankListUIObjects : ObservableCollection<TankUIObject>
    {
        protected override void InsertItem(int index, TankUIObject item)
        {
            base.InsertItem(index, item);

            // handle any EndEdit events relating to this item
            item.TankListItemEndEdit += new TankListItemEndEditEventHandler(TankListItemEndEditHandler);
        }

        void TankListItemEndEditHandler(IEditableObject sender)
        {
            // simply forward any EndEdit events
            if (TankListItemEndEdit != null)
            {
                TankListItemEndEdit(sender);
            }
        }

        #region events

        public event TankListItemEndEditEventHandler TankListItemEndEdit;

        #endregion
    }
    public delegate void TankListItemEndEditEventHandler(IEditableObject sender);
    public class TankUIObject : IEditableObject, INotifyPropertyChanged
    {
        private TankListRecord _TankList;
        public TankUIObject()
        {
            _TankList = new TankListRecord();
        }

        public TankUIObject(TankListRecord tankList)
        {
            _TankList = tankList;
        }
        public TankListRecord GetDataObject()
        {
            return _TankList;
        }
        

        public long TankId
        {
            get { return _TankList.TankId; }
            set { _TankList.TankId = value; RaisePropertyChanged("TankId"); }
        }
        public String TankName
        {
            get { return _TankList.TankName; }
            set { _TankList.TankName = value; RaisePropertyChanged("TankName"); }

        }
        public String TankDesc
        {
            get { return _TankList.TankDesc; }
            set { _TankList.TankDesc = value; RaisePropertyChanged("TankDesc"); }

        }

        public Double Max
        {
            get { return _TankList.Max; }
            set { _TankList.Max = value; RaisePropertyChanged("MaxCapacity"); }
        }
        public Double Offset
        {
            get { return _TankList.Offset; }
            set { _TankList.Offset = value; RaisePropertyChanged("Offset"); }
        }

        public long StationId
        {
            get { return _TankList.StationId; }
            set { _TankList.StationId = value; RaisePropertyChanged("StationId"); }
        }
        
        public long FuelId
        {
            get { return _TankList.FuelId; }
            set { _TankList.FuelId = value; RaisePropertyChanged("FuelId"); }
        }
        

        #region events

        public event TankListItemEndEditEventHandler TankListItemEndEdit;

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
            if (TankListItemEndEdit != null)
            {
                TankListItemEndEdit(this);
            }
        }
        public void NotifyIdChange()
        {
            RaisePropertyChanged("TankId");
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
