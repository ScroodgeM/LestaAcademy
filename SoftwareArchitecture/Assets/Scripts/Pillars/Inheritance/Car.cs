
using System;

namespace WGADemo.Pillars.Inheritance
{
    public class Car
    {
        public void StartEngine() { throw new NotImplementedException(); }
        public void Drive() { throw new NotImplementedException(); }
        public void StopEngine() { throw new NotImplementedException(); }
    }

    public class Cabriolet : Car
    {
        public void SetTopState(bool attached) { throw new NotImplementedException(); }
    }

    public class OffRoadVehicle : Car
    {
        public void SetLoweringGear(bool enabled) { throw new NotImplementedException(); }
    }
}
