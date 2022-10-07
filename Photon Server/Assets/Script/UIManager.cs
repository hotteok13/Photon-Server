using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Text scoreText;
    public Text leaderBorderText;

    [SerializeField] GameObject leaderBorder;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void GetLeaderBorder()
    {
        leaderBorder.SetActive(true);

        var request = new GetLeaderboardRequest
        {
            StartPosition = 0, // �⺻ ��ġ �� 
            StatisticName = "Score", // Playfab���� �ҷ��� ����ǥ �̸�
            MaxResultsCount = 20, // ����ǥ�� �ִ�� ��Ÿ���� ��
            ProfileConstraints = new PlayerProfileViewConstraints()
            {
                ShowLocations = true,
                ShowDisplayName = true
            }
        };

        PlayFabClientAPI.GetLeaderboard(request, (result) =>
        {
            for (int i = 0; i < result.Leaderboard.Count; i++)
            {
                var currentBoader = result.Leaderboard[i];

                leaderBorderText.text += currentBoader.Profile.Locations[0].CountryCode.Value +
                " - " + currentBoader.DisplayName + " - " + currentBoader.StatValue + "\n";
            }
        },
        (error) => Debug.Log("�������带 �ҷ����� ���߽��ϴ�."));
    }
}