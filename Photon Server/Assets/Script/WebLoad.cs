using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class WebLoad : MonoBehaviour
{
    [SerializeField] RawImage [] webImage;
    void Awake()
    {
        
        // 데이터를 로드할 때
        StartCoroutine(WebImageLoad("https://raw.githubusercontent.com/Unity2033/Unity-3D-Example/main/Assets/Class/Photon%20Server/Texture/Ice%20Kingdom.jpg"));
    }

    

    private IEnumerator WebImageLoad(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            for (int i = 0; i < webImage.Length; i++)
            {
                webImage[i].texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            }
        }
    }
}
