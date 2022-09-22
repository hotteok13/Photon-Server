using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUp : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI content;

    private static GameObject gamePanel;
    // PopUp 스크립트로 전역에서 접근할 수 있는 함수
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
//팝업의 내용을 업데이트하는 함수
    public void UpdateContent(string titleMessage, string contentMessage)
    {
        title.text = titleMessage;
        content.text = contentMessage;
    }

    //팝업을 닫는 함수
    public void Cancle()
    {
        Destroy(gameObject);
    }
    
}
