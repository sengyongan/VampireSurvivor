using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/* 敌人控制器 */
public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rid;
    private Transform target;
    public float damage;
    public float hitWaitTime = 1f;/* 伤害等待时间 */
    private float hitCounter;/* 伤害计时器 */
    public float health = 5f;
    public float knockBackTime = .5f;/* 敌人被击退的的持续时间（反向移动），时间结束重新移动向敌人 */
    private float knockBackCounter;
    public float EXPToGive = 10f;/* 掉落经验 */
    public int coinValue = 1;/* 掉落金币/ 属性点 */
    public float coinDropRate = .5f;/* 掉落概率 */
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerHealthController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealthController.instance.currentHealth > 0)
        {
            /* 击退 */
            if (knockBackCounter > 0)
            {
                knockBackCounter -= Time.deltaTime;
                if (moveSpeed > 0)/* 加上这个，才可以仅执行一次将速度变为反向，否则在knockBackCounter > 0时段下，一直来回-取相反值*/
                {
                    moveSpeed = -moveSpeed * 2f;
                }
                if (knockBackCounter <= 0)/* 这样写提高性能，不用每帧都判断 */
                {
                    moveSpeed = Math.Abs(moveSpeed * .5f);
                }
            }

            /* 追踪玩家 */
            rid.velocity = (target.position - transform.position).normalized * moveSpeed;
            hitCounter -= Time.deltaTime;
        }
        else
        {/* 玩家死亡，停止移动 */
            rid.velocity = Vector2.zero;
        }
    }
    /* 对玩家造成伤害 */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && hitCounter <= 0)
        {
            PlayerHealthController.instance.TackDamage(damage);
            hitCounter = hitWaitTime;
        }
    }
    public void TackDamage(float amount)
    {
        health -= amount;
        if (health <= 0)/* 敌人死亡，掉落exp */
        {
            ExperienceLevelController.instance.instanEXPPickup(transform.position, EXPToGive);
            if (UnityEngine.Random.value <= coinDropRate)
            {
                CoinController.instance.DropCoin(transform.position, coinValue);
            }
            UIPlayerDescriptionController.instance.killCount++;
            Destroy(gameObject);
        }
        DamageNumberController.instance.ActiveDamageNumberAtLocation(amount, transform.position);/* 创建伤害数字 */
    }
    public void TackDamage(float amount, bool shouldKnockBack)
    {
        UIPlayerDescriptionController.instance.injuryAccumulation += amount;
        TackDamage(amount);
        if (shouldKnockBack)
        {
            knockBackCounter = knockBackTime;/* 重置计时器 */
        }
    }
}
