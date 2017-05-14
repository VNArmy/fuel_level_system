using FLS.Business;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupervisorSetting.ViewModel
{
    public delegate void PumpListPersistenceErrorHandler(PumpListDataProvider dataProvider, Exception e);
    
    public class PumpListDataProvider
    {
        private IPumpListDAL dataAccessLayer;
        private long currentStationId=0;
        public PumpListDataProvider()
        {
            dataAccessLayer = new PumpListDAL();
        }
        public PumpListUIObjects GetPumpList(long stationid)
        {
            currentStationId = stationid;
            // populate our list of customers from the data access layer
            PumpListUIObjects iTabObjs = new PumpListUIObjects();
            try
            {
                App.Log.LogDebugMessage(String.Format(@"GetALLPump w stationid = {0}", stationid));
                List<PumpListRecord> iTabDataObjects = dataAccessLayer.GetAllPump(stationid);
                
                foreach (PumpListRecord iTabDataObject in iTabDataObjects)
                {
                    // create a business object from each data object
                    PumpUIObject tobj = new PumpUIObject(iTabDataObject);
                    tobj.StationId = stationid;
                    iTabObjs.Add(tobj);
                }

                iTabObjs.PumpListItemEndEdit += new PumpListItemEndEditEventHandler(PumpListItemEndEdit);
                iTabObjs.CollectionChanged += new NotifyCollectionChangedEventHandler(PumpListCollectionChanged);
                App.Log.LogDebugMessage(String.Format(@"GetALLPump w stationid = {0} success!", stationid));
            }
            catch (Exception err)
            {
                App.Log.LogException(err);
                //throw;
            }            
            

            return iTabObjs;
        }

        void PumpListCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                try
                {

                    foreach (object item in e.OldItems)
                    {
                        PumpUIObject iTabObject = item as PumpUIObject;

                        // use the data access layer to delete the wrapped data object
                        dataAccessLayer.DeletePump(iTabObject.GetDataObject());
                    }
                }
                catch (Exception ex)
                {
                    if (PumpListPersistenceError != null)
                    {
                        PumpListPersistenceError(this, ex);
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
                        PumpUIObject iTabObject = item as PumpUIObject;
                        iTabObject.StationId = currentStationId;
                    }
                }
                catch (Exception ex)
                {
                    if (PumpListPersistenceError != null)
                    {
                        PumpListPersistenceError(this, ex);
                    }
                    App.Log.LogException(ex); 

                }
            }
        }

        void PumpListItemEndEdit(System.ComponentModel.IEditableObject sender)
        {
            //throw new NotImplementedException();
            PumpUIObject iTabObject = sender as PumpUIObject;

            try
            {
                //if (iTabObject.PumpId == 0)
                //{
                //    iTabObject.PumpId = currPumpId;
                //}
                // use the data access layer to update the wrapped data object
                dataAccessLayer.UpdatePump(iTabObject.GetDataObject());
                iTabObject.NotifyIdChange();
            }
            catch (Exception ex)
            {
                if (PumpListPersistenceError != null)
                {
                    PumpListPersistenceError(this, ex);
                }
                App.Log.LogException(ex); App.Log.LogInfoMessage(ex.Message);

            }
        }

        public static event PumpListPersistenceErrorHandler PumpListPersistenceError;
    }
}
