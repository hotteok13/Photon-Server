using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonControl : MonoBehaviourPun
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] float angleSpeed;

    [SerializeField] Camera cam;
    void Start()
    {
        //���� �÷��̾ �� �ڽ��̶��
        if (photonView.IsMine)
        {
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            cam.enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //���� �÷��̾ �ڽ��� �ƴ϶��
        if (!photonView.IsMine)
        {
            //���������ʰ� �����Ѵ�
            return;
        }
        //��ġ �̵�
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // ī�޶� �̵�
        transform.eulerAngles += new Vector3(0, Input.GetAxis("Mouse X") * angleSpeed * Time.deltaTime, 0);
    }
}
