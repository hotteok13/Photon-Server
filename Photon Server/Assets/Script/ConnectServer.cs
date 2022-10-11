using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnectServer : MonoBehaviourPunCallbacks
{
    private string serverName;
    [SerializeField] GameObject[] character;

    private void Start()
    {
        character[DataManager.characterCount].SetActive(true);
    }
    
    public void RightCharacterSelect()
    {
        DataManager.characterCount++; //1
        // for문을 돌면서 게임 오브젝트(캐릭터) 전체를 비활성화합니다
        for(int i = 0; i < character.Length; i++)
        {
            character[i].SetActive(false);
        }

        // character[1] 활성화 상태 -> 
        character[DataManager.characterCount].SetActive(true);


        if (DataManager.characterCount >= 2)
        {
            DataManager.characterCount = -1;
        }
    }

    public void LeftCharacterSelect()
    {
        DataManager.characterCount--; //2
        // for문을 돌면서 게임 오브젝트(캐릭터) 전체를 비활성화합니다
        for (int i = 0; i < character.Length; i++)
        {
            character[i].SetActive(false);
        }

        // character[1] 활성화 상태 -> 
        character[DataManager.characterCount].SetActive(true);


        if (DataManager.characterCount <= 0)
        {
            DataManager.characterCount = 3;
        }
    }

    public void SelectLobby(string text)
    {
        // Challeger Server
        serverName = text;

        // 서버 접속
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.LoadLevel("Photon Room");
    }

    public override void OnConnectedToMaster()
    {
        // Challenger Server
        PhotonNetwork.JoinLobby(new TypedLobby(serverName, LobbyType.Default));
    }
}
