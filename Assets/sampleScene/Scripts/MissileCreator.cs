using UnityEngine;

public class MissileCreator : MonoBehaviour
{
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private Transform playerTransform;

    public void createMissile()
    {
        Instantiate(missilePrefab, playerTransform.position, Quaternion.identity);
    }
}