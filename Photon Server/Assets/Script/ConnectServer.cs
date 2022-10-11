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
        // for���� ���鼭 ���� ������Ʈ(ĳ����) ��ü�� ��Ȱ��ȭ�մϴ�
        for(int i = 0; i < character.Length; i++)
        {
            character[i].SetActive(false);
        }

        // character[1] Ȱ��ȭ ���� -> 
        character[DataManager.characterCount].SetActive(true);


        if (DataManager.characterCount >= 2)
        {
            DataManager.characterCount = -1;
        }
    }

    public void LeftCharacterSelect()
    {
        DataManager.characterCount--; //2
        // for���� ���鼭 ���� ������Ʈ(ĳ����) ��ü�� ��Ȱ��ȭ�մϴ�
        for (int i = 0; i < character.Length; i++)
        {
            character[i].SetActive(false);
        }

        // character[1] Ȱ��ȭ ���� -> 
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

        // ���� ����
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
