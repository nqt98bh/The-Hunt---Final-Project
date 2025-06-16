using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour, IDataPersistence
{
    private int currentCoin = 0;
    public static Action<int> OnCoinChanged;
   
    private void Start()
    {
        
    }
    public void AddCoin(int amount)
    {
        currentCoin  += amount;
        Debug.Log("Coin value: " + currentCoin);
        OnCoinChanged?.Invoke(currentCoin);
        
    }

    public void SpendCoin(int amount)
    {
        if (currentCoin < amount)
        {
            Debug.Log("Coin is not enough");
            return;
        } 
        currentCoin -= amount;
        OnCoinChanged?.Invoke(currentCoin);


    }
    public int GetCoinCurrentValue()
    {
        return currentCoin;
    }

    public void LoadData(GameData data)
    {
        this.currentCoin = data.CoinValue;
    }

    public void SaveData(ref GameData data)
    {
        data.CoinValue = this.currentCoin;
    }
}
