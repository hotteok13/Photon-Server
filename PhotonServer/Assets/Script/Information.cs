using Photon.Pun;
using UnityEngine.UI;

public class Information : MonoBehaviourPunCallbacks
{
    public Text roomData;

    // string name : 방에 대한 이름
    // int currentCount : 방에 현재 접속한 인원
    // int MaxCount : 방에 최대로 접속할 수 있는 인원
    public void SetInfo(string name, int currentCount, int MaxCount)
    {
        roomData.text = name + " ( " + currentCount + " / " + MaxCount + " ) "; 
    }
}
