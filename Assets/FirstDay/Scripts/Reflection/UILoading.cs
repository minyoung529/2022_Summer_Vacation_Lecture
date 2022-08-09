using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Reflection;
using UnityEngine.SceneManagement;

public enum eLoadingState
{
    None,
    Unload,
    GoToScene,
    Done
}
public class UILoading : MonoBehaviour
{
    private AsyncOperation unloadDone, loadLevelDone;
    private eLoadingState eLoadingStat;

    private StringBuilder stringBuilder;

    private const float LOAD_LIMIT_TIME = 1f;
    private float curLoadingTime;

    private void Awake()
    {
        stringBuilder = new StringBuilder();
    }

    private void Start()
    {
        eLoadingStat = eLoadingState.None;
        NextState();
    }

    void NextState()
    {
        stringBuilder.Remove(0, stringBuilder.Length);
        stringBuilder.Append(eLoadingStat.ToString());
        stringBuilder.Append("State");

        MethodInfo mInfo = GetType().GetMethod(stringBuilder.ToString(), BindingFlags.Instance | BindingFlags.NonPublic);
        StartCoroutine((IEnumerator)mInfo.Invoke(this, null));
    }

    private IEnumerator NoneState()
    {
        eLoadingStat = eLoadingState.Unload;

        while (eLoadingStat == eLoadingState.None)
        {
            // 로딩 시작 전에 해야할 일들
            yield return null;
        }

        NextState();
    }

    private IEnumerator Unload()
    {
        unloadDone = Resources.UnloadUnusedAssets();

        while (eLoadingStat == eLoadingState.Unload)
        {
            if (unloadDone.isDone)
            {
                eLoadingStat = eLoadingState.GoToScene;
            }

            yield return null;
        }

        NextState();
    }

    private IEnumerator GoToSceneState()
    {
        loadLevelDone = SceneManager.LoadSceneAsync("MainScene");   // 메인 씬을 어싱크 로드 함
        curLoadingTime = LOAD_LIMIT_TIME;

        while (eLoadingStat == eLoadingState.GoToScene)
        {
            curLoadingTime -= Time.deltaTime;

            if(loadLevelDone.isDone && curLoadingTime <= 0f)
            {
                eLoadingStat = eLoadingState.Done;
            }

            yield return null;
        }

        NextState();
    }

    private IEnumerator DoneState()
    {
        // 로딩이 끝났을 때 처리

        while(eLoadingStat == eLoadingState.Done)
        {
            yield return null;
        }
    }
}
