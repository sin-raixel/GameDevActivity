using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int coins = 0;
    public TMP_Text coinText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        updateCoin();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        updateCoin();
    }

    public void updateCoin()
    {
        coinText.text = "Coins: " + coins.ToString();
    }
}