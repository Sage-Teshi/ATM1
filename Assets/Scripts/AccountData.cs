using UnityEngine;

[System.Serializable]
public class AccountData
{
    public string userId;
    public string password;
    public string name;

    public int cashValue;
    public int balance;

    public AccountData(string id, string pw, string name)
    {
        userId = id;
        password = pw;
        this.name = name;

        // 신규 계정의 자산 설정
        cashValue = 50000;
        balance = 100000;
    }
}
