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
        // 현재 플레이어가 나 자신이라면
        if(photonView.IsMine)
        {
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            cam.enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
        }
    }

    void Update()
    {
        // 현재 플레이어가 나 자신이 아니라면
        if (!photonView.IsMine)
        {
            return; 
        }

        Vector3 direction = new Vector3
        (
           Input.GetAxis("Horizontal"),
           0,
           Input.GetAxis("Vertical")          
        );

        transform.Translate(direction.normalized * speed * Time.deltaTime);

        transform.eulerAngles += new Vector3
        (
            0,
            Input.GetAxis("Mouse X") * angleSpeed * Time.deltaTime,
            0
        );
    }
}
