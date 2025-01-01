using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 玩家控制器 */
public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public SpriteRenderer spriteRenderer;
    public Animator ani;
    public static PlayerController instance;
    public float pickupRange;
    public List<Weapons> unassignedWeapons, assignedWeapons;/* 玩家未分配武器，和已分配武器库 */
    public WeaponsBarController weaponsBarController;
    public List<Weapons> fullyLevelledWeapons = new List<Weapons>();/* 满级的武器 */
    public int WeaponsCount;

    // Start is called before the first frame update
    void Start()
    {
        WeaponsCount = (unassignedWeapons.Count + assignedWeapons.Count + fullyLevelledWeapons.Count);
        instance = this;
        AddWeapon(Random.Range(0, unassignedWeapons.Count));
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealthController.instance.currentHealth > 0)
        { /* 玩家移动 */
            Vector3 moveInput = new Vector3(0, 0, 0);
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");

            moveInput.Normalize();
            transform.position += moveInput * moveSpeed * Time.deltaTime;
            /* 设置动画 */
            if (moveInput != Vector3.zero)
            {
                ani.SetBool("IsMoving", true);
                /* 设置翻转 */
                if (moveInput.x > 0) spriteRenderer.flipX = false;
                if (moveInput.x < 0) spriteRenderer.flipX = true;
            }
            else
            {
                ani.SetBool("IsMoving", false);
            }
        }
    }
    public void AddWeapon(int index)/* 游戏开始添加随机武器 */
    {
        if (index < unassignedWeapons.Count)
        {
            unassignedWeapons[index].gameObject.SetActive(true);
            assignedWeapons.Add(unassignedWeapons[index]);
            weaponsBarController.AddToWeaponsVecForIndex(unassignedWeapons[index]);
            unassignedWeapons.RemoveAt(index);
        }
    }
    public void AddWeapon(Weapons weapon)
    {
        weapon.gameObject.SetActive(true);
        assignedWeapons.Add(weapon);
        weaponsBarController.AddToWeaponsVecForIndex(weapon);
        unassignedWeapons.Remove(weapon);
    }
}
