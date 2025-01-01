using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 鼠标控制 */
public class MouseController : MonoBehaviour
{
    public Texture2D customCursor;
    private Vector2 hotSpot;/* 目标点的从左上角开始的纹理偏移 */
    // Start is called before the first frame update
    void Start()
    {
        hotSpot = new Vector2(0, 0);
        Cursor.SetCursor(customCursor, hotSpot, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
