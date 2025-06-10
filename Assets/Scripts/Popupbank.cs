using TMPro;
using UnityEngine;

public class Popupbank : MonoBehaviour
{
    public GameObject loginPanel;
    public GameObject popupBankPanel;

    public GameObject Buttons;

    public GameObject depositPanel;
    public GameObject withdrawalPanel;
    public GameObject remittancePanel;

    public GameObject NotEnoughPanel;

    public TMP_InputField depositInputField;
    public TMP_InputField withdrawalInputField;

    public TMP_InputField targetInputField;
    public TMP_InputField amountInputField;

    public TextMeshProUGUI userNameText;


    // 입금 메뉴 보기
    public void ShowDeposit()
    {
        Buttons.SetActive(false);
        depositPanel.SetActive(true);
    }


    // 출금 메뉴 보기
    public void ShowWithdrawal()
    {
        Buttons.SetActive(false);
        withdrawalPanel.SetActive(true);
    }


    // 송금 메뉴 보기
    public void ShowRemittancePanel()
    {
        Buttons.SetActive(false);
        remittancePanel.SetActive(true);
    }

    // 입금 직접 입력 값 계산 메서드
    public void DepositFormInput()
    {
        int value = GetInputValue(depositInputField);
        
        if (value > 0)
        {
            Deposit(value);
        }
    }


    // 출금 직접 입력 값 계산 메서드 
    public void withdrawalFormInput()
    {
        int value = GetInputValue(withdrawalInputField);
        
        if (value > 0)
        {
            Withdrawal(value);
        }
    }
    

    // 입금 계산 메서드
    public void Deposit(int amount)
    {
        // 입금
        if (amount <= 0)
        {
            ErrorPanelManager.Instance.ShowError("1원 이상 입력해 주세요.");
            return;
        }

        // 갖고 있는 현금이 입금 금액보다 많을 때 실행
        var acc = GameManager.Instance.currentAccount;

        if (acc.cashValue >= amount)
        {
            acc.cashValue -= amount;
            acc.balance += amount;

            AccountManager.Instance.SaveAccounts(); // 변경 사항 저장
        }

        // 부족하면 실행
        else
        {
            // 잔액 부족 메시지 출력
            NotEnoughMoney();
        }
        

        depositInputField.text = "";
        GameManager.Instance.Refresh();
    }
    

    // 출금 계산 메서드
    public void Withdrawal(int amount)
    {

        if (amount <= 0)
        {
            ErrorPanelManager.Instance.ShowError("1원 이상 입력해 주세요.");
            return;
        }

        // 통장의 남은 금액이 출금 금액보다 많을 때 실행
        AccountData acc = GameManager.Instance.currentAccount;

        if (acc.balance >= amount)
        {
            acc.balance -= amount;
            acc.cashValue += amount;

            AccountManager.Instance.SaveAccounts(); // 변경된 정보 저장
        }
        // 부족하면
        else
        {
            // 잔액 부족 메시지 출력
            NotEnoughMoney();
        
        }

        withdrawalInputField.text = "";
        GameManager.Instance.Refresh();
    }


    // 입출금 값 직접 입력 받아오기 메서드
    public int GetInputValue(TMP_InputField inputField)
    {
        string input = inputField.text;

        if (int.TryParse(input, out int result))
        {
            return result;
        }
        else
        {
            Debug.LogWarning("숫자가 아닙니다.");
            return 0;
        }
    }


    // 송금 메서드
    public void Remit()
    {
        string targetName = targetInputField.text.Trim();
        string amountStr = amountInputField.text.Trim();

        // 입력값 확인
        if (string.IsNullOrEmpty(targetName) || string.IsNullOrEmpty(amountStr))
        {
            ErrorPanelManager.Instance.ShowError("입력 정보를 확인해주세요.");
            return;
        }

        // 숫자 변환 확인
        if (!int.TryParse(amountStr, out int amount) || amount <= 0)
        {
            ErrorPanelManager.Instance.ShowError("올바른 금액을 입력해주세요.");
            return;
        }

        AccountData sender = GameManager.Instance.currentAccount;
        AccountData receiver = AccountManager.Instance.GetAccountByName(targetName);

        // 송금 대상 존재 여부
        if (receiver == null)
        {
            ErrorPanelManager.Instance.ShowError("대상이 없습니다.");
            return;
        }

        // 자기 자신에게 송금 방지
        if (receiver.userId == sender.userId)
        {
            ErrorPanelManager.Instance.ShowError("자기 자신에게 송금할 수 없습니다.");
            return;
        }

        // 잔액 확인
        if (sender.balance < amount)
        {
            ErrorPanelManager.Instance.ShowError("잔액이 부족합니다.");
        }

        sender.balance -= amount;
        receiver.balance += amount;

        AccountManager.Instance.SaveAccounts();
        GameManager.Instance.Refresh();

        // 입력창 초기화
        targetInputField.text = "";
        amountInputField.text = "";

    }


    // 뒤로가기 버튼 (입금, 출금 버튼 보기)
    public void BackButtons()
    {
        Buttons.SetActive(true);
        depositPanel.SetActive(false);
        withdrawalPanel.SetActive(false);
        remittancePanel.SetActive(false);
    }

    public void Logout()
    {
        // 현재 로그인 정보 초기화
        GameManager.Instance.currentAccount = null;

        // 로그인 화면 다시 보여주기 
        loginPanel.SetActive(true);

        // 현재 화면(PopupBank) 숨기기
        popupBankPanel.SetActive(false);


    }



    // 잔액 부족 창 표시 메서드
    public void NotEnoughMoney()
    {
        ErrorPanelManager.Instance.ShowError("잔액이 부족합니다.");
    }


    // 사용자 표시
    public void OnEnable()
    {
        if (GameManager.Instance.currentAccount != null && userNameText != null)
        {
            userNameText.text = GameManager.Instance.currentAccount.name +"님";
        }
    }
}
