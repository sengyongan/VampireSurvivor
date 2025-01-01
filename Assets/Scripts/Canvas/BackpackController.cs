using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/* 背包控制 */
public class BackpackController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /* 获取武器栏 */
    /* 一维按钮数组 */
    /* 字典《按钮，武器》 */
    public WeaponsBarController weaponsBarController;
    public Transform buttonsRoot;
    public Transform imagesRoot;
    public Button[] buttons;
    public Image[] iamges;
    public int maxSlots = 32;
    public Dictionary<Button, Weapons> WeaponsDic = new Dictionary<Button, Weapons>();
    public Image dragIcon;
    private bool isDragging = false;
    private int dragStartIndex = -1;
    private Weapons draggedWeapon = null;
    public Sprite defaultIcon;
    /* 控制icon图标在最上方 */
    public Transform tempImagesRoot;
    public Transform realityImages, realityImages2;
    public Image realityImage3;
    public Transform tempImageRoot;
    public Image[] tempImages;

    public bool isOpenBackpack;

    void OnEnable()
    {
        isOpenBackpack = true;
    }
    void OnDisable()
    {
        isOpenBackpack = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        buttons = new Button[maxSlots];
        iamges = new Image[maxSlots];
        tempImages = new Image[12];
        /* 将所有子按钮自动添加到数组中 */
        for (int i = 0; i < maxSlots; i++)
        {
            buttons[i] = buttonsRoot.GetChild(i).GetComponent<Button>();
            iamges[i] = imagesRoot.GetChild(i).GetComponent<Image>();
            WeaponsDic.Add(buttons[i], null);
        }
        for (int i = 0; i < 12; i++)
        {
            tempImages[i] = tempImageRoot.GetChild(i).GetComponent<Image>();
        }
    }
    void Update()
    {
        UpdateTempIcon();
    }
    /* drag */
    /* 
        IBeginDragHandler：开始拖动时调用
        IDragHandler：正在拖动时调用
        IEndDragHandler：拖动结束时调用
    */
    public void OnBeginDrag(PointerEventData eventData)
    {
        Button draggedButton = eventData.pointerCurrentRaycast.gameObject.GetComponent<Button>();
        if (draggedButton != null)
        {
            dragStartIndex = GetIndexForButtons(draggedButton);
            if (dragStartIndex >= 0)
            {
                WeaponsDic.TryGetValue(draggedButton, out draggedWeapon);
                dragIcon.sprite = iamges[dragStartIndex].sprite;
                isDragging = true;
            }
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            dragIcon.gameObject.SetActive(true);
            dragIcon.transform.position = Input.mousePosition;
            SwitchTempBackground();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        dragIcon.gameObject.SetActive(false);
        UnSwitchTempBackground();
        /* 结束 */
        Button endDragButton = eventData.pointerCurrentRaycast.gameObject.GetComponent<Button>();
        if (endDragButton != null)
        {
            /* 背包内拖拽 */
            if (GetIndexForButtons(endDragButton) != -1)
            {
                int endIndex = GetIndexForButtons(endDragButton);
                if (endIndex != dragStartIndex && draggedWeapon != null)
                {
                    SwapData(dragStartIndex, endIndex);
                }
            }
            /* 从背包拖拽到武器栏 */
            else
            {
                int endIndex = weaponsBarController.GetIndexForButtons(endDragButton);
                Weapons tempWeapon = weaponsBarController.GetIndexForWeapons(endIndex);
                weaponsBarController.AddWeapon(endIndex, draggedWeapon);
                AddWeapon(dragStartIndex, tempWeapon);
            }
        }

        // Optionally, reset dragStartIndex and draggedWeapon to avoid potential issues  
        dragStartIndex = -1;
        draggedWeapon = null;
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
    /* 更新指定索引的图标 */
    private void UpdateIcon(int index)
    {
        if (WeaponsDic[buttons[index]] == null)
        {
            iamges[index].sprite = defaultIcon;
        }
        else if (index >= 0 && index < maxSlots)
        {
            iamges[index].sprite = WeaponsDic[buttons[index]].icon;
        }
    }
    /* 交换数据 */
    public void SwapData(int index1, int index2)
    {
        if (index1 >= 0 && index1 < maxSlots && index2 >= 0 && index2 < maxSlots)
        {
            Weapons temp = WeaponsDic[buttons[index1]];
            WeaponsDic[buttons[index1]] = WeaponsDic[buttons[index2]];
            WeaponsDic[buttons[index2]] = temp;
            UpdateIcon(index1);
            UpdateIcon(index2);
        }
    }
    /* 移除当前index的数据 */
    private void RemoveSlotDate(int index)
    {
        WeaponsDic[buttons[index]] = null;
        UpdateIcon(index);
    }
    /* 添加当前index的数据 */
    public void AddWeapon(int index, Weapons weapon)
    {
        WeaponsDic[buttons[index]] = weapon;
        UpdateIcon(index);
    }
    /* 根据索引找到武器 */
    public Weapons GetWeaponByIndex(int index)
    {
        return WeaponsDic[buttons[index]];
    }
    public void SwitchTempBackground()
    {
        tempImagesRoot.gameObject.SetActive(true);
        UpdateTempIcon();
        realityImages.gameObject.SetActive(false);
        realityImages2.gameObject.SetActive(false);
        realityImage3.color = new Color(realityImage3.color.r, realityImage3.color.g, realityImage3.color.b, 0);
    }
    public void UnSwitchTempBackground()
    {
        UpdateTempIcon();
        tempImagesRoot.gameObject.SetActive(false);
        realityImages.gameObject.SetActive(true);
        realityImages2.gameObject.SetActive(true);
        realityImage3.color = new Color(realityImage3.color.r, realityImage3.color.g, realityImage3.color.b, 255);
    }
    private void UpdateTempIcon()
    {
        if (isOpenBackpack)
        {
            for (int i = 0; i < 12; i++)
            {
                if (weaponsBarController.weapons[i] == null)
                {
                    tempImages[i].sprite = weaponsBarController.defaultIcon;
                }
                else if (i >= 0 && i < 12)
                {
                    tempImages[i].sprite = weaponsBarController.weapons[i].icon;
                }
            }
        }

    }
}
