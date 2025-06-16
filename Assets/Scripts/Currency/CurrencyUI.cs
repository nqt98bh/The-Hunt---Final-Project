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
    private void Start()
    {
        UpdateCurrencyUI(GameManager.Instance.CurencyManager.GetCoinCurrentValue());

    }
    private void UpdateCurrencyUI(int amount)
    {
        if (coinText == null) return;
        coinText.text = amount.ToString();
    }
}
