using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class MenuManager : MonoBehaviour
{
    /*
    - �޴� �Ŵ����� �̱���
    - ������ �޴����� �������� ����
    - ������ menu�� ������ >> Menu.cs <<�� �߻� Ŭ������
    */

    // �ʿ��� �޴����� ������ ��ũ��
    public MainMenu mainMenuPrefab;
    public SettingMenu settingMenuPrefab;
    public CreditMenu creditMenuPrefab;

    // �θ� ������Ʈ�� �� Ʈ������
    [SerializeField]
    private Transform menuParent;

    // �������� ĵ���� �޴��� ������
    private Stack<Menu> menuStack = new Stack<Menu>();

    private static MenuManager instance;
    public static MenuManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            InitMenus();

            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
    private void InitMenus()
    {
        if (!menuParent)
        {
            GameObject menuObj = new GameObject("menus");
            menuParent = menuObj.transform;
        }

        DontDestroyOnLoad(menuParent.gameObject);

        // C# ���÷��� ����� ���� �Լ� Ÿ���� ���ͼ� ����
        Type myType = GetType();
        BindingFlags myFlag = BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly;
        FieldInfo[] fields = myType.GetFields(myFlag);

        // ���� Ȯ�ο�
        for(int i = 0; i < fields.Length; i++)
        {
            print(fields[i].Name);
        }

        foreach(FieldInfo field in fields)
        {
            Debug.Log("Sdf");
            Menu prefab = field.GetValue(this) as Menu;

            if(prefab)
            {
                Menu menuInstance = Instantiate(prefab, menuParent);

                // ó�� ���� �޴��� ���� �޴�
                if (prefab != mainMenuPrefab)
                {
                    menuInstance.gameObject.SetActive(false);
                }
                else
                {
                    OpenMenu(menuInstance);
                }
            }
        }
    }

    // ���ÿ� �ִ� �޴��� ������ SetActive(true)
    public void OpenMenu(Menu menuInstance)
    {
        if (!menuInstance)
        {
            Debug.LogWarning("[Null Reference] Menu Open Error");
            return;
        }

        // �����ִ� â���� ��� ��Ȱ��ȭ
        if (menuStack.Count > 0)
        {
            foreach (Menu menu in menuStack)
            {
                menu.gameObject.SetActive(false);
            }
        }

        // �������ϴ� ĵ������ ���� ���ÿ� �־��ش�
        menuInstance.gameObject.SetActive(true);
        menuStack.Push(menuInstance);
    }

    // ������ 0�� �� ������ ������ �޴� �ݾ��ֱ�
    // ���� �� �� 4�ñ��� �̰� �ϰ�
    // �� ������...
    public void CloseMenu()
    {
        if (menuStack.Count == 0)
        {
            Debug.LogWarning("Close Menu Error");
            return;
        }

        Menu topMenu = menuStack.Pop();
        topMenu.gameObject.SetActive(false);

        if (menuStack.Count > 0)
        {
            Menu nextMenu = menuStack.Peek();
            nextMenu.gameObject.SetActive(true);
        }
    }
}
