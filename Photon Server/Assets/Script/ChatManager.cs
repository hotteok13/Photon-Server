using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ChatManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField input;
    [SerializeField] GameObject chatPrefab;
    [SerializeField] Transform chatContent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (input.text.Length == 0) return;

            // InputField�� �ִ� �ؽ�Ʈ�� �����ɴϴ�.
            string chat = PhotonNetwork.NickName + " : " + input.text;

            // RpcTargat.All : ���� �뿡 �ִ� ��� Ŭ���̾�Ʈ���� RpcAddChat()�Լ��� �����϶�� ����Դϴ�.
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

        // input �ؽ�Ʈ �ʱ�ȭ�մϴ�.
        input.text = "";
    }
}
