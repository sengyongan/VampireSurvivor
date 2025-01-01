using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/* GUI圆形经验 */
public class UIExpCircleBar : MonoBehaviour
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
        currentPadding.w = ScaleCurrentHaalth(CoinController.instance.currentCoins);
        rectMask2D.padding = currentPadding;
    }
    private float ScaleCurrentHaalth(float value)
    {/* 将经验映射到mask的范围 */
        float scale = (maxMaskTopValue - minMaskTopValue) / (CoinController.instance.maxCoins);
        return maxMaskTopValue - (value * scale);/* 需要做减法 */
    }
}
