using TMPro;
using UnityEngine;

public class CreateAccountUI : MonoBehaviour
{
    public TMP_InputField inputId;
    public TMP_InputField inputName;
    public TMP_InputField inputPw;
    public TMP_InputField inputPwCheck;


    public GameObject loginPanel;
    public GameObject createPanel;

    public void OnCreateAccountClicked()
    {
        string id = inputId.text.Trim();
        string name = inputName.text.Trim();
        string pw = inputPw.text;
        string pwCheck = inputPwCheck.text;

        if (id == "" || pw == "")
        {
            ErrorPanelManager.Instance.ShowError("아이디와 비밀번호를 입력해주세요.");
            return;
        }

        if (pw != pwCheck)
        {
            ErrorPanelManager.Instance.ShowError("비밀번호가 일치하지 않습니다.");
            return;
        }

        if (AccountManager.Instance.IsIdExist(id))
        {
            ErrorPanelManager.Instance.ShowError("이미 존재하는 아이디입니다.");
            return;
        }

        AccountManager.Instance.AddAccount(id, pw, name);
        ErrorPanelManager.Instance.ShowError("회원가입이 완료되었습니다.");

        // 로그인 화면으로 전환
        loginPanel.SetActive(true);
        createPanel.SetActive(false);
    }

    public void OnBackToLogin()
    {
        loginPanel.SetActive(true);
        createPanel.SetActive(false);
    }
}
