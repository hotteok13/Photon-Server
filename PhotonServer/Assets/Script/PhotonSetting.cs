using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.EventSystems;

public class PhotonSetting : MonoBehaviour
{
    EventSystem eventSystem;
    public Selectable firstInput;

    [SerializeField] InputField email;
    [SerializeField] InputField password;
    [SerializeField] InputField username;
    [SerializeField] Dropdown region;

    private void Awake()
    {
        eventSystem = EventSystem.current;
        // ó���� Email Input Field�� �����ϵ��� �����մϴ�.
        firstInput.Select();

        PlayFabSettings.TitleId = "8AED9";
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            // TAB + LeftShift�� ���� Selectable ��ü�� �����մϴ�.
            Selectable next = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
       
            if(next != null)
            {
                next.Select();
            }
        }
        else if(Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();

            if (next != null)
            {
                next.Select();
            }
        }
    }

    //  LoginResult <- �α��� ���� ���� ��ȯ�մϴ�.
    public void LoginSuccess(LoginResult result)
    {
        // AutomaticallySyncScene ������ Ŭ���̾�Ʈ�� �������� ���� ����ȭ���� ������ �����ϴ� ����Դϴ�.
        // false = ����ȭ�� ���� �ʰڴ�.
        // true = ������ Ŭ���̾�Ʈ�� �������� ����ȭ�� �ϰڴ�.
        PhotonNetwork.AutomaticallySyncScene = false;

        // ���� ������ �������� ������ ����մϴ�.
        // ���� ������ ������ �� �ֵ��� ���ڿ� ����� �����մϴ�.
        PhotonNetwork.GameVersion = "1.0f";

        // ���� ���̵� ����
        PhotonNetwork.NickName = username.text;

        // �Է��� ������ �����մϴ�.
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = region.options[region.value].text;

        // ���� ����
        PhotonNetwork.LoadLevel("Photon Lobby");
    }

    public void LoginFailure(PlayFabError error)
    {
        PopUp.Show
        (
            "LOGIN FAILURE",
            "Login Failed.\nPlease login again"
        );
    }

    public void SignUpSuccess(RegisterPlayFabUserResult result)
    {
        PopUp.Show
        (
           "MEMBERSHIP\nSUCCESSFUL",
           "Congratulations on your\nSuccessful Membership Registration"
        );
    }

    public void SignUpFailure(PlayFabError error)
    {
        Debug.Log("ȸ�� ���� ����");
    }

    public void SignUp()
    {
        // RegisterPlayFabUserRequest : ������ ������ ����ϱ� ���� Ŭ����
        var request = new RegisterPlayFabUserRequest
        {
            Email = email.text,       // �Է��� Email
            Password = password.text, // �Է��� ��й�ȣ
            Username = username.text  // �Է��� ���� �̸�
        };

        PlayFabClientAPI.RegisterPlayFabUser
        (
               request,       // ȸ�� ���Կ� ���� ���� ����
               SignUpSuccess, // ȸ�� ������ �������� �� ȸ�� ���� ���� �Լ� ȣ��
               SignUpFailure  // ȸ�� ������ �������� �� ȸ�� ���� ���� �Լ� ȣ��
        );
    }

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email.text,
            Password = password.text 
        };

        PlayFabClientAPI.LoginWithEmailAddress
        (
            request,
            LoginSuccess,
            LoginFailure
        );

    }
}
