using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bicycle.Models;
using Bicycle.Repository;

namespace Bicycle.Manager
{
    public class BicycleManager : IBicycleManager
    {
        private readonly IBicycleRepository _bicycleRepository;
        public BicycleManager(IBicycleRepository bicycleRepository)
        {
            _bicycleRepository = bicycleRepository;
        }
        public bool AssignCycle(string userId, int cycleNum)
        {
            bool result = false;
            var model = _bicycleRepository.AssignCycle(userId, cycleNum);
            if (model != 0)
            {
                result = true;
            }
            return result;
        }

        public bool EndTrip(string userId, int cycleNum, int stationNum)
        {
            bool result = false;
            var model = _bicycleRepository.EndTrip(userId, cycleNum, stationNum);
            if (model != 0)
            {
                result = true;
            }
            return result;
        }

        public bool Login(string userId, string password)
        {
            bool result = false;
            var model = _bicycleRepository.Login(userId, password);
            if (model != null)
            {
                result = true;
            }
            return result;
            
        }

        public IEnumerable<StationModel> Stations()
        {
            List<StationModel> modelList = new List<StationModel>();

            var dataModel = _bicycleRepository.Stations();
            if (dataModel != null)
            {
                foreach (var item in dataModel)
                {
                    StationModel model = new StationModel();
                    model.lat = item.Station_Lat.Value;
                    model.lng = item.Station_Long.Value;
                    model.sname = item.Station_Name;
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        public bool UnlockCycle(string userId, int cycleNum)
        {
            bool result = false;
            var model = _bicycleRepository.UnlockCycle(userId, cycleNum);
            if (model != null && model!=0)
            {
                result = true;
            }
            return result;
        }
    }
}
