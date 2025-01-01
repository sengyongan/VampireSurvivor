using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int value = 1;
    private bool isMoveToPlayer;
    public float speed;
    public float timeBetween = .2f;/* 防止过多的pickup在同一帧计算 */
    private float pickupCounter;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isMoveToPlayer)
        {/* 移项玩家 */
            transform.position = Vector2.MoveTowards(transform.position, PlayerController.instance.transform.position, speed * Time.deltaTime);
        }
        else
        {/* 每间隔timeBetween的时间，检查经验拾取物是否在玩家范围内 */
            pickupCounter -= Time.deltaTime;
            if (pickupCounter <= 0)
            {
                pickupCounter = timeBetween;
                if (Vector2.Distance(transform.position, PlayerController.instance.transform.position) < PlayerController.instance.pickupRange)
                {
                    isMoveToPlayer = true;
                    speed += PlayerController.instance.moveSpeed;/* 需要保证比玩家速度快 */
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)/* 发生碰撞销毁和增加 */
    {
        if (collider.tag == "Player")
        {
            CoinController.instance.AddConis(value);
            SoundController.instance.PlayPickupEXPSound();
            Destroy(gameObject);
        }
    }
}
