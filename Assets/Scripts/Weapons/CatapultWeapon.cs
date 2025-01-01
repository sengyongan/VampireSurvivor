using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 在屏幕间弹射武器 */
public class CatapultWeapon : Weapons
{
    public float moveSpeed;
    private Vector3 moveDirection;
    public float spawnBetweenTime;
    private float spawnCounter;
    public EnemyDamage enemyDamage;
    public Rigidbody2D rigidbody2D;
    public Transform minSpawnerPoint, maxSpawnerPoint;
    public EnemySpawner enemySpawner;
    private bool isSwitch;
    // Start is called before the first frame update
    void Start()
    {
        SetState();

        spawnCounter = spawnBetweenTime;
        moveDirection = RandomDirection();
        rigidbody2D.velocity = moveDirection * moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        /* 
            在玩家位置生成，每间隔一段时间，重新生成在玩家位置
            向0--360度的方向发射武器-》（在正方形区域边界框任意位置，的向量归一化）
            遇到屏幕边缘弹射，方向为反射方向
            对沿途敌人造成伤害
         */
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            isSwitch = !isSwitch;
            enemyDamage.gameObject.SetActive(true);
            moveDirection = RandomDirection();
            rigidbody2D.velocity = moveDirection * moveSpeed;
            enemyDamage.transform.position = PlayerController.instance.transform.position;
            spawnCounter = spawnBetweenTime;
        }
        if (!isSwitch)
        {
            enemyDamage.gameObject.SetActive(true);
            if (!IsAtScreenInside())/* 超出屏幕范围，改变方向 */
            {
                Vector3 reflection = Vector3.Reflect(moveDirection, GetScreenEdgeNormal().normalized);
                moveDirection = reflection; // 更新移动方向  
                rigidbody2D.velocity = moveDirection * moveSpeed;
            }
        }
        else
        {
            enemyDamage.gameObject.SetActive(false);
        }
        if (isUpdateLevWeapon)
        {/* 武器升级 */
            isUpdateLevWeapon = false;
            SetState();
        }
    }
    private Vector3 RandomDirection()/* 随机初始方向 */
    {
        return enemySpawner.RandomSpawnPosition().normalized;
    }
    private bool IsAtScreenInside()
    {
        Vector3 position = enemyDamage.gameObject.transform.position;
        return position.x <= maxSpawnerPoint.transform.position.x &&
               position.x >= minSpawnerPoint.transform.position.x &&
               position.y <= maxSpawnerPoint.transform.position.y &&
               position.y >= minSpawnerPoint.transform.position.y;
    }

    private Vector3 GetScreenEdgeNormal()
    {
        Vector3 position = enemyDamage.gameObject.transform.position;
        Vector3 cameraBottomLeft = minSpawnerPoint.transform.position;
        Vector3 cameraTopRight = maxSpawnerPoint.transform.position;

        if (position.x <= cameraBottomLeft.x)
            return Vector3.right;
        else if (position.x >= cameraTopRight.x)
            return Vector3.left;
        else if (position.y <= cameraBottomLeft.y)
            return Vector3.up;
        else if (position.y >= cameraTopRight.y)
            return Vector3.down;

        return Vector3.zero;
    }
    public void SetState()
    {
        moveSpeed = stats[weaponLevel].speed;
        enemyDamage.damage = stats[weaponLevel].damage;
        spawnBetweenTime = stats[weaponLevel].timeBetweenAttack;
        spawnCounter = 0;
    }
}
