using UnityEngine;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;

public class PhotonControl : MonoBehaviourPun, IPunObservable
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] float angleSpeed;

    [SerializeField] Camera cam;

    private Animator animator;

    public int score;

    void Start()
    {
        animator = GetComponent<Animator>();

        // ���� �÷��̾ �� �ڽ��̶��
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

    void Update()
    {
        // ���� �÷��̾ �� �ڽ��� �ƴ϶��
        if (!photonView.IsMine)
        {
            return; 
        }

        if (Input.GetButtonDown("Fire1"))
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Crystal(Clone)")
        {
            if (photonView.IsMine)
            {
                score++;
            }

            PlayFabDataSave();

            PhotonView view = other.gameObject.GetComponent<PhotonView>();

            if(view.IsMine) // �浹�� ��ü�� �ڱ� �ڽ��̶��
            {             
                // �浹���� ��Ʈ��ũ ��ü�� �ı��մϴ�.
                PhotonNetwork.Destroy(other.gameObject);
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // ���� ������Ʈ��� ���� �κ��� ����˴ϴ�.
        if (stream.IsWriting)
        {
            // ��Ʈ��ũ�� ���� score ���� �����ϴ�.
            stream.SendNext(score);
        }
        else // ���� ������Ʈ��� �б� �κ��� ����˴ϴ�.
        {
            score = (int)stream.ReceiveNext();
        }
    }

    public void PlayFabDataSave()
    {
        PlayFabClientAPI.UpdatePlayerStatistics
        (
            new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>
                {
                    new StatisticUpdate
                    {
                         StatisticName = "Score", Value = score
                    },
                }
            },
            (result) => { UIManager.instance.scoreText.text = "Current Crystal : " + score.ToString(); },
            (error) => { UIManager.instance.scoreText.text = "No value Saved"; }
        );
    }
}
