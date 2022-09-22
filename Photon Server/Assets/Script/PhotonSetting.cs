using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class PhotonSetting : MonoBehaviour
{
    [SerializeField] InputField email;
    [SerializeField] InputField password;
    [SerializeField] InputField username;
    [SerializeField] Dropdown region;

    void Start()
    {
        
    }
    
    // LoginResult <- �α��� ���� ���� ��ȯ�Ѵ�
    public void LoginSuccess(LoginResult result)
    {
        // AutomaticallySyncScene ������ Ŭ���̾�Ʈ ���� ����ȭ ���� ������ �����ϴ� ����̴�
        // false = ����ȭ�� ���� �ʰڴ�.
        // true = ������ Ŭ���̾�Ʈ�� �������� ����ȭ�� �ϰڴ�.
        PhotonNetwork.AutomaticallySyncScene = false;

        //���� ������ �������� ������ ����Ѵ�.
        //���� ������ ������ �� �ֵ��� ���ڿ� ����� �����Ѵ�.
        PhotonNetwork.GameVersion = "1.0f";

        // ���� ���̵� ����
        PhotonNetwork.NickName = username.text;

        // �Է��� ������ �����մϴ�.
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = region.options[region.value].text;

        //���� ����
        PhotonNetwork.LoadLevel("Photon Lobby");
    }

    public void LoginFailure(PlayFabError error)
    {
        PopUp.Show("LOGIN FAILURE", "Login Failed.\nPlease Login againe");
            
    }

    public void SignUpSucces(RegisterPlayFabUserResult result)
    {
        Debug.Log("ȸ������ ����");
    }

    public void SignUpFailure(PlayFabError error)
    {
        Debug.Log("ȸ������ ����");
    }

    public void SignUP()
    {
        //RegisterPlayFabUserRequest : ������ ������ ����ϱ� ���� Ŭ����
        var request = new RegisterPlayFabUserRequest
        {
            Email = email.text,     //�Է��� Email
            Password = password.text,   //�Է��� password
            Username = username.text,   //�Է��� UserName
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, SignUpSucces, SignUpFailure);// ȸ�� ���Կ� ���� ���� ����, ȸ�� ������ �������� �� �Լ�, ȸ�� ������ �������� �� �Լ�
    }

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email.text,
            Password = password.text,
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, LoginSuccess, LoginFailure);
    }

}
