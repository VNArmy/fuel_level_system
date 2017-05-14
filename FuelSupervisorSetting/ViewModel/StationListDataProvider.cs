using FLS.Business;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FuelSupervisorSetting.ViewModel
{
    public delegate void StationListPersistenceErrorHandler(StationListDataProvider dataProvider, Exception e);

    public class StationListDataProvider
    {
        IStationListDAL dataAccessLayer;
        public StationListDataProvider()
        {
            dataAccessLayer = new StationListDAL();
        }

        public StationUIObjects GetStationList()
        {
            
            // populate our list of customers from the data access layer
            StationUIObjects iTabObjs = new StationUIObjects();

            List<StationListRecord> iTabDataObjects = dataAccessLayer.GetAllStation();
            foreach (StationListRecord iTabDataObject in iTabDataObjects)
            {
                // create a business object from each data object
                iTabObjs.Add(new StationUIObject(iTabDataObject));
            }

            iTabObjs.StationListItemEndEdit += new StationListItemEndEditEventHandler(StationListItemEndEdit);
            iTabObjs.CollectionChanged += new NotifyCollectionChangedEventHandler(StationListCollectionChanged);

            return iTabObjs;
        }

        void StationListCollectionChanged (object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                try
                {
                    
                    foreach (object item in e.OldItems)
                    {
                        StationUIObject iTabObject = item as StationUIObject;

                        // use the data access layer to delete the wrapped data object
                        dataAccessLayer.DeleteStation(iTabObject.GetDataObject());
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

        void StationListItemEndEdit(IEditableObject sender)
        {
            StationUIObject iTabObject = sender as StationUIObject;

            try
            {
                //if (iTabObject.TankId == 0)
                //{
                //    iTabObject.TankId = currTankId;
                //}
                // use the data access layer to update the wrapped data object
                dataAccessLayer.UpdateStation(iTabObject.GetDataObject());
                iTabObject.NotifyIdChange();
            }
            catch (Exception ex)
            {
                if (PersistenceError != null)
                {
                    PersistenceError(this, ex);
                }
            }
        }

        public static event StationListPersistenceErrorHandler PersistenceError;
    }

}
