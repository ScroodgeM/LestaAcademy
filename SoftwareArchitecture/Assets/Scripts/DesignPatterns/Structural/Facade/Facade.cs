//this empty line for UTF-8 BOM header

namespace LestaAcademyDemo.DesignPatterns.Structural.Facade
{
    public class Facade : IFacade
    {
        private IAudioManager audioManager;
        private IPlayerInput playerInput;
        private IUserInterface userInterface;

        public void SelectUnit(IUnit unit)
        {
            IUnitView unitView = null; // resolve this unit's view here

            userInterface.ShowUnitActionButtons(unit);
            userInterface.ShowUnitName(unit.GetDisplayName());
            audioManager.PlaySound("unit_selected");
            playerInput.SetInputReceiver(unit);
            unitView.SetSelectionGlow(true);
        }
    }
}
