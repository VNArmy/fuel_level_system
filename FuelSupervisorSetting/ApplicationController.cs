using FuelSupervisorSetting.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
public enum ApplicationPage
{
    Station,
    Tank,
    Interpolation,
}
namespace FuelSupervisorSetting
{
    class ApplicationController
    {
        static ApplicationController _instance;
        public static ApplicationController GetInstance()
        {
            if (_instance == null)
                _instance = new ApplicationController();
            return _instance;
        }

        Border _stage;

        private ApplicationController() { }

        public void GoToPage(ApplicationPage page)
        {
            switch (page)
            {
                case ApplicationPage.Station:
                    _stage.Child = new StationControl();
                    break;
                case ApplicationPage.Tank:
                    _stage.Child = new TankListControl();
                    
                    break;
                case ApplicationPage.Interpolation:
                    _stage.Child = new Interpolation();

                    break;
            }
        }

        public void SetStage(Border Stage)
        {
            _stage = Stage;
        }

    }

}
