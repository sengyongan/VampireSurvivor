using UnityEngine;
/* 开始，暂停游戏 */
public class TimeManager : MonoBehaviour
{
    public void Stop()
    {
        Time.timeScale = 0;/* 插件 */
    }
    public void Start()
    {
        Time.timeScale = 1;
    }
}