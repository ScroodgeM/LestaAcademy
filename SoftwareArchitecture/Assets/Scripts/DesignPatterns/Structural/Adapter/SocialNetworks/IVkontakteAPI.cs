
using System.Collections.Generic;

namespace WGADemo.DesignPatterns.Structural.Adapter.SocialNetworks
{
    public interface IVkontakteAPI
    {
        void Go(string apiKey);
        string GetAvatarURL(string friendId);
        int GetFriendPagesCount();
        List<IVkontakteFriend> GetFriends(int pageIndex);
    }

    public interface IVkontakteFriend
    {
        string Id { get; }
        string[] GetName();
    }
}
