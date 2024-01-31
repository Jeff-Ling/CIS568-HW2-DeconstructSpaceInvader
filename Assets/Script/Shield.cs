using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Shield : MonoBehaviour
{
    public float shieldLives = 10f;
    public TextMeshProUGUI shieldLivesUI;

    void Start()
    {
        shieldLivesUI.text = shieldLives.ToString();
    }
    public void UpdateShieldLivesText()
    {
        shieldLives--;
        shieldLivesUI.text = shieldLives.ToString();
        if (shieldLives == 0) Die();
    }

    void Die()
    {
        shieldLivesUI.enabled = false;
        Destroy(gameObject);
    }
}
