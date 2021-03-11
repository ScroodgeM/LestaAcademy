using System.Collections.Generic;

namespace WGADemo.DesignPatterns.Structural.Adapter
{
    public class VkontakteAdapter : ISocialNetworkAPI
    {
        private readonly IVkontakteAPI vkontakteAPI;

        private string apiKey;

        public VkontakteAdapter(IVkontakteAPI vkontakteAPI)
        {
            this.vkontakteAPI = vkontakteAPI;
        }

        public void Init(string apiKey)
        {
            this.apiKey = apiKey;
        }
        public void Start()
        {
            vkontakteAPI.Go(apiKey);
        }

        public IEnumerable<ISocialNetworkFriend> GetFriends()
        {
            int pagesCount = vkontakteAPI.GetFriendPagesCount();

            for (int i = 0; i < pagesCount; i++)
            {
                foreach (IVkontakteFriend vkontakteFriend in vkontakteAPI.GetFriends(i))
                {
                    string avatarURL = vkontakteAPI.GetAvatarURL(vkontakteFriend.Id);

                    yield return new VkontakteFriendAdapter(vkontakteFriend, avatarURL);
                }
            }
        }
    }
}
