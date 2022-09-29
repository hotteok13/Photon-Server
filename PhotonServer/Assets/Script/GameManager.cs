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

  
}
