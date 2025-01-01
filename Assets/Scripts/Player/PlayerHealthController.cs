using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/* 玩家生命单例 */
public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public float currentHealth, maxHealth;
    public Slider healthSlider;
    public Animator ani;
    public float dieAniTime = 1f;
    private float dieCounter;
    private bool isDie;
    public ColorEffectController colorEffectController;
    public CameraController cameraController;
    public float duration = 0.25f;
    public float amplitude = 1f;
    public float frequency = 1f;
    public GameObject dieEffect;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        dieCounter -= Time.deltaTime;
        if (isDie && dieCounter <= 0f)/* 设置取消激活角色 */
        {
            gameObject.SetActive(false);
        }
    }
    /* 受到伤害 */
    public void TackDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)/* 死亡 */
        {
            dieCounter = dieAniTime;
            Instantiate(dieEffect, transform.position, transform.rotation);
            ani.SetBool("isDie", true);
            SoundController.instance.PlayDieSound();
            isDie = true;
        }
        healthSlider.value = currentHealth;/* 血条 */
        ani.SetTrigger("isHurt");
        SoundController.instance.PlayTackDamageSound();
        colorEffectController.spriteHurt();/* 颜色变化 */
        cameraController.CameraShake(duration, amplitude, frequency);/* 震动屏幕 */
    }

}
