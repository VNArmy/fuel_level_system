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
    public class CalibrationTabUIObjects : ObservableCollection<CalibrationTabUIObject>
    {
        protected override void InsertItem(int index, CalibrationTabUIObject item)
        {
            base.InsertItem(index, item);

            // handle any EndEdit events relating to this item
            item.ItemEndEdit += new CalibrationItemEndEditEventHandler(ItemEndEditHandler);
        }

        void ItemEndEditHandler(IEditableObject sender)
        {
            // simply forward any EndEdit events
            if (ItemEndEdit != null)
            {
                ItemEndEdit(sender);
            }
        }

        #region events

        public event CalibrationItemEndEditEventHandler ItemEndEdit;

        #endregion
    }
    
    public delegate void CalibrationItemEndEditEventHandler(IEditableObject sender);
    
    public class CalibrationTabUIObject : IEditableObject, INotifyPropertyChanged
    {
        private CalibrationRecord _Calibration;
        public CalibrationTabUIObject()
        {
            _Calibration = new CalibrationRecord();
        }
      
        public CalibrationTabUIObject(CalibrationRecord interpolation)
        {
            _Calibration = interpolation;
        }
        public CalibrationRecord GetDataObject()
        {
            return _Calibration;
        }

        public UInt64 PK {
            get { return _Calibration.PK;}
            set { _Calibration.PK = value; RaisePropertyChanged("PK"); }
        }
        public long TankId {
            get { return _Calibration.TankId; }
            set { _Calibration.TankId = value; RaisePropertyChanged("TankId"); }
        
        }
        public Int32 Raw
        {
            get { return _Calibration.Raw; }
            set { _Calibration.Raw = value; RaisePropertyChanged("Raw"); }
        }
        public Int32 Level {
            get { return _Calibration.Level; }
            set { _Calibration.Level = value; RaisePropertyChanged("Level"); }
        }
        


        #region events

        public event CalibrationItemEndEditEventHandler ItemEndEdit;

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
            if (ItemEndEdit != null)
            {
                ItemEndEdit(this);
            }
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

