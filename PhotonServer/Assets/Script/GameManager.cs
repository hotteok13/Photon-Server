using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{

    void Start()
    {
        // 포톤 서버에서 오브젝트를 생성하는 방법
        PhotonNetwork.Instantiate
        (
            "Character", new Vector3 ( Random.Range(0, 5), 1, Random.Range(0, 5)),Quaternion.identity
        );
    }
    
    public void ExitRoom()
    {
        // PhotonNetwork.LeaveRoom() : 방을 나간다
        PhotonNetwork.LeaveRoom();
        
    }

    // 현재 플레이어가 룸에서 나갔다면 호출되는 함수
    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Photon Room");
    }

}
