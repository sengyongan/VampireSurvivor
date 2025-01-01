using UnityEngine;
/* 玩家管理器 */
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private static PlayerManager instance;
    public static Vector2 Position/* 属性（Property） */
    {
        get { return instance.playerTransform.position; }/* 属性访问器，只读的，读取 Position 属性的值时，应该返回什么 */
    }
    private void Awake()
    {
        instance = this;
    }
}
