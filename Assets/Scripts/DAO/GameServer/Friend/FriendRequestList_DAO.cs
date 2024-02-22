using System.ComponentModel;

namespace Assets.Scripts.DAO.GameServer
{
    public class FriendRequestElement
    {
        public long RequestId { get; set; }
        public string FromUserName { get; set; }
        public string ToUserName { get; set; }
    }
}