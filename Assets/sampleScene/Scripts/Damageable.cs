using DG.Tweening;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Color defaultColor;
    public void TackDamage(int damage)/* 应用伤害 */
    {
        health.DecreaseHealth(damage);
        spriteRenderer.DOColor(Color.red, 0.2f).SetLoops(2, LoopType.Yoyo).ChangeStartValue(defaultColor);/* 白到红，红到白，渐变,起始白色 */
    }
    private void Awake()
    {
        defaultColor = spriteRenderer.color;
    }
}