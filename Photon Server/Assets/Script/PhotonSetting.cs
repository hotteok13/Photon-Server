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
    
    // LoginResult <- 로그인 성공 여부 반환한다
    public void LoginSuccess(LoginResult result)
    {
        // AutomaticallySyncScene 마스터 클라이언트 씬을 동기화 할지 안할지 결정하는 기능이다
        // false = 동기화를 하지 않겠다.
        // true = 마스터 클라이언트를 기준으로 동기화를 하겠다.
        PhotonNetwork.AutomaticallySyncScene = false;

        //같은 버전의 유저끼리 접속을 허용한다.
        //같은 버전만 접속할 수 있도록 문자열 상수를 설정한다.
        PhotonNetwork.GameVersion = "1.0f";

        // 유저 아이디 설정
        PhotonNetwork.NickName = username.text;

        // 입력한 지역을 설정합니다.
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = region.options[region.value].text;

        //서버 접속
        PhotonNetwork.LoadLevel("Photon Lobby");
    }

    public void LoginFailure(PlayFabError error)
    {
        PopUp.Show("LOGIN FAILURE", "Login Failed.\nPlease Login againe");
            
    }

    public void SignUpSucces(RegisterPlayFabUserResult result)
    {
        Debug.Log("회원가입 성공");
    }

    public void SignUpFailure(PlayFabError error)
    {
        Debug.Log("회원가입 실패");
    }

    public void SignUP()
    {
        //RegisterPlayFabUserRequest : 서버에 유저를 등록하기 위한 클래스
        var request = new RegisterPlayFabUserRequest
        {
            Email = email.text,     //입력한 Email
            Password = password.text,   //입력한 password
            Username = username.text,   //입력한 UserName
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, SignUpSucces, SignUpFailure);// 회원 가입에 대한 유저 정보, 회원 가입이 성공했을 때 함수, 회원 가입이 실패했을 때 함수
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
