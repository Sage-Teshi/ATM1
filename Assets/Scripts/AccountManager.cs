using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AccountManager : MonoBehaviour
{
    public static AccountManager Instance;
    private string filePath;
    private List<AccountData> accountList = new List<AccountData>();

    [System.Serializable]
    class AccountListWrapper
    {
        public List<AccountData> accounts;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            filePath = Application.persistentDataPath + "/accounts.json";
            LoadAccounts();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadAccounts()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            var wrapper = JsonUtility.FromJson<AccountListWrapper>(json);
            accountList = wrapper.accounts ?? new List<AccountData>();
        }
    }

    public void SaveAccounts()
    {
        var wrapper = new AccountListWrapper { accounts = accountList };
        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(filePath, json);
    }

    public bool IsIdExist(string id)
    {
        return accountList.Exists(acc => acc.userId == id);
    }

    public void AddAccount(string id, string pw, string name)
    {
        accountList.Add(new AccountData(id, pw, name));
        SaveAccounts();
    }

    public bool IsValidLogin(string id, string pw)
    {
        return accountList.Exists(acc => acc.userId == id && acc.password == pw);
    }

    public AccountData GetAccountById(string id)
    {
        return accountList.Find(acc => acc.userId == id);
    }

    public AccountData GetAccountByName(string name)
    {
        return accountList.Find(acc => acc.name == name);
    }
}
