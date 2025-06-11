namespace VolvoWebApp.Enums
{
    public enum VehicleType
    {
        Bus,
        Truck,
        Car,
    }

    public static class VehicleTypeExtensions
    {
        public static uint NumberOfPassengers(this VehicleType vehicleType)
        {
            uint result = 0;
            switch (vehicleType)
            {
                case VehicleType.Bus:
                    result = 42;
                    break;
                case VehicleType.Truck:
                    result = 1;
                    break;
                case VehicleType.Car:
                    result = 4;
                    break;
            }
            return result;
        }
    }
}
