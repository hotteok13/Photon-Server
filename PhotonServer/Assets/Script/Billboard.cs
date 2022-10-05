using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
public class Billboard : MonoBehaviourPun
{
    [SerializeField] Text nickName;

    void Start()
    {
        // 자신의 닉네임으로 설정하는 변수입니다.
        nickName.text = photonView.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {

        transform.forward = Camera.main.transform.forward;      
    }
}
