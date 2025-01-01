using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 玩家经验单例 */
public class ExperienceLevelController : MonoBehaviour
{
    public static ExperienceLevelController instance;
    public float currentExperience;
    public ExperiencePickup experiencePickup;
    public List<int> expLevels;/* 当玩家等级为此级别，需要获得的经验 */
    public int currentLevel = 1, levelCount = 100;/* 当前等级，等级个数 */
    public UIController uicontroller;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        while (expLevels.Count < levelCount)
        {
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f));/* 算法：每级需要多少经验值 */
        }
        uicontroller.UpdateEXPBar(expLevels[currentLevel], currentExperience, currentLevel);/* 更新经验条 */
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddExperience(float amount)/* 增加经验 */
    {
        currentExperience += amount;
        if (currentExperience >= expLevels[currentLevel])/* 升级 */
        {
            LevelUp();
        }
        UIController.instance.UpdateEXPBar(expLevels[currentLevel], currentExperience, currentLevel);/* 更新经验条 */
    }
    public void instanEXPPickup(Vector3 position, float expToGive)
    {
        Instantiate(experiencePickup, position, Quaternion.identity).value = expToGive;/* 创建经验拾取物 */
    }
    private void LevelUp()
    {
        currentExperience -= expLevels[currentLevel];/* 每次需要的经验都是前面的expLevels累加和 */
        ++currentLevel;
        if (currentLevel >= expLevels.Count)
        {
            currentLevel = expLevels.Count - 1;/* 若等级已到最高，则不再升级 */
        }
        UIController.instance.panel.gameObject.SetActive(true);/* 激活武器选择面板 */

        /* 随机选择武器显示 */
        List<Weapons> weaponsToUpgrade = new List<Weapons>();/* 被更新的武器 */
        List<Weapons> availableWeapons = new List<Weapons>();/* 可被选择的武器 */
        /* 如果未分配库中存在，选1个 */
        availableWeapons.AddRange(PlayerController.instance.unassignedWeapons);
        if (availableWeapons.Count > 0)
        {
            int randomIndex = Random.Range(0, availableWeapons.Count);
            weaponsToUpgrade.Add(availableWeapons[randomIndex]);
            availableWeapons.RemoveAt(randomIndex);
        }
        /* 剩下的从已分配库选择 */
        availableWeapons.AddRange(PlayerController.instance.assignedWeapons);
        for (int i = weaponsToUpgrade.Count; i < 3; i++)
        {
            if (availableWeapons.Count > 0)
            {
                int randomIndex = Random.Range(0, availableWeapons.Count);
                weaponsToUpgrade.Add(availableWeapons[randomIndex]);
                availableWeapons.RemoveAt(randomIndex);
            }
        }
        /* 更新技能选择面板 */
        for (int i = 0; i < weaponsToUpgrade.Count; i++)
        {
            uicontroller.levelUpSelectionButtons[i].UpdateButtonDescription(weaponsToUpgrade[i]);
        }
        /* 如果武器满级不在显示 */
        for (int i = 0; i < uicontroller.levelUpSelectionButtons.Count; i++)
        {
            if (i < weaponsToUpgrade.Count)
            {
                uicontroller.levelUpSelectionButtons[i].gameObject.SetActive(true);
            }
            else
            {
                uicontroller.levelUpSelectionButtons[i].gameObject.SetActive(false);
            }
        }

    }
}
