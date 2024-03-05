//this empty line for UTF-8 BOM header

using LestaAcademyDemo.DesignPatterns.Structural.Adapter.SocialNetworks;
using LestaAcademyDemo.DesignPatterns.Structural.Adapter.Units;

namespace LestaAcademyDemo.DesignPatterns.Structural.Adapter
{
    public class Factory
    {
        public enum SocialNetworkType
        {
            VK,
            NonVK,
        }

        public enum PlayerUnitType
        {
            Character,
            Car,
        }

        public enum NPCUnitType
        {
            Car,
        }

        public ISocialNetworkAPI GetSocialNetworkAPI(SocialNetworkType socialNetworkType)
        {
            switch (socialNetworkType)
            {
                case SocialNetworkType.VK:
                    IVkontakteAPI vkontakteAPI = null; // resolve vkontakte implementation here
                    ISocialNetworkAPI vkontakteAdapter = new VkontakteAdapter(vkontakteAPI);
                    return vkontakteAdapter;

                case SocialNetworkType.NonVK:
                    ISocialNetworkAPI nonVkAPI = null; // resolve non-vk implementation here
                    return nonVkAPI;

                default:
                    throw new System.NotSupportedException();
            }
        }

        public IPlayerControllable GetPlayerUnit(PlayerUnitType playerUnitType)
        {
            switch (playerUnitType)
            {
                case PlayerUnitType.Character:
                    IPlayerControllable character = null; // resolve character implementation here
                    return character;

                case PlayerUnitType.Car:
                    IPlayerControllable vehicle = new PlayerControllableVehicle(); // replace it with valid constructor
                    return vehicle;

                default:
                    throw new System.NotSupportedException();
            }
        }

        public INPC GetNPCUnit(NPCUnitType npcUnitType)
        {
            switch (npcUnitType)
            {
                case NPCUnitType.Car:
                    INPC vehicle = new PlayerControllableVehicle(); // replace it with valid constructor
                    return vehicle;

                default:
                    throw new System.NotSupportedException();
            }
        }
    }
}
