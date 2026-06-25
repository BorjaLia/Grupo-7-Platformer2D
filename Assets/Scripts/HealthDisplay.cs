using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    
    [SerializeField] private Sprite filledHealthSprite;
    [SerializeField] private Sprite emptyHealthSprite;
    
    public PlayerHealth playerHealth;
    public Image[] Hearts;
    
    private void Update()
    {
        currentHealth = playerHealth.currentHealth;
        maxHealth = playerHealth.maxHealth;
        
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
