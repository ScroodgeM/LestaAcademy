//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.Pillars.Abstraction
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
            /* some actions here */
        }

        private void GrindGrains()
        {
            /* some actions here */
        }

        private void TurnOnPump()
        {
            /* some actions here */
        }

        private void TurnOffPump()
        {
            /* some actions here */
        }

        private void CleanBrewingUnit()
        {
            /* some actions here */
        }
    }
}
