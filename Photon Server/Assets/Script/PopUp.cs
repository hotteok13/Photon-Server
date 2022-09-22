using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUp : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI content;

    private static GameObject gamePanel;
    // PopUp ��ũ��Ʈ�� �������� ������ �� �ִ� �Լ�
    public static PopUp Show(string title,string massage)
    {
        if (gamePanel == null)
        {
            gamePanel = Resources.Load<GameObject>("Game Panel");
        }
        PopUp window = gamePanel.GetComponent<PopUp>();
        window.UpdateContent(title, massage);
        return window;
        
    }
//�˾��� ������ ������Ʈ�ϴ� �Լ�
    public void UpdateContent(string titleMessage, string contentMessage)
    {
        title.text = titleMessage;
        content.text = contentMessage;
    }

    //�˾��� �ݴ� �Լ�
    public void Cancle()
    {
        Destroy(gameObject);
    }
    
}
