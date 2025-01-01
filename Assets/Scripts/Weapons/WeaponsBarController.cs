using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/* 武器栏控制 */
public class WeaponsBarController : MonoBehaviour
{
    public Transform imagesRoot, buttonsRoot;
    public Image[] images;/* 12 */
    public Button[] buttons;
    public Weapons[] weapons;
    public int maxSlots = 12;
    public WeaponsLibrary weaponsLibrary;
    public Sprite defaultIcon;
    public BackpackController backpackController;
    void Awake()
    {
        InitializeSlots();
    }
    // Start is called before the first frame update
    void Start()
    {
    }
    private void InitializeSlots()
    {
        images = new Image[maxSlots];
        buttons = new Button[maxSlots];
        weapons = new Weapons[maxSlots];
        AddToVec();
        AddToWeaponsVec();
    }
    void Update()
    {
    }
    /* 遍历所有子物体，添加到数组 */
    private void AddToVec()
    {
        for (int i = 0; i < maxSlots; i++)
        {
            images[i] = imagesRoot.GetChild(i).GetComponent<Image>();
            buttons[i] = buttonsRoot.GetChild(i).GetComponent<Button>();
            weapons[i] = null;
        }
    }

    /* 遍历所有武器，如果等级>=0极，就可以添加到Weapons数组，并更新图标 */
    private void AddToWeaponsVec()
    {
        foreach (var weapon in weaponsLibrary.weaponsList)
        {
            if (weapon.weaponLevel >= 1)
            {
                int index = GetEmptySlotForWeaponsVec();
                if (index != -1)
                {
                    weapons[index] = weapon;
                    UpdateIcon(index);
                }
            }
        }
    }
    /* 添加特定武器，如果武器存在就不用添加，否则添加到空位 */
    public void AddToWeaponsVecForIndex(Weapons weapon)
    {
        int index = weaponsLibrary.playerWeaponsLibrary[weapon];
        bool isValid = false;
        for (int i = 0; i < maxSlots; i++)
        {
            if (weapons[i] != null && weaponsLibrary.playerWeaponsLibrary[weapons[i]] == index)
            {
                isValid = true;
                break;
            }
        }
        if (!isValid)
        {
            int i = GetEmptySlotForWeaponsVec();
            weapons[i] = weapon;
            UpdateIcon(i);
        }

    }
    /* 找到Weapons数组的空位 */
    private int GetEmptySlotForWeaponsVec()
    {
        for (int i = 0; i < maxSlots; i++)
        {
            if (weapons[i] == null)
            {
                return i;
            }
        }
        return -1;
    }
    /* 更新指定索引的图标 */
    public void UpdateIcon(int index)
    {
        if (weapons[index] == null)
        {
            images[index].sprite = defaultIcon;
        }
        else if (index >= 0 && index < maxSlots)
        {
            images[index].sprite = weapons[index].icon;
        }
    }
    /* 循环button数组，找到按钮对应的索引 */
    public int GetIndexForButtons(Button button)
    {
        for (int i = 0; i < maxSlots; i++)
        {
            if (buttons[i] == button)
            {
                return i;
            }
        }
        return -1;
    }
    /* 根据索引返回图片 */
    public Image GetIndexForImage(int index)
    {
        if (index >= 0 && index < maxSlots)
            return images[index];
        return null;
    }
    /* 根据索引返回武器 */
    public Weapons GetIndexForWeapons(int index)
    {
        if (index >= 0 && index < maxSlots)
            return weapons[index];
        return null;
    }
    /* 交换数据 */
    public void SwapData(int index1, int index2)
    {
        if (index1 >= 0 && index1 < maxSlots && index2 >= 0 && index2 < maxSlots)
        {
            Weapons temp = weapons[index1];
            weapons[index1] = weapons[index2];
            weapons[index2] = temp;
            UpdateIcon(index1);
            UpdateIcon(index2);
        }
    }
    /* 在索引添加数据 */
    public void AddWeapon(int index, Weapons weapon)
    {
        weapons[index] = weapon;
        UpdateIcon(index);
    }
    /* 在索引移除数据 */
    public void RemoveWeapon(int index)
    {
        weapons[index] = null;
        UpdateIcon(index);
    }
    /* 将Temp的图片全部更新 */
    public void UpdateTempIcons()
    {
        if (backpackController.isOpenBackpack)
        {
            for (int i = 0; i < maxSlots; i++)
            {
                backpackController.tempImages[i].sprite = images[i].sprite;
            }
        }
    }
}
