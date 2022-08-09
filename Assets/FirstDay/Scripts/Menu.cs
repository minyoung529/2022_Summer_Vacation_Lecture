using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Menu : MonoBehaviour
{
    // 껍데기, 모형, 템플릿

    public virtual void OnBackPressed()
    {
        // 기능 => 뒤로 가기 and 나가기
        if(MenuManager.Instance)
        {
            MenuManager.Instance.CloseMenu();
        }
    }

}

// 제네릭 싱글톤
public abstract class Menu<T> : Menu where T : Menu<T>
{
    private static T instance;
    public static T Instance
    {
        get => instance;
    }

    protected virtual void Awake()
    {
        if(instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = (T)this;

        }
    }

    protected virtual void OnDestroy()
    {
        instance = null;
    }

    public static void Open()
    {
        if(MenuManager.Instance && Instance)
        {
            MenuManager.Instance.OpenMenu(Instance);
        }
    }
}