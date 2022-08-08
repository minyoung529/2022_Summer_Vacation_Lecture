using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Menu : MonoBehaviour
{
    // ������, ����, ���ø�

    public virtual void OnBackPressed()
    {
        // ��� => �ڷ� ���� and ������
        if(MenuManager.Instance)
        {
            MenuManager.Instance.CloseMenu();
        }
    }

}

// ���׸� �̱���
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