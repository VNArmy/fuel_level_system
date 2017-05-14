using FLS.Business;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupervisorSetting.ViewModel
{
    public delegate void TankListPersistenceErrorHandler(TankListDataProvider dataProvider, Exception e);
    
    public class TankListDataProvider
    {
        private ITankListDAL dataAccessLayer;
        private long currentStationId=0;
        public TankListDataProvider()
        {
            dataAccessLayer = new TankListDAL();
        }
        public TankListUIObjects GetTankList(long stationid)
        {
            currentStationId = stationid;
            // populate our list of customers from the data access layer
            TankListUIObjects iTabObjs = new TankListUIObjects();
            try
            {
                App.Log.LogDebugMessage(String.Format(@"GetALLTank w stationid = {0}", stationid));
                List<TankListRecord> iTabDataObjects = dataAccessLayer.GetAllTank(stationid);
                
                foreach (TankListRecord iTabDataObject in iTabDataObjects)
                {
                    // create a business object from each data object
                    TankUIObject tobj = new TankUIObject(iTabDataObject);
                    tobj.StationId = stationid;
                    iTabObjs.Add(tobj);
                }

                iTabObjs.TankListItemEndEdit += new TankListItemEndEditEventHandler(TankListItemEndEdit);
                iTabObjs.CollectionChanged += new NotifyCollectionChangedEventHandler(TankListCollectionChanged);
                App.Log.LogDebugMessage(String.Format(@"GetALLTank w stationid = {0} success!", stationid));
            }
            catch (Exception err)
            {
                App.Log.LogException(err);
                //throw;
            }            
            

            return iTabObjs;
        }

        void TankListCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                try
                {

                    foreach (object item in e.OldItems)
                    {
                        TankUIObject iTabObject = item as TankUIObject;

                        // use the data access layer to delete the wrapped data object
                        dataAccessLayer.DeleteTank(iTabObject.GetDataObject());
                    }
                }
                catch (Exception ex)
                {
                    if (TankListPersistenceError != null)
                    {
                        TankListPersistenceError(this, ex);
                    }
                    App.Log.LogException(ex); 

                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                try
                {
                    foreach (object item in e.NewItems)
                    {
                        TankUIObject iTabObject = item as TankUIObject;
                        iTabObject.StationId = currentStationId;
                    }
                }
                catch (Exception ex)
                {
                    if (TankListPersistenceError != null)
                    {
                        TankListPersistenceError(this, ex);
                    }
                    App.Log.LogException(ex); 

                }
            }
        }

        void TankListItemEndEdit(System.ComponentModel.IEditableObject sender)
        {
            //throw new NotImplementedException();
            TankUIObject iTabObject = sender as TankUIObject;

            try
            {
                //if (iTabObject.TankId == 0)
                //{
                //    iTabObject.TankId = currTankId;
                //}
                // use the data access layer to update the wrapped data object
                dataAccessLayer.UpdateTank(iTabObject.GetDataObject());
                iTabObject.NotifyIdChange();
            }
            catch (Exception ex)
            {
                if (TankListPersistenceError != null)
                {
                    TankListPersistenceError(this, ex);
                }
                App.Log.LogException(ex); App.Log.LogInfoMessage(ex.Message);

            }
        }

        public static event TankListPersistenceErrorHandler TankListPersistenceError;
    }
}
