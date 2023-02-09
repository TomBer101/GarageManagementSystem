using System;
using System.Collections.Generic;
using System.Dynamic;

namespace GarageManagerLibrary
{
    public class VehicleFactory
    {
        private static readonly float sr_MaximumMotorcycleFuel = 5.8f; 
        private static readonly float sr_MaximumCarFuel = 48f; 
        private static readonly float sr_MaximumCarElectricity = 2.6f; 
        private static readonly float sr_MaximumMotorcycleElectricity = 2.3f;
        private static readonly float sr_MaximumTruckFuel = 130f;

        public Vehicle CreateVehicle(int i_VehicleType)
        {
            Vehicle vehicleObject = null;

            switch(i_VehicleType)
            {
                case 1:
                    VehicleEngine feuledCarEngine = new VehicleFuelEngine(sr_MaximumCarFuel, eFuelType.Octan95);
                    vehicleObject = new Car(feuledCarEngine);
                    break;
                case 2:
                    VehicleEngine electricCarEngine = new VehicleElectricEngine(sr_MaximumCarElectricity);
                    vehicleObject = new Car(electricCarEngine);
                    break;
                case 3:
                    VehicleEngine feuledMotorcycleEngine = new VehicleFuelEngine(sr_MaximumMotorcycleFuel, eFuelType.Octan98);
                    vehicleObject = new Motorcycle(feuledMotorcycleEngine);
                    break;
                case 4:
                    VehicleEngine electricMotorcycleEngine = new VehicleElectricEngine(sr_MaximumMotorcycleElectricity);
                    vehicleObject = new Motorcycle(electricMotorcycleEngine);
                    break;
                case 5:
                    VehicleEngine feuledTruckEngine = new VehicleFuelEngine(sr_MaximumTruckFuel, eFuelType.Soler);
                    vehicleObject = new Truck(feuledTruckEngine);
                    break;
            }

            return vehicleObject;
        }
    }
}
