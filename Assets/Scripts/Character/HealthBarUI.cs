using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;

    private void OnEnable()
    {
        CharacterState.OnHealthChanged += UpdateHealthBar;
    }
    private void OnDisable()
    {
       
        CharacterState.OnHealthChanged -= UpdateHealthBar;
    }
    private void Start()
    {
      
        UpdateHealthBar(CharacterState.Instance.GetCurrentHP());
    }
    private void UpdateHealthBar(float health)
    {
        healthSlider.value = health;
        Debug.Log("health UI change:" +health);
    }
}

