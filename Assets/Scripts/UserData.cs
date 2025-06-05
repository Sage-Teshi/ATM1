using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public string userName = "kim";
    public int cashValue = 100000;
    public int balance = 50000;

    public UserData(string name, int cash, int bal)
    {
        userName = name;
        cashValue = cash;
        balance = bal;
    }
}
