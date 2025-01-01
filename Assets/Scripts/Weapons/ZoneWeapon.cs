using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 范围武器 */
public class ZoneWeapon : Weapons
{
    public EnemyDamage enemyDamage;
    private float spawnTime, spawnCounter;
    public Transform Holder;
    // Start is called before the first frame update
    void Start()
    {
        SetState();
    }

    // Update is called once per frame
    void Update()
    {
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = spawnTime;
            Instantiate(enemyDamage, enemyDamage.transform.position, Quaternion.identity, Holder).gameObject.SetActive(true);
        }
        if (isUpdateLevWeapon)
        {/* 武器升级 */
            isUpdateLevWeapon = false;
            SetState();
        }
    }
    public void SetState()
    {
        enemyDamage.damage = stats[weaponLevel].damage;
        transform.localScale = Vector3.one * stats[weaponLevel].range;
        enemyDamage.damageForTime = stats[weaponLevel].speed;
        enemyDamage.liftTime = stats[weaponLevel].duration;
        spawnTime = stats[weaponLevel].timeBetweenAttack;
        spawnCounter = 0;
    }
}
