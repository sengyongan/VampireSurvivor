using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
/* 控制开始游戏，重新开始， */
public class GameController : MonoBehaviour
{
    public TMP_Text currentStateText;
    public TMP_Text descriptionText;
    public string mainMenuName;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (TimerController.instance.countdownToVictory >= 0 && PlayerHealthController.instance.currentHealth <= 0)/* 失败 */
        {
            currentStateText.text = "Fail";
            descriptionText.text = "A merciful priest to bring you back to life";
        }
        else if (TimerController.instance.countdownToVictory < 0)/* 玩家胜利 */
        {
            currentStateText.text = "Success";
            descriptionText.text = "This is your victory. You can return to the Empire!";
        }
        else/* 暂时无法判断结果 */
        {
            currentStateText.text = "Unknown";
            descriptionText.text = "You can't be a deserter";
        }
    }
    public void ClickButton()
    {
        if (TimerController.instance.countdownToVictory >= 0 && PlayerHealthController.instance.currentHealth <= 0)
        {
            Time.timeScale = 1;
            /* 重新开始 */
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (TimerController.instance.countdownToVictory < 0)
        {
            /* 返回主菜单 */
            SceneManager.LoadScene(mainMenuName);
        }

    }
}
