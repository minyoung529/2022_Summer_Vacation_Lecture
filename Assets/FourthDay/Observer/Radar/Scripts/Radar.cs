using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarObject
{
    public Image icon { get; set; }
    public GameObject owner { get; set; }

}

public class Radar : MonoBehaviour
{
    // �÷��̾��� ��ġ
    public Transform playerTransform;

    // ���̴� �ȿ� ���� ��ü���� ��Ƶ� ����Ʈ
    public static List<RadarObject> radarObjects = new List<RadarObject>();

    // ���̴� ������ ����
    private const float MAP_SCALE = 2.0f;

    // �Ÿ� ���

    // �÷��̾� Ʈ�������� ���Ϸ� ���� (Y)

    public GameObject playerIcon;
    public GameObject eggIconPrefab;

    private void Update()
    {
        DrawRadarDots();
    }

    private void DrawRadarDots()
    {
        foreach (RadarObject obj in radarObjects)
        {
            Vector3 radarPos = (obj.owner.transform.position - playerTransform.position);

            float distanceToObject = Vector3.Distance(playerTransform.position, obj.owner.transform.position) * MAP_SCALE;
            float deltaY = Mathf.Atan2(radarPos.x, radarPos.z) * Mathf.Rad2Deg - 270f - playerTransform.eulerAngles.y;

            radarPos.x = distanceToObject * Mathf.Cos(deltaY * Mathf.Deg2Rad) * -1f;
            radarPos.z = distanceToObject * Mathf.Sin(deltaY * Mathf.Deg2Rad);

            // ���̴��� ǥ�ø� �غ���
            obj.icon.transform.SetParent(transform);

            RectTransform rt = GetComponent<RectTransform>();

            obj.icon.transform.position = new Vector3(radarPos.x + rt.pivot.x, radarPos.z + rt.pivot.y, 0) + transform.position;
        }
    }

    // �̺�Ʈ �����ʸ� �߰��� ���Ŀ� �Լ��� ����
    public void ItemDropped(GameObject obj)
    {
        RegisterRadarObject(obj, obj.GetComponent<Item>().icon);
    }

    public static void RegisterRadarObject(GameObject obj, Image icon)
    {
        Image image = Instantiate(icon);
        radarObjects.Add(new RadarObject() { owner = obj, icon = image });
    }

    // Remove => Pick up
    public void ItemPickedUp(GameObject obj)
    {
        Debug.Log("Pickup");
        RadarObject rObj = radarObjects.Find(x => x.owner == obj);
        radarObjects.Remove(rObj);

        Destroy(rObj.icon.gameObject, 3f);
    }
}
