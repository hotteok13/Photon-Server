using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime; // ��� ������ �������� �� 
using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField roomName, roomPerson;
    [SerializeField] Button roomCreate, roomJoin;

    [SerializeField] GameObject roomPrefab;
    [SerializeField] Transform roomContent;

    // �ð� ���⵵�� 0(1)�� �ð� ���⵵�� �����ϴ�.
    // Dictionary : Key-Value ������ ���� ������ �� �ִ� �ڷᱸ���Դϴ�.
    Dictionary<string, RoomInfo> roomCatalog = new Dictionary<string, RoomInfo>();

    void Update()
    {
        // �� �̸��� �ϳ��� �Է��ߴٸ�
        if(roomName.text.Length > 0)
        {
            // �� ���� ��ư�� Ȱ��ȭ�մϴ�.
            roomJoin.interactable = true;
        }
        else // �� �̸��� �ϳ��� �Է����� �ʾҴٸ�
        {
            // �� ���� ��ư�� ��Ȱ��ȭ�մϴ�.
            roomJoin.interactable = false;
        }

        // OnJoinRoomFailed : �� ���ӿ� �������� �� ȣ��Ǵ� �Լ�
        // �� ���ӿ� �����ϰ� �Ǹ� �ڵ����� �ϳ� �����ؼ� ���� ����� �ֽ��ϴ�.

        // �� �̸��� �� �ο��� �Է����� ������
        if(roomName.text.Length > 0 && roomPerson.text.Length > 0)
        {
            // �� ���� ��ư Ȱ��ȭ
            roomCreate.interactable = true;
        }
        else
        {
            // �� ���� ��ư ��Ȱ��ȭ
            roomCreate.interactable = false;
        }
    }

    // ���� ���� �ִ� 10������� ������ �� �ֵ��� ������ �� �ֽ��ϴ�.
    public void OnClickCreateRoom()
    {
        // �� �ɼ��� �����մϴ�.
        RoomOptions room = new RoomOptions();

        // �ִ� �������� ���� �����մϴ�.
        room.MaxPlayers = byte.Parse(roomPerson.text);

        // ���� ���� ���θ� �����մϴ�.
        room.IsOpen = true;

        // �κ񿡼� �� ����� �����ų �� �����մϴ�.
        room.IsVisible = true;

        // ���� �����ϴ� �Լ�
        PhotonNetwork.CreateRoom(roomName.text, room);
    }

    public void OnClickJoinRoom()
    {
        PhotonNetwork.JoinRoom(roomName.text);
    }

    // ���� �����Ǿ��� �� ȣ��Ǵ� �ݹ��Լ�
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");
    }

    public void AllDeleteRoom()
    {
        // Transform ������Ʈ(Content)�� �ִ� ���� ������Ʈ�� �����Ͽ� ��ü ���� �����մϴ�.

        // element  <- [room]
        foreach (Transform element in roomContent)
        {
            // Transform�� ������ �ִ� ���� ������Ʈ�� �����մϴ�.
            Destroy(element.gameObject);
        }   
    }

    // �뿡 �������� �� ȣ��Ǵ� �ݹ��Լ�
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Photon Game");
    }

    public void CreateRoomObject()
    {
        // roomCatalog�� ���� ���� value���� �� �ִٸ� RoomInfo�� �־��ݴϴ�.
        foreach(RoomInfo info in roomCatalog.Values)
        {
            // ���� �����մϴ�.
            GameObject room = Instantiate(roomPrefab);

            // room ������Ʈ�� roomContent�� ���� ������Ʈ�� �־��ݴϴ�.
            room.transform.SetParent(roomContent);

            // �� ������ �Է��մϴ�.
            room.GetComponent<Information>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
        }    
    }

    // �� ����� ������Ʈ�ϴ� �Լ�
    public void UpdateRoom(List<RoomInfo> roomList)
    {
        // [0] <- �ݼ���
        // [1] <- �Թ��Թ�
       
        // ���� ������ ���� �ִٸ�
        for(int i = 0; i < roomList.Count; i++)
        {
            // �ش� �̸��� roomCatalog�� key ������ �����Ǿ� �ִٸ�
            if(roomCatalog.ContainsKey(roomList[i].Name))
            {
                // RemovedFromList : (true) �뿡�� ������ �Ǿ��� ��
                if(roomList[i].RemovedFromList)
                {
                    // ��ųʸ��� �ִ� �����͸� �����մϴ�.
                    roomCatalog.Remove(roomList[i].Name);
                    continue;
                }
            }

            // ���� ���ٸ� roominfo�� roomCatalog�� �߰��մϴ�.
            roomCatalog[roomList[i].Name] = roomList[i];
        }
    }

    // �ش� �κ� �� ����� ���� ������ ������ ȣ��(�߰�, ����, ����)
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        AllDeleteRoom();
        UpdateRoom(roomList);
        CreateRoomObject();
    }


}
