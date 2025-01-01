using UnityEngine;
using UnityEngine.UI;
/* 玩家血条 */
public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Health health;
    private void Awake()
    {
        healthBar.maxValue = health.Value;
        healthBar.value = health.Value;
    }
    public void UpdateUI()
    {
        healthBar.value = health.Value;
    }
}