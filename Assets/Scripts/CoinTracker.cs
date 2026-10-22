

using TMPro;
using UnityEngine;

public class CoinTracker : MonoBehaviour
{
    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _text.text = "Coins: " + Coin.Coins;
    }
}
