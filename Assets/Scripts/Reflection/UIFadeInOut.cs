using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Reflection;
using System;

public enum eFadeState
{
    None,
    FadeOut,
    ChangeBackground,
    FadeIn,
    Done
}

public class UIFadeInOut : MonoBehaviour
{
    private eFadeState state;
    private Image backgroundImage;
    private IEnumerator iStateCoroutine;

    private const float ALPHA_LIMIT = 1.0f;

    private void Awake()
    {
        backgroundImage = gameObject.GetComponent<Image>();
        if (!backgroundImage)
        {
            backgroundImage = gameObject.AddComponent<Image>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && state == eFadeState.None)
        {
            state = eFadeState.None;
            NextState();
        }
    }

    protected void NextState()
    {
        MethodInfo mInfo = this.GetType().GetMethod(state.ToString(), BindingFlags.Instance | BindingFlags.NonPublic);
        iStateCoroutine = (IEnumerator)mInfo.Invoke(this, null);
        StartCoroutine(iStateCoroutine);
    }

    private IEnumerator None()
    {
        Debug.Log("None");
        while (state == eFadeState.None)
        {
            state = eFadeState.FadeOut;
            yield return null;
        }

        NextState();
    }

    private IEnumerator FadeOut()
    {
        float alpha = 0f;

        Debug.Log("FadeOut");
        while (state == eFadeState.FadeOut)
        {
            if (backgroundImage.color.a < ALPHA_LIMIT)
            {
                alpha += Time.deltaTime;
            }
            else
            {
                state = eFadeState.ChangeBackground;
            }

            alpha = Mathf.Clamp(alpha, 0f, ALPHA_LIMIT);

            Color c = backgroundImage.color;
            c.a = alpha;
            backgroundImage.color = c;

            yield return null;
        }

        NextState();
    }

    private IEnumerator ChangeBackground()
    {
        yield return null;
        Debug.Log("초기화 처리");
        state = eFadeState.FadeIn;
        NextState();
    }

    private IEnumerator FadeIn()
    {
        Debug.Log("FadeIn");

        float alpha = ALPHA_LIMIT;

        while (state == eFadeState.FadeIn)
        {
            if (backgroundImage.color.a > 0f)
            {
                alpha -= Time.deltaTime;
            }
            else
            {
                state = eFadeState.Done;
            }

            alpha = Mathf.Clamp(alpha, 0f, ALPHA_LIMIT);

            Color c = backgroundImage.color;
            c.a = alpha;
            backgroundImage.color = c;

            yield return null;
        }

        NextState();
    }

    private IEnumerator Done()
    {
        Debug.Log("Done");
        yield return null;
        state = eFadeState.None;
        NextState();
    }
}
