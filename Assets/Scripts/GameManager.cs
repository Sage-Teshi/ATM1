using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public TextMeshProUGUI cashText;
    public TextMeshProUGUI balanceText;

    public GameObject loginPanel;
    public GameObject createPanel;

    public AccountData currentAccount; // 현재 로그인한 사용자 

    private string saveFilePath;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        saveFilePath = Application.persistentDataPath + "/userData.json";
    }

    private void Start()
    {
        loginPanel.SetActive(true);
        createPanel.SetActive(true);

        StartCoroutine(InitUI());
    }

    void Update()
    {
        Refresh();
    }

    IEnumerator InitUI()
    {
        yield return null; // 한 프레임 기다리기 

        loginPanel.SetActive(true);
        createPanel.SetActive(false);
    }

    // UI 업데이트 메서드
    public void Refresh() 
    {
        if (currentAccount == null) return;

        if (cashText != null)
            cashText.text = string.Format("{0:N0}", currentAccount.cashValue);

        if (balanceText != null)
            balanceText.text = string.Format("{0:N0}", currentAccount.balance);
    }

  

}
