using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErrorPanelManager : MonoBehaviour
{
    public static ErrorPanelManager Instance;

    public GameObject errorPanel;       // 전채 패널 오브젝트
    public TextMeshProUGUI messageText; // 안에 들어갈 텍스트 오브젝트

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }



    // 오류 메시지를 띄우는 함수
    public void ShowError(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
        }

        errorPanel.SetActive(true);
    }

    public void OnClickClose()
    {
        CloseError();
    }

    // 오류 패널을 닫는 함수 
    public void CloseError()
    {
        errorPanel.SetActive(false);
    }

}
