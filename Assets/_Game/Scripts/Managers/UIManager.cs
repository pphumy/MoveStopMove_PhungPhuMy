using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Numerics;

public enum UIID
{
    UICMainMenu = 0,
    UICGameplay = 1,
    UICVictory = 2,
    UICFail = 3,
    UICWeaponShop = 4,
    UICSkinShop = 5,
    UICCoin = 6,
    UICSetting = 7,
    UICRevive = 8,
    UICFreeItem = 9
}

public class UIManager : Singleton<UIManager>
{
    public Player player;

    public UIID startCanvasId;
    public List<UIID> startUIID;
    public List<UICanvas> startUICanvas;
    
    private Dictionary<UIID, UICanvas> UICanvas = new Dictionary<UIID, UICanvas>();

    public Transform CanvasParentTF;

    private void Start()
    {
        for (int i = 0; i < startUIID.Count; i++)
        {
            UICanvas[startUIID[i]] = startUICanvas[i];
        }

        OpenUI(startCanvasId);
        OpenUI(UIID.UICCoin);
    }

    #region Canvas

    public bool IsOpenedUI(UIID ID)
    {
        return UICanvas.ContainsKey(ID) && UICanvas[ID] != null && UICanvas[ID].gameObject.activeInHierarchy;
    }

    public UICanvas GetUI(UIID ID)
    {
        if (!UICanvas.ContainsKey(ID) || UICanvas[ID] == null)
        {
            UICanvas canvas = Instantiate(Resources.Load<UICanvas>("UI/" + ID.ToString()), CanvasParentTF);
            UICanvas[ID] = canvas;
        }

        return UICanvas[ID];
    }

    public T GetUI<T>(UIID ID) where T : UICanvas
    {
        return GetUI(ID) as T;
    }

    public UICanvas OpenUI(UIID ID)
    {
        UICanvas canvas = GetUI(ID);

        canvas.Setup();
        canvas.Open();

        return canvas;
    }

    public T OpenUI<T>(UIID ID) where T : UICanvas
    {
        return OpenUI(ID) as T;
    }

    public bool IsOpened(UIID ID)
    {
        return UICanvas.ContainsKey(ID) && UICanvas[ID] != null;
    }

    #endregion

}
