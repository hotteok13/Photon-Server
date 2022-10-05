using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    
    //public Text scoreText;
    //public Text leaderBorderText;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

   
}
