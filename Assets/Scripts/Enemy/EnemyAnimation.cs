using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 敌人动画 */
public class EnemyAnimation : MonoBehaviour
{
    public Transform sprite;
    public float speed;
    public float minSize;
    public float maxSize;
    private float targetSize;
    // Start is called before the first frame update
    void Start()
    {
        targetSize = maxSize;
        speed = speed * Random.Range(.75f, 1.25f);
    }

    // Update is called once per frame
    void Update()
    {
        /* 缩放 */
        sprite.localScale = Vector3.MoveTowards(sprite.localScale, Vector3.one * targetSize, speed * Time.deltaTime);
        if (sprite.localScale.x == targetSize)
        {
            if (targetSize == maxSize) targetSize = minSize;
            else if (targetSize == minSize) targetSize = maxSize;
        }
    }
}
