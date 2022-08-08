using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : Menu<MainMenu>
{
    public void OnPlayPressed()
    {
        print("GAME PLAY");
    }

    public void OnSettingPressed()
    {
        SettingMenu.Open();
    }

    public void OnCreditPressed()
    {
        CreditMenu.Open();
    }

    public override void OnBackPressed()
    {
        Application.Quit();
    }
}
