using System;

namespace GarageLogic
{
    public class Program
    {
        public static void Main()
        {
            GarageManagerSystem garageManagerSystem = new GarageManagerSystem();
            garageManagerSystem.StartGarageManager();
            Console.ReadLine();
        }
    }
}
