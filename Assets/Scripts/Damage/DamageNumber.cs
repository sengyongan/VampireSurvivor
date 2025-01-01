using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/* 伤害的数字 */
public class DamageNumber : MonoBehaviour
{
    public TMP_Text text;
    public float lifeTime;/* 文本的生命周期 */
    private float lifeCounter;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        lifeCounter = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        lifeCounter -= Time.deltaTime;
        transform.position += Vector3.up * speed * Time.deltaTime;
        if (lifeCounter <= 0)
        {
            Destroy(gameObject);
            DamageNumberController.instance.DestroyNumberForPool(this);/* 数字销毁存放到池内 */
        }
    }
    public void SetNumber(float damageCount)
    {
        text.text = damageCount.ToString();
    }
}
