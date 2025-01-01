using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
/* 管理panel的激活 */
public class UiGamesActiveController : MonoBehaviour
{
    /* 栈/list《panel》，动态调整 */
    private Stack<Image> panelList = new Stack<Image>();
    /* 管理映射关系 */
    public Button[] buttons;
    public Image[] panels;
    private Dictionary<Button, Image> buttonToPanelDict = new Dictionary<Button, Image>();
    public Image returnImage;
    private bool isFristDie = true;
    void Start()
    {
        // 初始化字典  
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < panels.Length)
            {
                buttonToPanelDict[buttons[i]] = panels[i];
            }
            else
            {
                Debug.LogWarning("Button without corresponding Panel at index: " + i);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealthController.instance.currentHealth <= 0 && isFristDie)
        {
            /* 如果玩家死亡，停止所有面板激活 */
            StopActiveAllPanel();
            isFristDie = false;

            Time.timeScale = 1;
        }
        /* 任意一个面板打开，就暂停游戏 */
        else if (panelList.Count > 0 || UIController.instance.panel.activeSelf)
        {
            returnImage.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else/* 否则没有打开面板 */
        {
            returnImage.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
    /* 当点击返回按钮，当前栈顶设置为未激活，从栈移除top，当前栈顶设置激活（当不为空） */
    public void OnReturnClick()
    {
        if (UIController.instance.panel.activeSelf)
        {
            UIController.instance.panel.SetActive(false);
        }
        else if (panelList.Count > 0)
        {
            panelList.Peek().gameObject.SetActive(false);
            panelList.Pop();
            if (panelList.Count > 0)
            {
                panelList.Peek().gameObject.SetActive(true);
            }
        }
    }
    /* 每次点击按钮，向栈中添加，（当不为空）当前栈顶设置未激活，新添加的设置激活 */
    public void OnButtonClick(Button clickedButton)
    {
        if (buttonToPanelDict.TryGetValue(clickedButton, out Image panel))
        {
            if (isValid(panel))
            {
                if (panelList.Count > 0)
                {
                    panelList.Peek().gameObject.SetActive(false);
                }
                MoveToTopOfStack(panel);
                panel.gameObject.SetActive(true);
            }
            else
            {
                if (panelList.Count > 0)
                {
                    panelList.Peek().gameObject.SetActive(false);
                }
                panelList.Push(panel);
                panel.gameObject.SetActive(true);
            }
        }

    }
    /* 返回是否已经添加到栈中，如果false，就添加到栈，否则将它移到栈顶*/
    private bool isValid(Image panel)
    {
        if (panelList.Count > 0 && panelList.Contains(panel))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void MoveToTopOfStack(Image element)
    {
        if (panelList.Count == 0 || !panelList.Contains(element))
        {
            return;
        }
        /* 将element后的元素转移到tempStack */
        Stack<Image> tempStack = new Stack<Image>();
        while (panelList.Peek() != element)
        {
            tempStack.Push(panelList.Peek());
            panelList.Pop();
        }
        panelList.Pop();
        /* 重新添加到panelList */
        while (tempStack.Count > 0)
        {
            panelList.Push(tempStack.Pop());
        }
        // 将元素重新推送到栈顶  
        panelList.Push(element);
    }
    private void StopActiveAllPanel()
    {/* 当玩家死亡停止所有面板激活 */
        foreach (Image a in panels)
        {
            a.gameObject.SetActive(false);
        }
        UIController.instance.panel.SetActive(false);/* 技能选择面板 */
        panelList.Clear();
    }
}
