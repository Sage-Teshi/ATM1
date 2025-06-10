using TMPro;
using UnityEngine;

public class LoginUI : MonoBehaviour
{
    public TMP_InputField inputId;
    public TMP_InputField inputPw;

    public GameObject loginPanel;
    public GameObject bankPanel;
    public GameObject createPanel;

    public void OnLoginClicked()
    {
        string id = inputId.text.Trim();
        string pw = inputPw.text;

        if (AccountManager.Instance.IsValidLogin(id, pw))
        {
            GameManager.Instance.currentAccount = AccountManager.Instance.GetAccountById(id);

            Debug.Log("로그인 성공!");
            loginPanel.SetActive(false);
            bankPanel.SetActive(true);
        }
        else
        {
            ErrorPanelManager.Instance.ShowError("아이디 또는 비밀번호가 올바르지 않습니다.");
        }
    }

    public void OnClickSignUp()
    {
        loginPanel.SetActive(false);
        createPanel.SetActive(true);
    }

    public void ClearInputFields()
    {
        inputId.text = "";
        inputPw.text = "";
    }
}
