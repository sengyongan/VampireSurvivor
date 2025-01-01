using UnityEngine;
using UnityEngine.InputSystem;
/* 玩家动画控制 */
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private string walkState;
    [SerializeField] private string idleState;
    [SerializeField] private string attackState;

    public void Move(InputAction.CallbackContext context)
    {
        var inputDirection = context.ReadValue<Vector2>();
        if (inputDirection == Vector2.zero)
        {
            animator.Play(idleState);
        }
        else
        {
            animator.Play(walkState);
        }
        if (inputDirection.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (inputDirection.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
    public void PlayAttackState()
    {
        animator.Play(attackState);

    }
}