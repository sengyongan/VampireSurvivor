using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/* ui总控制面板 */
public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Slider slider;
    public TMP_Text text;
    public List<LevelUpSelectionButton> levelUpSelectionButtons;/* 武器旋选择面板的信息 */
    public GameObject panel;
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {

            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.fullyLevelledWeapons.Count == playerController.WeaponsCount)
        {
            panel.gameObject.SetActive(false);
        }
    }
    public void UpdateEXPBar(float maxEXP, float currentExperience, float curLevel)
    {
        slider.maxValue = maxEXP;
        slider.value = currentExperience;
        text.text = "Level: " + curLevel;
    }
}
