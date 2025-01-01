using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/* 颜色效果控制 */
public class ColorEffectController : MonoBehaviour
{
    /* 效果 */
    public SpriteRenderer spriteRenderer;
    private Color defaultColor;
    private float timeForHurt = 0.2f;
    private float hurtCounter;
    private bool isHurt;

    public Image panel;
    private Color defaultColor1;
    private float timeForHealthLow = 0.2f;
    private float healthCounter;
    private bool isHealthLow;
    // Start is called before the first frame update
    void Start()
    {
        defaultColor = spriteRenderer.color;
        defaultColor1 = panel.color;
    }

    // Update is called once per frame
    void Update()
    {
        /* 人物sprite效果 */
        hurtCounter -= Time.deltaTime;
        if (isHurt && hurtCounter <= 0)
        {
            isHurt = false;
            spriteRenderer.color = defaultColor;
        }

        healthCounter -= Time.deltaTime;
        if (healthCounter <= 0)
        {
            healthCounter = timeForHealthLow;

            isHealthLow = !isHealthLow;
        }
        /* 周围panel效果 */
        if (PlayerHealthController.instance.currentHealth <= (PlayerHealthController.instance.maxHealth / 4))
        {
            if (isHealthLow)
            {
                panel.color = Color.red;
            }
            else
            {
                panel.color = defaultColor1;
            }
        }
        else
        {
            panel.color = defaultColor1;
        }
    }
    public void spriteHurt()
    {
        spriteRenderer.color = Color.red;
        hurtCounter = timeForHurt;
        isHurt = true;
    }
}
