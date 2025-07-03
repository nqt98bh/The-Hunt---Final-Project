using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] int damageCost;
    [SerializeField] int defendCost;
    [SerializeField] int speedCost;
    [SerializeField] int HPCost;

    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI defendText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI HPText;

    [SerializeField] private TextMeshProUGUI damageAttribute;
    [SerializeField] private TextMeshProUGUI defendAttribute;
    [SerializeField] private TextMeshProUGUI speedAttribute;
    [SerializeField] private TextMeshProUGUI HPAttribute;

    private void Start()
    {
        damageText.text = damageCost.ToString();
        defendText.text = defendCost.ToString();
        speedText.text = speedCost.ToString();
        HPText.text = HPCost.ToString();
    }


    public void BuyDamageUp()
    {
        GameManager.Instance.CurencyManager.SpendCoin(damageCost);
        GameManager.Instance.CharacterController.DefendUp(damageCost);
    }
    public void BuyDefendUp()
    {
        GameManager.Instance.CurencyManager.SpendCoin(defendCost);
        GameManager.Instance.CharacterController.DamageUp(damageCost);
    }
    public void BuySpeedUp()
    {
        GameManager.Instance.CurencyManager.SpendCoin(speedCost);
        GameManager.Instance.CharacterController.DamageUp(damageCost);
    }
    public void BuyHPUp()
    {
        GameManager.Instance.CurencyManager.SpendCoin(HPCost);
        GameManager.Instance.CharacterController.DamageUp(damageCost);
    }
}
