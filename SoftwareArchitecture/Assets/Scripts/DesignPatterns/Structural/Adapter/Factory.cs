
namespace WGADemo.DesignPatterns.Structural.Adapter
{
    public class Factory
    {
        public enum SocialNetworkType
        {
            VK, FB
        }

        public ISocialNetworkAPI GetSocialNetworkAPI(SocialNetworkType socialNetworkType)
        {
            switch (socialNetworkType)
            {
                case SocialNetworkType.VK:
                    IVkontakteAPI vkontakteAPI = null; // resolve vkontakte implementation here
                    ISocialNetworkAPI vkontakteAdapter = new VkontakteAdapter(vkontakteAPI);
                    return vkontakteAdapter;

                case SocialNetworkType.FB:
                    ISocialNetworkAPI facebookAPI = null; // resolve facebook implementation here
                    return facebookAPI;

                default:
                    throw new System.NotSupportedException();
            }
        }
    }
}
