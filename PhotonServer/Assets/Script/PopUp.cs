using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUp : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI content;

    private static GameObject gamePanel;

    // PopUp 스크립트로 전역에서 접근할 수 있는 함수
    public static PopUp Show(string title, string message)
    {
        // gamePanel이 아직 생성이 되지 않았다면
        if (gamePanel == null)
        {
            // Resources 폴더에 있는 Game Panel을 생성합니다.
            gamePanel = Resources.Load<GameObject>("Game Panel");
        }

        GameObject obj = Instantiate(gamePanel);

        PopUp window = obj.GetComponent<PopUp>();
        window.UpdateContent(title, message);

        return window;
    }

    // 팝업의 내용을 업데이트하는 함수
    public void UpdateContent(string titleMessage, string contentMessage)
    {
        title.text = titleMessage;
        content.text = contentMessage;
    }

    // 팝업을 닫는 함수
    public void Cancle()
    {
        Destroy(gameObject);
    }
}
