using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Billboard : MonoBehaviourPun
{
    [SerializeField] Text nickName;

    void Start()
    {
        // �ڽ��� �г������� �����ϴ� �����Դϴ�.
         nickName.text = photonView.Owner.NickName;
    }

    void Update()
    {
        // �ڱ� ���� ���⿡ ī�޶��� �� ������ �����Ͽ� �ٶ� �� �ֵ��� �����մϴ�.
        transform.forward = Camera.main.transform.forward;
    }
}