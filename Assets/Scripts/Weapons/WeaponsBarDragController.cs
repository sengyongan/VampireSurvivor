using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/* 武器栏拖放控制 */
public class WeaponsBarDragController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /* drag类 */
    /*
        IBeginDragHandler：开始拖动时调用
        IDragHandler：正在拖动时调用
        IEndDragHandler：拖动结束时调用
    */
    /* 开始拖动，起始槽更新为空（释放所有数据） */
    /* 拖动时，在鼠标位置显示起点的icon， */
    /* 结束拖放如果是button，交换数据，鼠标icon取消显示*/
    public WeaponsBarController weaponsBarController;
    public Image dragIcon;
    public Image dragIcon1;
    private bool isDragging = false;
    private int dragStartIndex = -1;
    private Weapons draggedWeapon = null;
    public BackpackController backpackController;

    void Start()
    {
        dragIcon.gameObject.SetActive(false);
        dragIcon1.gameObject.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)/* 获取起始按钮的索引，并更新dragIcon的图标 */
    {
        Button draggedButton = eventData.pointerCurrentRaycast.gameObject.GetComponent<Button>();
        if (draggedButton != null)
        {
            dragStartIndex = weaponsBarController.GetIndexForButtons(draggedButton);
            if (dragStartIndex >= 0)
            {
                draggedWeapon = weaponsBarController.GetIndexForWeapons(dragStartIndex);
                dragIcon.sprite = weaponsBarController.GetIndexForImage(dragStartIndex).sprite;
                isDragging = true;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)/* dragIcon的图标更新，并随鼠标移动 */
    {
        if (isDragging)
        {
            if (backpackController.isOpenBackpack)/* 当背包打开 */
            {
                dragIcon.gameObject.SetActive(true);
                dragIcon.transform.position = Input.mousePosition;
                backpackController.SwitchTempBackground();
            }
            else
            {
                dragIcon1.sprite = dragIcon.sprite;
                dragIcon1.gameObject.SetActive(true);
                dragIcon1.transform.position = Input.mousePosition;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)/* 取消dragIcon的图标，获取终点按钮的索引，交换数据 */
    {
        isDragging = false;
        dragIcon.gameObject.SetActive(false);
        dragIcon1.gameObject.SetActive(false);
        backpackController.UnSwitchTempBackground();

        Button endDragButton = eventData.pointerCurrentRaycast.gameObject.GetComponent<Button>();
        if (endDragButton != null)
        {
            int endIndex = weaponsBarController.GetIndexForButtons(endDragButton);
            /* 在武器栏拖拽 */
            if (weaponsBarController.GetIndexForButtons(endDragButton) != -1)
            {
                if (endIndex != dragStartIndex && draggedWeapon != null)
                {
                    weaponsBarController.SwapData(dragStartIndex, endIndex);
                }
            }
            else if (backpackController.GetIndexForButtons(endDragButton) != -1)/* 拖拽到武器栏 */
            {
                endIndex = backpackController.GetIndexForButtons(endDragButton);
                Weapons tempWeapon = backpackController.GetWeaponByIndex(endIndex);
                backpackController.AddWeapon(endIndex, draggedWeapon);
                weaponsBarController.AddWeapon(dragStartIndex, tempWeapon);
            }

        }

        // Optionally, reset dragStartIndex and draggedWeapon to avoid potential issues
        dragStartIndex = -1;
        draggedWeapon = null;
    }
}
