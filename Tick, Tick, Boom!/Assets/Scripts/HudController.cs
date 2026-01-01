using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;

    [SerializeField] Vector3 startingTimeTextPos;

    private void Awake()
    {
        MoveTimeText(Vector3.zero, 0f, startingTimeTextPos);
    }

    private void OnEnable()
    {
        GameEventsController.TimeChanged += UpdateTimeText;
        GameEventsController.RoomFinished += MoveTimeText;
    }

    private void OnDisable()
    {
        GameEventsController.TimeChanged -= UpdateTimeText;
        GameEventsController.RoomFinished -= MoveTimeText;
    }

    private void UpdateTimeText(float time)
    {
        int seconds = Mathf.FloorToInt(time % 60f);
        int centiseconds = Mathf.FloorToInt((time * 100) % 100);

        timeText.text = seconds + ":" + centiseconds;
    }

    private void MoveTimeText(Vector3 nonUsedVector3, float nonUsedFloat, Vector3 newTimeTextPos)
    {
        timeText.rectTransform.anchoredPosition = newTimeTextPos;
    }
}
