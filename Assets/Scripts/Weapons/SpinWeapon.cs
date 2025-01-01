using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 旋转武器 */
public class SpinWeapon : Weapons
{
    public float rotateSpeed;
    private float initspeed = 1;
    public Transform holder, fireBallHolder;
    public float spawnBetweenTime;
    private float spawnCounter;
    public EnemyDamage enemyDamage;/* 类类型为引用，而非拷贝 */
    public UIController uiController;
    // Start is called before the first frame update
    void Start()
    {
        SetState();
    }

    // Update is called once per frame
    void Update()
    {

        /* 生成技能 */
        holder.rotation = Quaternion.Euler(0, 0, holder.transform.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime * initspeed));
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = spawnBetweenTime;
            for (int i = 0; i < stats[weaponLevel].acount; i++)
            {
                float rot = (360f / stats[weaponLevel].acount) * i;/* 计算多个球体的角度 */
                Instantiate(fireBallHolder, fireBallHolder.position, Quaternion.Euler(0f, 0f, rot), holder).gameObject.SetActive(true);
            }
        }


        if (isUpdateLevWeapon)
        {/* 武器升级 */
            isUpdateLevWeapon = false;
            SetState();
        }

    }
    public void SetState()
    {
        initspeed = stats[weaponLevel].speed;
        enemyDamage.damage = stats[weaponLevel].damage;
        transform.localScale = Vector3.one * stats[weaponLevel].range;
        spawnBetweenTime = stats[weaponLevel].timeBetweenAttack;
        enemyDamage.liftTime = stats[weaponLevel].duration;
        spawnCounter = 0;
    }
}
