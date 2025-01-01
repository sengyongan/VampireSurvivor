using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
/* Weapons的派生类：生成，武器行动方式，等级，武器信息 */
/* 伤害类负责：碰撞，伤害，销毁 */
public class EnemyDamage : MonoBehaviour
{
    public float damage;
    public float liftTime;
    /* 开始和结束大小变换，0--1--0 */
    private Vector3 targetSize;
    public float growSpeed;
    public bool shouldKnockBack;
    /* 是否是持续伤害的技能 */
    public bool isDamegeContinue;
    public float damageForTime;
    public float damegeContinueCounter;
    public List<EnemyController> enemysForRange = new List<EnemyController>();
    /* 是否是立即销毁的物体 */
    public bool isDestroyOnImpact;
    // Start is called before the first frame update
    void Start()
    {
        targetSize = transform.localScale * 5;
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        /* 小-》大-》小的scale控制 */
        /* 出生 */
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);
        /* 销毁 */
        liftTime -= Time.deltaTime;
        if (liftTime <= 0)
        {
            targetSize = Vector3.zero;
            if (transform.localScale.x == 0)
            {
                Destroy(gameObject);
            }
        }
        /* 如果是持续伤害技能，每间隔一段时间伤害一次，范围内的敌人 */
        if (isDamegeContinue)
        {
            transform.Rotate(0, 0, 180.0f * Time.deltaTime);/* 出生后不断旋转 */

            damegeContinueCounter -= Time.deltaTime;
            if (damegeContinueCounter <= 0)
            {
                damegeContinueCounter = damageForTime;
                for (int i = 0; i < enemysForRange.Count; i++)
                {
                    if (enemysForRange[i])
                    {
                        enemysForRange[i].TackDamage(damage, shouldKnockBack);
                    }
                    else
                    {
                        enemysForRange.RemoveAt(i);
                        i--; // 防止越界,这次检查失败，回到上一次
                    }
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (!isDamegeContinue)
            {
                collision.GetComponent<EnemyController>().TackDamage(damage, shouldKnockBack);
                if (isDestroyOnImpact)
                {/* 立即销毁的物体，碰撞就立即销毁 */
                    Destroy(gameObject);
                }
            }
            else/* 范围内的敌人 */
            {
                enemysForRange.Add(collision.GetComponent<EnemyController>());
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (isDamegeContinue)
            {
                enemysForRange.Remove(collision.GetComponent<EnemyController>());
            }
        }
    }
}
