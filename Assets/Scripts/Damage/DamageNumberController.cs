using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 玩家伤害单例，控制在何处显示伤害值 */
public class DamageNumberController : MonoBehaviour
{
    public static DamageNumberController instance;
    public DamageNumber damageNumber;
    public Transform canvas;
    private List<DamageNumber> damageNumberPool = new List<DamageNumber>();/* 对象池 */
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ActiveDamageNumberAtLocation(float damage, Vector3 position)
    {
        DamageNumber newDamageNumber = CreateNumberForPool(damageNumber);
        newDamageNumber.SetNumber(damage);
        newDamageNumber.gameObject.SetActive(true);
        newDamageNumber.transform.position = position;
    }
    /* 对象池 */
    public DamageNumber CreateNumberForPool(DamageNumber damageNumber)/* 数字在世界需要在池中的操作 */
    {
        DamageNumber newDamageNumber;
        if (damageNumberPool.Count == 0)/* 当池中为空 */
        {
            newDamageNumber = Instantiate(damageNumber, canvas);
        }
        else/* 否则池中存在，直接取出 */
        {
            newDamageNumber = damageNumberPool[0];
            damageNumberPool.RemoveAt(0);
        }
        return newDamageNumber;
    }
    public void DestroyNumberForPool(DamageNumber damageNumber)/* 数字从世界销毁需要在池中的操作 */
    {
        damageNumber.gameObject.SetActive(false);
        damageNumberPool.Add(damageNumber);
    }
}
