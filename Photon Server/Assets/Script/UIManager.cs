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
        // L vlaue와 R value
        // L value : 표현식 이후에도 사라지지 않은 값 (메모리 공간을 가지고 있는 변수)
        // R value : 표현식 이후에는 사라지는 값 (임시 변수)

        int count = 30;

        // l value (box) <- r value =(count+10)
        int box = (count + 10);
    }

    public void GetLeaderBorder()
    {
        leaderBorder.SetActive(true);

        var request = new GetLeaderboardRequest
        {
            StartPosition = 0, // 기본 위치 값 
            StatisticName = "Score", // Playfab에서 불러올 순위표 이름
            MaxResultsCount = 20, // 순위표에 최대로 나타나는 수
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
        (error) => Debug.Log("리더보드를 불러오지 못했습니다."));
    }

    public void GetVirtualCurrency()
    {
        var request = new AddUserVirtualCurrencyRequest()
        {
            VirtualCurrency = "NP",//Playfab예 설정한 통화 코드
            Amount = 100,//추가
        };

        PlayFabClientAPI.AddUserVirtualCurrency(request, (result) => Debug.Log("돈이 추가됨"), (error) => Debug.Log("오류 발생"));
    }
}
