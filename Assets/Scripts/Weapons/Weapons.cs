using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 武器基类 */
public class Weapons : MonoBehaviour
{
    public List<WeaponStats> stats;/* 不同等级的武器属性 */
    public int weaponLevel;/* 武器等级,索引，0级代表未激活，>=1代表激活 */
    [HideInInspector]
    public bool isUpdateLevWeapon;/* 是否成功升级了武器 */
    /* 武器不变化的属性 */
    public Sprite icon;
    public string names;
    public string upgradeText;
    public void SetUpWeaponLevel()/* 武器升级 */
    {
        if (weaponLevel < stats.Count - 1)
        {
            weaponLevel++;
            isUpdateLevWeapon = true;
            if (weaponLevel >= stats.Count - 1)
            {/* 满级武器 */
                PlayerController.instance.fullyLevelledWeapons.Add(this);
                PlayerController.instance.assignedWeapons.Remove(this);
            }
        }
    }
    public void SetState()/* 更新武器属性 */
    {

    }
}
[System.Serializable]
public class WeaponStats/* 武器随等级变化的属性 */
{
    public float speed, damage, range, timeBetweenAttack, acount, duration;
}