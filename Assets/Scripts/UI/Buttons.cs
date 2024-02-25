using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public static int curMailBoxId = -1;

    public void OnClickedOpen()
    {
        if (curMailBoxId != -1)
        {
            _ = OpenMailAsync();
        }
    }

    public async Task OpenMailAsync()
    {
        await Mail.MailOpenPostAsync(curMailBoxId);

        // 로비씬 유저 데이터 UI 업데이트
        _ = UserInfoData.RequestUserInfoPostAsync();
    }
}
