using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;
    private void OnEnable()
    {
        CurrencyManager.OnCoinChanged += UpdateCurrencyUI;

    }

    private void OnDisable()
    {
        CurrencyManager.OnCoinChanged -= UpdateCurrencyUI;
    }
 
    private void UpdateCurrencyUI()
    {
        if (coinText == null) return;
        coinText.text = GameManager.Instance.CurencyManager.CurrentCoin.ToString();
    }
}
