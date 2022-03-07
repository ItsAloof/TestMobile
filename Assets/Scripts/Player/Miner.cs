using System;
using System.Collections;
using System.Collections.Generic;

public class Miner
{
    double dogeCoinPerSecond;
    public Miner(double dogeCoinPerSecond)
    {
        this.dogeCoinPerSecond = dogeCoinPerSecond;
    }

    public double Mine(bool dropCoin)
    {
        if(dropCoin)
            GameManager.dropDogeCoin(GameManager.Instance.dogeCoin);
        return Math.Round((new Random().NextDouble() * dogeCoinPerSecond), 3, MidpointRounding.AwayFromZero);
    }
    // Time in seconds
    public double calculateDogeCoin(double time)
    {
        double totalDogeCoins = 0;
        for(double i = 0; i < time; i += 60)
        {
            totalDogeCoins += Math.Round((new Random().NextDouble() * dogeCoinPerSecond * 60), 3, MidpointRounding.AwayFromZero);
            GameManager.dropDogeCoin(GameManager.Instance.dogeCoin);
        }
        return totalDogeCoins;
    }

    public void setMultiplier(double multiplier)
    {
        this.dogeCoinPerSecond = multiplier;
    }

}
