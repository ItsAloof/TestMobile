using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System;


public class BankAccount
{
    BigDecimal balance;
    string accountName { get; set; }
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
        balance.add(Math.Round(amount, 5, MidpointRounding.AwayFromZero));
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
