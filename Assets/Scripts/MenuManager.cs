using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class MenuManager : MonoBehaviour
{
    /*
    - 메뉴 매니저는 싱글톤
    - 각각의 메뉴들을 스택으로 관리
    - 각각의 menu를 관리할 >> Menu.cs <<를 추상 클래스로
    */

    // 필요한 메뉴들의 프리팹 링크용
    public MainMenu mainMenuPrefab;
    public SettingMenu settingMenuPrefab;
    public CreditMenu creditMenuPrefab;

    // 부모 오브젝트로 쓸 트랜스폼
    [SerializeField]
    private Transform menuParent;

    // 스택으로 캔버스 메뉴를 관리함
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

        // C# 리플렉션 기능을 통한 함수 타입을 얻어와서 통합
        Type myType = GetType();
        BindingFlags myFlag = BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly;
        FieldInfo[] fields = myType.GetFields(myFlag);

        // 실제 확인용
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

                // 처음 오픈 메뉴는 메인 메뉴
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

    // 스택에 있는 메뉴를 꺼내고 SetActive(true)
    public void OpenMenu(Menu menuInstance)
    {
        if (!menuInstance)
        {
            Debug.LogWarning("[Null Reference] Menu Open Error");
            return;
        }

        // 열려있는 창들을 모두 비활성화
        if (menuStack.Count > 0)
        {
            foreach (Menu menu in menuStack)
            {
                menu.gameObject.SetActive(false);
            }
        }

        // 열고자하는 캔버스를 열고 스택에 넣어준다
        menuInstance.gameObject.SetActive(true);
        menuStack.Push(menuInstance);
    }

    // 스택이 0일 될 때까지 꺼내고 메뉴 닫아주기
    // 오늘 할 일 4시까지 이거 하고
    // 그 다음에...
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
