using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;

public class PhotonControl : MonoBehaviourPun, IPunObservable
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] float angleSpeed;

    private Animator animator;
    public int score;
    [SerializeField] Camera cam;

    void Start()
    {
        animator = GetComponent<Animator>();
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


        if (Input.GetButton("Fire1"))
        {
            animator.SetTrigger("Attack");
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

    // 실시간으로 데이터를 처리하기 위한 동기화 함수
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //로컬 오브젝트라면 쓰기 부분이 실행됩니다
        stream.SendNext(score);
        if (stream.IsWriting)
        {
            // 네트워크를 통해 score 값을 보냅니다.
            stream.SendNext(score);
        }
        else// 원격 오브젝트라면 읽기 부분이 실행됩니다.
        {
            score = (int)stream.ReceiveNext();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Crystal(Clone)")
        {

            if (photonView.IsMine)
            {
                score++;
            }

            PlayFabDataSave();
           

            PhotonView view = other.gameObject.GetComponent<PhotonView>();

            if (view.IsMine)
            {
                // 충돌한 물체가 네트워크 객체라면
                // 충돌당한 네트워크 객체를 파괴합니다.
                PhotonNetwork.Destroy(other.gameObject);
            }
        }
    }

    public void PlayFabDataSave()
    {
        PlayFabClientAPI.UpdatePlayerStatistics(
            new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>
                    {
                        new StatisticUpdate
                        {
                            StatisticName="Score",Value=score
                        },
                    }
            },
            (result) => { Debug.Log("값 저장 성공"); },
            (error) => { Debug.Log("값 저장 실패"); }
        );
    }
}
