using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour, IDataPersistence
{
    private int currentCoin = 0;
    public int CurrentCoin => currentCoin;
    public static Action OnCoinChanged;
   

    public void AddCoin(int amount)
    {
        currentCoin  += amount;
        Debug.Log("Coin value: " + currentCoin);
        OnCoinChanged?.Invoke();
        
    }

    public void SpendCoin(int amount)
    {
        if (currentCoin < amount)
        {
            Debug.Log("Coin is not enough");
            return;
        } 
        currentCoin -= amount;
        OnCoinChanged?.Invoke();


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
