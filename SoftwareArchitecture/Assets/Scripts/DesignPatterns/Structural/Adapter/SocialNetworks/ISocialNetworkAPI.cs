using System.Collections.Generic;

namespace LestaAcademyDemo.DesignPatterns.Structural.Adapter.SocialNetworks
{
    public interface ISocialNetworkAPI
    {
        void Init(string apiKey);
        void Start();
        IEnumerable<ISocialNetworkFriend> GetFriends();
    }

    public interface ISocialNetworkFriend
    {
        string GetId();
        string GetFirstName();
        string GetLastName();
        string GetAvatarURL();
    }
}
