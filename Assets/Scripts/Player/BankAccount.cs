using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;


public class BankAccount : MonoBehaviour
{
    BigDecimal balance;
    string accountName { get; }
    public BankAccount(string name)
    {
        this.accountName = name;
        balance = new BigDecimal();
    }
    void Start()
    {
        balance = new BigDecimal();
    }

    public void addMoney(double amount)
    {
        balance.add(amount);
    }

    public string getBalanceString()
    {
        return balance.ToString();
    }

    public BigDecimal getBalance()
    {
        return balance;
    }

    public string getName()
    {
        return accountName;
    }
}
