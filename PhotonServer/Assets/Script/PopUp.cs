using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUp : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI content;

    private static GameObject gamePanel;

    // PopUp ��ũ��Ʈ�� �������� ������ �� �ִ� �Լ�
    public static PopUp Show(string title, string message)
    {
        // gamePanel�� ���� ������ ���� �ʾҴٸ�
        if (gamePanel == null)
        {
            // Resources ������ �ִ� Game Panel�� �����մϴ�.
            gamePanel = Resources.Load<GameObject>("Game Panel");
        }

        GameObject obj = Instantiate(gamePanel);

        PopUp window = obj.GetComponent<PopUp>();
        window.UpdateContent(title, message);

        return window;
    }

    // �˾��� ������ ������Ʈ�ϴ� �Լ�
    public void UpdateContent(string titleMessage, string contentMessage)
    {
        title.text = titleMessage;
        content.text = contentMessage;
    }

    // �˾��� �ݴ� �Լ�
    public void Cancle()
    {
        Destroy(gameObject);
    }
}
