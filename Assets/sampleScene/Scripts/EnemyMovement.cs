using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private UnityEvent<Vector2> MoveAnimation;


    private void FixedUpdate()
    {
        var playerPosition = PlayerManager.Position;
        var position = (Vector2)transform.position;
        var direction = playerPosition - position;
        direction.Normalize();
        var targetPosition = position + direction;

        if (position == targetPosition) return;/* 防止抖动 */
        rb.DOMove(targetPosition, speed).SetSpeedBased();

        MoveAnimation.Invoke(direction);

    }
}
