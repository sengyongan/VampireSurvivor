using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/* 武器升级面板控制 */
public class LevelUpSelectionButton : MonoBehaviour
{
    public Image icon;
    public TMP_Text names;
    public TMP_Text description;
    public TMP_Text speed, damage, range, tbAttack, acount, duration;
    private Weapons currentWeapon;/* 按钮面板对应的武器 */
    public WeaponsBarController weaponsBarController;
    public Sprite defaultIcon;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateButtonDescription(Weapons weapon)/* 更新武器升级面板的信息 */
    {
        if (weapon == null || weapon.weaponLevel >= weapon.stats.Count - 1)/*当最大等级时 */
        {/* 武器不存在 / 武器已经是最大的等级，跳过 */
            // icon.sprite = defaultIcon;
            // names.text = "Unknown";
            // description.text = "Skip";
            currentWeapon = null;
            return;
        }
        else if (weapon.gameObject.activeSelf == false)/* 武器是未激活的 */
        {
            icon.sprite = weapon.icon;
            names.text = weapon.names + " LVL" + weapon.weaponLevel;
            description.text = weapon.upgradeText;

            int nextWeaponLevel = weapon.weaponLevel + 1;
            speed.text = "Speed: " + weapon.stats[weapon.weaponLevel].speed.ToString();
            damage.text = "Damage: " + weapon.stats[weapon.weaponLevel].damage.ToString();
            range.text = "Range: " + weapon.stats[weapon.weaponLevel].range.ToString();
            tbAttack.text = "TBAttacks: " + weapon.stats[weapon.weaponLevel].timeBetweenAttack.ToString();
            acount.text = "AttackCount: " + weapon.stats[weapon.weaponLevel].acount.ToString();
            duration.text = "Duration: " + weapon.stats[weapon.weaponLevel].duration.ToString();
            currentWeapon = weapon;/*  */
        }
        else/* 武器是激活的 */
        {
            int nextWeaponLevel = weapon.weaponLevel + 1;
            icon.sprite = weapon.icon;
            names.text = weapon.names + " LVL" + weapon.weaponLevel + " -> " + "LVL" + nextWeaponLevel;
            description.text = weapon.upgradeText;

            speed.text = "Speed: " + weapon.stats[weapon.weaponLevel].speed.ToString()
             + " + " + (((weapon.stats[nextWeaponLevel].speed) - (weapon.stats[weapon.weaponLevel].speed))).ToString();

            damage.text = "Damage: " + weapon.stats[weapon.weaponLevel].damage.ToString()
             + " + " + (((weapon.stats[nextWeaponLevel].damage) - (weapon.stats[weapon.weaponLevel].damage))).ToString();

            range.text = "Range: " + weapon.stats[weapon.weaponLevel].range.ToString()
             + " + " + (((weapon.stats[nextWeaponLevel].range) - (weapon.stats[weapon.weaponLevel].range))).ToString();

            tbAttack.text = "TBAttacks: " + weapon.stats[weapon.weaponLevel].timeBetweenAttack.ToString()
             + " + " + (((weapon.stats[nextWeaponLevel].timeBetweenAttack) - (weapon.stats[weapon.weaponLevel].timeBetweenAttack))).ToString();

            acount.text = "AttackCount: " + weapon.stats[weapon.weaponLevel].acount.ToString()
             + " + " + (((weapon.stats[nextWeaponLevel].acount) - (weapon.stats[weapon.weaponLevel].acount))).ToString();

            duration.text = "Duration: " + weapon.stats[weapon.weaponLevel].duration.ToString()
             + " + " + (((weapon.stats[nextWeaponLevel].duration) - (weapon.stats[weapon.weaponLevel].duration))).ToString();

            currentWeapon = weapon;/*  */
        }
    }
    public void UpdateCurrentSelectWeapon()
    {
        if (currentWeapon != null)
        {
            if (currentWeapon.gameObject.activeSelf == true)
            {
                currentWeapon.SetUpWeaponLevel();/* 对武器升级 */
                weaponsBarController.AddToWeaponsVecForIndex(currentWeapon);/* 更新武器栏 */
            }
            else
            {
                PlayerController.instance.AddWeapon(currentWeapon);
            }
            UIController.instance.panel.SetActive(false);/* 隐藏武器选择面板 */
            Time.timeScale = 1f;
        }
        else
        {
            UIController.instance.panel.SetActive(false);/* 隐藏武器选择面板 */
            Time.timeScale = 1f;
        }
    }
}
