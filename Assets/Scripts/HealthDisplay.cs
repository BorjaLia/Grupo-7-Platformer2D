using System;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    
    [SerializeField] private Sprite filledHealthSprite;
    [SerializeField] private Sprite emptyHealthSprite;
    
    private PlayerHealth _playerHealth;
    public Image[] Hearts;

    private void Awake()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        if (_playerHealth == null) Debug.LogError("No SSCRIPT for _PlayerHealth was FOUND");
    }

    private void Update()
    {
        currentHealth = _playerHealth.currentHealth;
        maxHealth = _playerHealth.maxHealth;
        
        for (int i = 0; i < Hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                Hearts[i].sprite = filledHealthSprite;
            }
            else
            {
                {
                    Hearts[i].sprite = emptyHealthSprite;
                }
            }
            
            if (i < maxHealth)
            {
                Hearts[i].enabled = true;
            }
            else
            {
                Hearts[i].enabled = false;
            }
        }
    }
}
