using System;
using Timers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Attack : MonoBehaviour
{
    [SerializeField] private string tags;
    [SerializeField] private UnityEvent attack;
    private bool IsCanAttack = true;
    private void OnTriggerEnter2D(Collider2D col)
    {
        DealDamage(col);
    }

    private void CanAttack()
    {
        IsCanAttack = true;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        DealDamage(col);
    }
    private void DealDamage(Collider2D col)/* 制造伤害 */
    {
        if (!IsCanAttack) return;

        if (col.CompareTag(tags))
        {
            var damageable = col.GetComponent<Damageable>();
            damageable.TackDamage(10);
            TimersManager.SetTimer(this, 1, CanAttack);/* 间隔一秒执行一次 */
            IsCanAttack = false;
            attack.Invoke();
        }
    }
}