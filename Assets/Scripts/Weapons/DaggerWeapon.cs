using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 回旋镖武器 */
public class DaggerWeapon : Weapons
{
    public float moveSpeed;
    public float desiredAngularVelocity;
    public float range;
    private Vector3 moveDirection;
    public Transform holder;/* 持有者 */
    public float spawnBetweenTime;
    private float spawnCounter;
    public EnemyDamage enemyDamage;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        SetState();
    }

    // Update is called once per frame
    void Update()
    {
        /* 
        每隔一段时间生成一个匕首
        生成的匕首以一定速度移动和旋转
        如果在玩家一定范围没有敌人，那么就向右侧方向攻击，
        否则向敌人攻击
        如果路径中发生碰撞 / 就销毁，并对敌人造成伤害
        */
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            /* 生成 */
            spawnCounter = spawnBetweenTime;
            GameObject newSpawnObj = Instantiate(enemyDamage, enemyDamage.transform.position, Quaternion.identity, holder).gameObject;
            newSpawnObj.gameObject.SetActive(true);
            /* 移动和旋转 */
            GameObject gam = LocateEnemy();
            if (gam != null)
            {
                moveDirection = MoveDirection(gam.transform);
            }
            else
            {
                moveDirection = new Vector3(1, 0, 0);
            }
            newSpawnObj.GetComponent<Rigidbody2D>().velocity = (moveDirection).normalized * moveSpeed;
            newSpawnObj.GetComponent<Rigidbody2D>().angularVelocity = desiredAngularVelocity * 360f;

        }
        if (isUpdateLevWeapon)
        {/* 武器升级 */
            isUpdateLevWeapon = false;
            SetState();
        }
    }
    private GameObject LocateEnemy()/* 查找玩家范围内最近的敌人 */
    {
        var results = new Collider2D[5];
        Physics2D.OverlapCircleNonAlloc(playerController.gameObject.transform.position, range, results);
        float minDis = float.MaxValue;
        GameObject gameObject = null;
        foreach (var col in results)
        {
            if (col != null && col.CompareTag("Enemy"))
            {
                float dis = Vector3.Distance(col.transform.position, playerController.gameObject.transform.position);
                if (dis < minDis)
                {
                    minDis = dis;
                    gameObject = col.gameObject;
                }
            }
        }
        return gameObject;
    }
    private Vector3 MoveDirection(Transform target)
    {
        Vector3 res = Vector3.zero;
        if (target != null)
        {
            res = (target.transform.position - transform.position).normalized;
        }
        return res;
    }
    public void SetState()
    {
        moveSpeed = stats[weaponLevel].speed;
        enemyDamage.damage = stats[weaponLevel].damage;
        range = stats[weaponLevel].range;
        spawnBetweenTime = stats[weaponLevel].timeBetweenAttack;
        spawnCounter = 0;
    }
}
