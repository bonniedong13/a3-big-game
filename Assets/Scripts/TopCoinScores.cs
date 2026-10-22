using TMPro;
using UnityEngine;

public class TopCoinScores : MonoBehaviour
{
    public TMP_Text scoreText;

    private void Start()
    {
        ShowScores();
    }

    public void RecordRun()
    {
        int[] scores =
        {
            PlayerPrefs.GetInt("TopCoin0", 0),
            PlayerPrefs.GetInt("TopCoin1", 0),
            PlayerPrefs.GetInt("TopCoin2", 0)
        };

        int coins = Coin.Coins;

        for (int i = 0; i < 3; i++)
        {
            if (coins >= scores[i])
            {
                for (int j = 2; j > i; j--)
                    scores[j] = scores[j - 1];

                scores[i] = coins;
                break;
            }
        }

        for (int i = 0; i < 3; i++)
            PlayerPrefs.SetInt("TopCoin" + i, scores[i]);

        PlayerPrefs.Save();
        ShowScores();
    }

    private void ShowScores()
    {
        scoreText.text =
            "Top scores\n" +
            "1. " + PlayerPrefs.GetInt("TopCoin0", 0) + "\n" +
            "2. " + PlayerPrefs.GetInt("TopCoin1", 0) + "\n" +
            "3. " + PlayerPrefs.GetInt("TopCoin2", 0);
    }
}
