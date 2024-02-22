using System.Collections.Generic;
using System.ComponentModel;
using Assets.Scripts.DAO.GameServer;
using Assets.Scripts.DTO;

namespace Assets.Scripts.DTO.GameServer
{
    //  요청 데이터

    // 응답 데이터
    public class FriendListRes : ErrorCodeDTO
    {
        public IEnumerable<FriendElement> FriendList { get; set; } // DAO의 친구 요소로 이루어진 List를 반환
    }
}