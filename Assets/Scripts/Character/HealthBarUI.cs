using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;

    private void OnEnable()
    {
        CharacterController.OnHealthChanged += UpdateHealthBar;
    }
    private void OnDisable()
    {
       
        CharacterController.OnHealthChanged -= UpdateHealthBar;
    }
   
    private void UpdateHealthBar()
    {
        healthSlider.value =(float) GameManager.Instance.CharacterController.CurrentHP/ (float)GameManager.Instance.CharacterController.maxHP;
        
    }
}

