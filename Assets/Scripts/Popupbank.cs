using TMPro;
using UnityEngine;

public class Popupbank : MonoBehaviour
{
    public GameObject Buttons;

    public GameObject depositPanel;
    public GameObject withdrawalPanel;

    public GameObject NotEnoughPanel;

    public TMP_InputField depositInputField;
    public TMP_InputField withdrawalInputField;


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
    public void Deposit(int value)
    {
        // 입금
        if (value > 0)
        {
            // 갖고 있는 현금이 입금 금액보다 많을 때 실행
            if (GameManager.Instance.userData.cashValue >= value)
            {
                // 입금 실행
                GameManager.Instance.userData.cashValue -= value;
                GameManager.Instance.userData.balance += value;
            }
            // 부족하면 실행
            else
            {
                // 잔액 부족 메시지 출력
                NotEnoughMoney();
            }
        }
    }
    
    // 출금 계산 메서드
    public void Withdrawal(int value)
    {
        // 통장의 남은 금액이 출금 금액보다 많을 때 실행
        if (GameManager.Instance.userData.balance >= value)
        {
            // 출금 실행
            GameManager.Instance.userData.balance -= value;
            GameManager.Instance.userData.cashValue += value;

        }
        // 부족하면
        else
        {
            // 잔액 부족 메시지 출력
            NotEnoughMoney();
        }
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


    // 뒤로가기 버튼 (입금, 출금 버튼 보기)
    public void BackButtons()
    {
        Buttons.SetActive(true);
        depositPanel.SetActive(false);
        withdrawalPanel.SetActive(false);
    }

    // 잔액 부족 창 표시 메서드
    public void NotEnoughMoney()
    {
        NotEnoughPanel.SetActive(true);
    }

    // 잔액 부족 창 닫기 메서드
    public void NotEnoughMoneyClose()
    {
        NotEnoughPanel.SetActive(false);
    }

}
