
namespace WGADemo.DesignPatterns.Structural.Adapter.SocialNetworks
{
    public class VkontakteFriendAdapter : ISocialNetworkFriend
    {
        private readonly IVkontakteFriend vkontakteFriend;
        private readonly string avatarURL;

        public VkontakteFriendAdapter(IVkontakteFriend vkontakteFriend, string avatarURL)
        {
            this.vkontakteFriend = vkontakteFriend;
            this.avatarURL = avatarURL;
        }

        public string GetAvatarURL()
        {
            return avatarURL;
        }

        public string GetFirstName()
        {
            return vkontakteFriend.GetName()[0];
        }

        public string GetLastName()
        {
            return vkontakteFriend.GetName()[1];
        }

        public string GetId()
        {
            return vkontakteFriend.Id;
        }
    }
}
