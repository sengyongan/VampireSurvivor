using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 显示玩家鼠标的方向 */
public class PlayerOrientationDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;/* 鼠标位置 */
        Vector3 worldPos2D = new Vector3(mousePos.x, mousePos.y, 0);
        Vector3 offset = worldPos2D - Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y, 0));
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;/* 玩家位置-》鼠标位置 */
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
