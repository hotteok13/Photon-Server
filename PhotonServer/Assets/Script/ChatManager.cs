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

            // RpcTarget.All : ���� �뿡 �ִ� ��� Ŭ���̾�Ʈ���� RpcAddChat()�Լ��� �����϶�� ���
            photonView.RPC("RpcAddChat", RpcTarget.All, chat);
        }
    }

    [PunRPC]
    void RpcAddChat(string message)
    {
        // ChatPrefab�� �ϳ� ���� text�� ���� �����մϴ�.
        GameObject chat = Instantiate(chatPrefab);

        chat.GetComponent<Text>().text = message;

        // ��ũ�� �� - content �ڽ����� ����մϴ�.
        chat.transform.SetParent(chatContent);

        // ä���� �Է��� �Ŀ��� �̾ �Է��� �� �ֵ��� �����մϴ�.
        input.ActivateInputField();

        // input �ؽ�Ʈ �ʱ�ȭ �մϴ�.
        input.text = "";
    }
    
}
