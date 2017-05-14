using FLS.Business;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FuelSupervisorSetting.ViewModel
{
    public delegate void PersistenceErrorHandler(InterpolationTabDataProvider dataProvider, Exception e);

    public class InterpolationTabDataProvider
    {
        IInterpolationTabDAL dataAccessLayer;
        long currTankId;
        public InterpolationTabDataProvider()
        {
            dataAccessLayer = new InterpolationTabDAL();
        }

        public InterpolationTabUIObjects GetInterpolationTab(long tankid)
        {
            //if (!(String.IsNullOrEmpty(fileName)))
            //{
            //     dataAccessLayer.ImportInterpolationTab(fileName, pumpid);
            //}
            currTankId = tankid;
            // populate our list of customers from the data access layer
            InterpolationTabUIObjects iTabObjs = new InterpolationTabUIObjects();

            List<InterpolationRecord> iTabDataObjects = dataAccessLayer.GetInterpolationTab(tankid);
            foreach (InterpolationRecord iTabDataObject in iTabDataObjects)
            {
                // create a business object from each data object
                iTabObjs.Add(new InterpolationTabUIObject(iTabDataObject));
            }

            iTabObjs.ItemEndEdit += new ItemEndEditEventHandler(InterpolationTabItemEndEdit);
            iTabObjs.CollectionChanged += new NotifyCollectionChangedEventHandler(InterpolationTabCollectionChanged);

            return iTabObjs;
        }
        public InterpolationTabUIObjects ImportInterpolationTab(long tankid, String fileName)
        {
            if ((String.IsNullOrEmpty(fileName))) return null;
            dataAccessLayer.ImportInterpolationTab(fileName, tankid);

            return GetInterpolationTab(tankid);
        }

        void InterpolationTabCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                try
                {
                    
                    foreach (object item in e.OldItems)
                    {
                        InterpolationTabUIObject iTabObject = item as InterpolationTabUIObject;

                        // use the data access layer to delete the wrapped data object
                        dataAccessLayer.DeleteInterpolationTab(iTabObject.GetDataObject());
                    }
                }
                catch (Exception ex)
                {
                    if (PersistenceError != null)
                    {
                        PersistenceError(this, ex);
                    }
                }
            }
        }

        void InterpolationTabItemEndEdit(IEditableObject sender)
        {
            InterpolationTabUIObject iTabObject = sender as InterpolationTabUIObject;

            try
            {
                if (iTabObject.TankId == 0)
                {
                    iTabObject.TankId = currTankId;
                }
                // use the data access layer to update the wrapped data object
                dataAccessLayer.UpdateInterpolationTab(iTabObject.GetDataObject());
            }
            catch (Exception ex)
            {
                if (PersistenceError != null)
                {
                    PersistenceError(this, ex);
                }
            }
        }

        public static event PersistenceErrorHandler PersistenceError;
    }

}
