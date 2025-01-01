using UnityEngine;
using UnityEngine.Events;

public class Death : MonoBehaviour
{
    [SerializeField] private UnityEvent Died;
    public void CheckDeath(int health)
    {
        if (health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        gameObject.SetActive(false);
        Died.Invoke();
    }
}