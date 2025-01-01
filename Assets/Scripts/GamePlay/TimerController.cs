using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/* 游戏时计时器 */
public class TimerController : MonoBehaviour
{
    public static TimerController instance;
    public float countdownToVictory;
    public float totalTime;
    private bool gameActive;
    public TMP_Text countdown_Text;
    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealthController.instance.currentHealth <= 0)
        {/* 当玩家死亡不执行任何计算时间 */

        }

        else if (countdownToVictory >= 0)/* 倒计时未结束 */
        {
            countdownToVictory -= Time.deltaTime;
            ComputerTime(countdownToVictory);
            totalTime += Time.deltaTime;
        }
        else/* 倒计时结束 */
        {
            totalTime += Time.deltaTime;
            countdown_Text.text = "You have reached your battle time and are free to leave the field! (Click back to town, else Enter Infinite mode)";
        }

    }
    private void ComputerTime(float time)/* 显示倒计时时间 */
    {
        float minutes = time / 60;/* 取整 */
        float seconds = time % 60;/* 取余 */
        countdown_Text.text = "CountdownToVictory: " + Mathf.FloorToInt(minutes) + " M " + Mathf.FloorToInt(seconds).ToString("00") + " S";
    }
}
