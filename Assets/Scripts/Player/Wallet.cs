using System;

public class Wallet 
{
    private uint _coins = 0;

    public uint Coins { get; private set; }
    public event Action<uint> Change;

    public void AddCoin()
    {
        _coins++;
        Change?.Invoke(_coins);
    }

    public void TakeCoins(uint amount)
    {
        if (_coins >= amount)
        {
            _coins -= amount;
            Change?.Invoke(_coins);
        }
    }
}
