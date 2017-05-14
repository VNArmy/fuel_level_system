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
    public class InterpolationTabUIObjects : ObservableCollection<InterpolationTabUIObject>
    {
        protected override void InsertItem(int index, InterpolationTabUIObject item)
        {
            base.InsertItem(index, item);

            // handle any EndEdit events relating to this item
            item.ItemEndEdit += new ItemEndEditEventHandler(ItemEndEditHandler);
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

        public event ItemEndEditEventHandler ItemEndEdit;

        #endregion
    }
    
    public delegate void ItemEndEditEventHandler(IEditableObject sender);
    
    public class InterpolationTabUIObject : IEditableObject, INotifyPropertyChanged
    {
        private InterpolationRecord _Interpolation;
        public InterpolationTabUIObject()
        {
            _Interpolation = new InterpolationRecord();
        }
      
        public InterpolationTabUIObject(InterpolationRecord interpolation)
        {
            _Interpolation = interpolation;
        }
        public InterpolationRecord GetDataObject()
        {
            return _Interpolation;
        }

        public UInt64 PK {
            get { return _Interpolation.PK;}
            set { _Interpolation.PK = value; RaisePropertyChanged("PK"); }
        }
        public long TankId {
            get { return _Interpolation.TankId; }
            set { _Interpolation.TankId = value; RaisePropertyChanged("TankId"); }
        
        }
        public Double Level {
            get { return _Interpolation.Level; }
            set { _Interpolation.Level = value; RaisePropertyChanged("Level"); }
        }
        public Double Capacity {
            get { return _Interpolation.Capacity; }
            set { _Interpolation.Capacity = value; RaisePropertyChanged("Capacity"); }
        }


        #region events

        public event ItemEndEditEventHandler ItemEndEdit;

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

