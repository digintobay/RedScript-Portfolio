using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Slider healthSlider;
    public Slider backSlider;
  
    public void SetHealth(float currentHealth, float maxHealth)
    {
        healthSlider.value = currentHealth / maxHealth;
        Debug.Log("슬라이더 반영");
        StartCoroutine(BackHealth(currentHealth, maxHealth));

    }

    IEnumerator BackHealth(float currentCopy, float maxCopy)
    {
        yield return new WaitForSeconds(1f);
        backSlider.value = currentCopy / maxCopy;
    }
}
