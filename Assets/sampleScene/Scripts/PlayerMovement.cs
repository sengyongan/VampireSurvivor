using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
/* 玩家移动 */
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;/* SerializeField安全名，将变量显示到引擎上 */
    [SerializeField] private float speed;
    private Vector2 inputDirection;
    public void Move(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();/* 按键向量 */
    }
    private void FixedUpdate()
    {
        var position = (Vector2)transform.position;
        var targetPosition = position + inputDirection;

        if (position == targetPosition) return;/* 防止抖动 */
        rb.DOMove(targetPosition, speed).SetSpeedBased();/* dotween */
    }
}
