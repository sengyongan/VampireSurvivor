using UnityEngine;
/* 销毁对象 */
public class SelfDestory : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(gameObject);
    }
}