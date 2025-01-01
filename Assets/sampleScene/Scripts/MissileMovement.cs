using UnityEngine;
using DG.Tweening;
/* 子弹移动 */
public class MissileMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    private Vector2 direction;
    private GameObject LocateEnemy()
    {
        var results = new Collider2D[5];
        Physics2D.OverlapCircleNonAlloc(transform.position, 10, results);
        foreach (var col in results)
        {
            if (col != null && col.CompareTag("Enemy"))
            {
                return col.gameObject;
            }
        }
        return null;
    }
    private Vector2 MoveDirection(Transform target)
    {
        if (target == null)
        {
            var dir = new Vector3(100, 0, 0);
            direction = (dir - transform.position).normalized;/* 当不存在目标时 */
        }
        else if (target != null)
        {
            direction = (target.transform.position - transform.position).normalized;
        }
        return direction;
    }
    private void Awake()
    {/* 子弹生成，找到敌人 */
        var ememy = LocateEnemy();
        if (ememy == null)
        {
            direction = MoveDirection(null);
        }
        else
        {
            direction = MoveDirection(ememy.transform);
        }
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }
    private void FixedUpdate()
    {
        var targetPosition = (Vector2)transform.position + direction;
        rb.DOMove(targetPosition, speed).SetSpeedBased();
    }
}