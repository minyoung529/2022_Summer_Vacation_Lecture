using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageController : MonoBehaviour
{
    public GameObject messagePanel;
    public Text messageText;

    private StringBuilder stringBuilder;

    public void ShowMessage(GameObject obj)
    {
        StartCoroutine(ShowCoroutine(obj));
    }

    private IEnumerator ShowCoroutine(GameObject obj)
    {
        stringBuilder.Remove(0, stringBuilder.Length);
        stringBuilder.AppendFormat("{0}∏¶ »πµÊ«ﬂΩ¿¥œ¥Á!", obj.name);

        messageText.text = stringBuilder.ToString();
        messagePanel.SetActive(true);

        yield return new WaitForSeconds(3f);

        messagePanel.SetActive(false);
    }
}
