
using System;

namespace WGADemo.Pillars.Abstraction
{
    public class CoffeeMachine
    {
        bool turnedOn = false;

        public void TurnOn()
        {
            // say "hello"
            turnedOn = true;
        }

        public void TurnOff()
        {
            // say "good bye"
            turnedOn = false;
        }

        public void MakeCoffee(float cupVolume)
        {
            if (turnedOn == false)
            {
                // just do nothing
                return;
            }

            BoilWater();
            GrindGrains();
            TurnOnPump();
            //wait for some time here - depends on cupVolume
            TurnOffPump();
            CleanBrewingUnit();
        }

        private void BoilWater()
        {
            throw new NotImplementedException();
        }

        private void GrindGrains()
        {
            throw new NotImplementedException();
        }

        private void TurnOnPump()
        {
            throw new NotImplementedException();
        }

        private void TurnOffPump()
        {
            throw new NotImplementedException();
        }

        private void CleanBrewingUnit()
        {
            throw new NotImplementedException();
        }
    }
}
