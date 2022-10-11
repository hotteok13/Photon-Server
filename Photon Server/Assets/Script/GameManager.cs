using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        switch (DataManager.characterCount)
        {
            case 1:
                CreateCharacter("Character");
                break;
            case 2:
                CreateCharacter("Character2");
                break;
            case 3:
                CreateCharacter("Character3");
                break;
        }
        // ���� �������� ������Ʈ�� �����ϴ� ���
        
    
    }

    public void CreateCharacter(string name)
    {
        PhotonNetwork.Instantiate(name, new Vector3(Random.Range(0, 5), 1, Random.Range(0, 5)), Quaternion.identity);
    }

    public void ExitRoom()
    {
        // PhotonNetwork.LeaveRoom() : ���� �뿡�� ������ �Լ��Դϴ�.
        PhotonNetwork.LeaveRoom();
    }

    // ���� �÷��̾ �뿡�� �����ٸ� ȣ��Ǵ� �Լ�
    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Photon Room");
    }

}
