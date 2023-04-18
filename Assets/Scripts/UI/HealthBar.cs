using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] RectTransform healthFill;
    [SerializeField] Image fillImage;
    [SerializeField] float fullWidth;
    
    [SerializeField] Color safeColor;
    [SerializeField] Color criticalColor;

    // Normalized Range
    [SerializeField] float safeRange;
    [SerializeField] float criticalRange;

    PlayerDamage playerDamage;

    private void OnEnable()
    {
        StartCoroutine(GetPlayer());
    }
    private void OnDisable()
    {
        if (playerDamage) playerDamage.OnHealthUpdate -= UpdateHealthBar;
    }
    IEnumerator GetPlayer()
    {
        while(GameManager.Instance.player == null)
        {
           yield return null;
        }

        GameObject player = GameManager.Instance.player;
        playerDamage = player.GetComponent<PlayerDamage>();
        float currentHealth = playerDamage.GetCurrentHealthNormalized();
        UpdateHealthBar(currentHealth);
        playerDamage.OnHealthUpdate += UpdateHealthBar;
    }



    private void UpdateHealthBar(float value)
    {
        Vector2 offset;
        Color color;

        offset = healthFill.offsetMax;
        offset.x = Mathf.Lerp(-fullWidth, 0, value);
        healthFill.offsetMax = offset;

        float colorRange = FindColorRange(value);
        color = Color.Lerp(criticalColor, safeColor, colorRange);
        fillImage.color = color;
    }

    private float FindColorRange(float value)
    {
        float range;
        range = Mathf.Clamp(value, criticalRange, safeRange);
        range = Mathf.InverseLerp(criticalRange, safeRange, range);
        return range;
    }
}
