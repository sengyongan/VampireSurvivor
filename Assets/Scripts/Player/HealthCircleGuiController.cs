using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/* GUI圆形血量 */
public class HealthCircleGuiController : MonoBehaviour
{
    public RectMask2D rectMask2D;
    public float minMaskTopValue;/* 17 */
    public float maxMaskTopValue;/* 137 */
    // Start is called before the first frame update
    void Start()
    {
        Vector4 currentPadding = rectMask2D.padding;
        currentPadding.w = minMaskTopValue;/* 这里17是满的，137是空的 */
        rectMask2D.padding = currentPadding;
    }

    // Update is called once per frame
    void Update()
    {
        Vector4 currentPadding = rectMask2D.padding;
        currentPadding.w = ScaleCurrentHaalth(PlayerHealthController.instance.currentHealth);
        rectMask2D.padding = currentPadding;
    }
    private float ScaleCurrentHaalth(float currentHealth)
    {/* 将血量映射到mask的范围 */
        float scale = (maxMaskTopValue - minMaskTopValue) / (PlayerHealthController.instance.maxHealth);
        return maxMaskTopValue - (currentHealth * scale);/* 需要做减法 */
    }
}
