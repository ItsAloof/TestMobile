using System.Numerics;
using System.Collections.Generic;
using System;

public class BigDecimal
{
    BigInteger number { get; set; }
    // The decimal remainer from a big integer
    double change {get; set; }
    public BigDecimal(BigInteger bigint, double number)
    {
        double remainder = number % 1;
        change = Math.Round(remainder, 5, MidpointRounding.AwayFromZero);
    }

    public BigDecimal()
    {

    }

    public void add(double amount)
    {
        double remainder = amount % 1.0f;
        change += Math.Round(remainder, 5, MidpointRounding.AwayFromZero);
        number += (int) Math.Truncate(amount);
        if(change >= 1.0)
        {
            change--;
            number++;
        }
    }

    public BigInteger getIntBalance()
    {
        return number;
    }

    public double getChangeBalance()
    {
        return change;
    }

    public override string ToString()
    {
        return change == 0 ? $"{number.ToString("C")}" : $"{number.ToString("C").Substring(0, number.ToString("C").Length-3)}.{Math.Round(change, 5, MidpointRounding.AwayFromZero).ToString().Substring(2)}";
    }



}
