using System.Collections;
using System.Collections.Generic;

public class Player
{
    BankAccount account { get; set; }

    List<Miner> miners = new List<Miner>();
    public Player(BankAccount bankAccount)
    {
        this.account = bankAccount;
    }

    public void deposit(double amount)
    {
        account.addMoney(amount);
        GameManager.updateBalance(account, GameManager.Instance.balance);
    }

    public void withdraw(double amount)
    {
        account.addMoney(-amount);
        GameManager.updateBalance(account, GameManager.Instance.balance);
    }

    public BankAccount getBankAccount()
    {
        return account;
    }

    public List<Miner> getMiners()
    {
        return miners;
    }

    public void addMiner(Miner miner)
    {
        miners.Add(miner);
    }

    public double runMiners()
    {
        if (miners.Count == 0)
            return 0;
        double coins = 0;
        foreach(Miner miner in miners)
        {
            coins += miner.Mine(true);
        }
        deposit(coins * GameManager.DogeCoinValue);
        return coins;
    }
}
