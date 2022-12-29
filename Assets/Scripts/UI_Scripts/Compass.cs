using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public RawImage compassImage;
    public Transform playerTransform;
    public float angle = 720f;

    public GameObject iconPrefab;
    List<QuestMarker> questMarkers = new List<QuestMarker>();

    public float angle1 = 1f;
    public float angle2 = 0.2f;
    public float angle3 = 1f;

    public float maxDistance = 80f;
    public float maxDistPerc = 0.01f;

    public QuestMarker one;
    public QuestMarker two;
    float compassUnit;
    private void Start()
    {
        compassUnit = compassImage.rectTransform.rect.width / angle;

        AddQuestMarker(one);
        AddQuestMarker(two);
    }


    private void Update()
    {
        compassImage.uvRect = new Rect(playerTransform.localEulerAngles.y / angle, angle1, angle2, angle3);

        foreach(QuestMarker marker in questMarkers)
        {
          
            marker.image.rectTransform.anchoredPosition = GetPosOnCompass(marker);

            float dst = Vector2.Distance(new Vector2(playerTransform.transform.position.x, playerTransform.position.z), marker.position);
            float scale = 0f;

            if(dst < maxDistance)
            {
                scale = 1f - (dst * maxDistPerc);
            }

            marker.image.rectTransform.localScale = Vector3.one * scale;
        }

    }

    public void AddQuestMarker(QuestMarker marker)
    {
        GameObject newMarker = Instantiate(iconPrefab, compassImage.transform);
        marker.image = newMarker.GetComponent<Image>();
        marker.image.sprite = marker.icon;

        questMarkers.Add(marker);
    }

    Vector2 GetPosOnCompass(QuestMarker marker)
    {
        Vector2 playerPos = new Vector2(playerTransform.transform.position.x, playerTransform.transform.position.z);
        Vector2 playerFwd = new Vector2(playerTransform.transform.forward.x, playerTransform.transform.forward.z);

        float angle = Vector2.SignedAngle(marker.position - playerPos, playerFwd);

        return new Vector2(compassUnit * angle, 0f);
    }
}
