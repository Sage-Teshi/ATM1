using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public UserData userData;

    public TextMeshProUGUI cashText;
    public TextMeshProUGUI balanceText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        userData = new UserData("Kim", 100000, 50000);
    }

    void Start()
    {
        
    }

    void Update()
    {
        Refresh();
    }

    public void Refresh() 
    { 
        if (cashText != null)
        {
            cashText.text = string.Format("{0:N0}", userData.cashValue);
        }

        if (balanceText != null)
        {
            balanceText.text = string.Format("{0:N0}", userData.balance);
        }

    }
}
