using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/* 玩家属性展示面板 */
public class UIPlayerDescriptionController : MonoBehaviour
{
    public static UIPlayerDescriptionController instance;
    public TMP_Text levelText;
    public TMP_Text healthText;
    public TMP_Text experienceText;
    public TMP_Text attributePointText;
    public TMP_Text weaponCountText;
    public TMP_Text moveSpeedText;
    public TMP_Text totalTime;
    public TMP_Text killCountText;
    public TMP_Text injuryAccumulationText;

    public int killCount;
    public float injuryAccumulation;
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = "Level: " + ExperienceLevelController.instance.currentLevel;
        healthText.text = "Health: " + PlayerHealthController.instance.currentHealth;
        experienceText.text = "Experience: " + ExperienceLevelController.instance.currentExperience; ;
        attributePointText.text = "Attribute Points: " + CoinController.instance.currentCoins;
        weaponCountText.text = "Weapon Count: " + playerController.WeaponsCount;
        moveSpeedText.text = "Move Speed: " + playerController.moveSpeed;
        ComputerTime(TimerController.instance.totalTime);
        killCountText.text = "Kill Count: " + killCount;
        injuryAccumulationText.text = "Injury Accumulation: " + injuryAccumulation;
    }
    private void ComputerTime(float time)
    {
        float minutes = time / 60;/* 取整 */
        float seconds = time % 60;/* 取余 */
        totalTime.text = "TotalTime: " + Mathf.FloorToInt(minutes) + " M " + Mathf.FloorToInt(seconds).ToString("00") + " S";
    }
}
