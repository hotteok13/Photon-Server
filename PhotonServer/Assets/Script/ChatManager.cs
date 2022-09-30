using Photon.Pun;
using UnityEngine.UI;
using UnityEngine;

public class ChatManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField input;
    [SerializeField] GameObject chatPrefab;
    [SerializeField] Transform chatContent;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (input.text.Length == 0) return;
            string chat = PhotonNetwork.NickName + " : " + input.text;

            // RpcTarget.All : 현재 룸에 있는 모든 클라이언트에게 RpcAddChat()함수를 실행하라는 명령
            photonView.RPC("RpcAddChat", RpcTarget.All, chat);
        }
    }

    [PunRPC]
    void RpcAddChat(string message)
    {
        // ChatPrefab을 하나 만들어서 text에 값을 설정합니다.
        GameObject chat = Instantiate(chatPrefab);

        chat.GetComponent<Text>().text = message;

        // 스크롤 뷰 - content 자식으로 등록합니다.
        chat.transform.SetParent(chatContent);

        // 채팅을 입력한 후에도 이어서 입력할 수 있도록 설정합니다.
        input.ActivateInputField();

        // input 텍스트 초기화 합니다.
        input.text = "";
    }
    
}
