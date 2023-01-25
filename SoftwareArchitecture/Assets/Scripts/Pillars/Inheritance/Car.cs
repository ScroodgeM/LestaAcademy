
namespace WGADemo.Pillars.Inheritance
{
    public class Car
    {
        public void StartEngine() { /* some actions here */ }
        public void Drive() { /* some actions here */ }
        public void StopEngine() { /* some actions here */ }
    }

    public class Cabriolet : Car
    {
        public void SetTopState(bool attached) { /* some actions here */ }
    }

    public class OffRoadVehicle : Car
    {
        public void SetLoweringGear(bool enabled) { /* some actions here */ }
    }
}
