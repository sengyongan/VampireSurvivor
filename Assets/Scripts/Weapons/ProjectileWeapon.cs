using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 默认武器：特定方向子弹 */
public class ProjectileWeapon : Weapons
{
    public float speed;
    private float initspeed = 1;
    public float spawnBetweenTime;
    private float spawnCounter;
    public Transform parent;
    public GameObject spawnObject;
    public EnemyDamage enemyDamage;
    public Animator ani;
    public WeaponsBarController weaponsBarController;
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
            /* 生成发射物 */
            spawnCounter = spawnBetweenTime;
            GameObject newSpawnObj = Instantiate(spawnObject, transform.position, transform.rotation, parent);
            newSpawnObj.gameObject.SetActive(true);
            Vector3 dir = LookAtToMouse(newSpawnObj);
            /* 转向，并发射 */
            newSpawnObj.GetComponent<Rigidbody2D>().velocity = (dir).normalized * speed * initspeed;

            ani.SetTrigger("isAttack");
            SoundController.instance.PlayAttackDamageSound();
        }
        if (isUpdateLevWeapon)
        {/* 武器升级 */
            isUpdateLevWeapon = false;
            SetState();
        }
    }
    private Vector3 LookAtToMouse(GameObject newSpawnObj)/* 新生成物在生成的一刻，转向鼠标方向 */
    {
        Vector3 mousePos = Input.mousePosition;/* 鼠标位置 */
        Vector3 worldPos2D = new Vector3(mousePos.x, mousePos.y, 0);
        Vector3 offset = worldPos2D - Camera.main.WorldToScreenPoint(new Vector3(newSpawnObj.transform.position.x, newSpawnObj.transform.position.y, 0));
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;/* 玩家位置-》鼠标位置 */
        newSpawnObj.transform.rotation = Quaternion.Euler(0, 0, angle);

        return new Vector3(offset.x, offset.y, 0);
    }
    public void SetState()
    {
        speed *= stats[weaponLevel].speed;
        enemyDamage.damage = stats[weaponLevel].damage;
        transform.localScale = Vector3.one * stats[weaponLevel].range;
        spawnBetweenTime = stats[weaponLevel].timeBetweenAttack;
        spawnCounter = 0;
    }

}
