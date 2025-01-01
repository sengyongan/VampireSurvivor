using UnityEngine;
using UnityEngine.Events;
using Timers;

public class AutoCreatorMissile : MonoBehaviour
{
    [SerializeField] private MissileCreator missileCreator;
    [SerializeField] private UnityEvent missileLaunch;
    public void launchMissile()
    {
        missileCreator.createMissile();
        missileLaunch.Invoke();  /* 事件触发 */
    }
    private void Awake()
    {
        TimersManager.SetLoopableTimer(this, 1, launchMissile);/* 循环计时器 */
    }

}