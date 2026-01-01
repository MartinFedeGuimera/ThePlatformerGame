using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float time = 5f;
    private bool hasMoved;
    private bool hasDied;

    private InputController inputController;

    private void Awake()
    {
        GameEventsController.InvokeTimeChanged(time);

        inputController = GetComponent<InputController>();

        hasDied = false;
    }

    private void OnEnable()
    {
        GameEventsController.PlayerDied += StopTime;
        GameEventsController.RoomFinished += ResetTime;
    }
    private void OnDisable()
    {
        GameEventsController.PlayerDied -= StopTime;
        GameEventsController.RoomFinished -= ResetTime;
    }

    private void Update()
    {
        if(hasMoved)
        {
            if(!hasDied)
            {
                time -= Time.deltaTime;
            }

            if(time <= 0 )
            {
                time = 0;
                GameEventsController.InvokeTimeFinished();
            }

            GameEventsController.InvokeTimeChanged(time);
        }
        else if (inputController.GetMoveDirection() != 0)
        {
            hasMoved = true;
        }

    }

    public void DestroyPlayer(float delay = 0)
    {
        StartCoroutine(DestroyPlayerCoroutine(delay));
    }

    private IEnumerator DestroyPlayerCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }

    private void ResetTime(Vector3 nonUsedVector3_0, float nonUsedFloat, Vector3 nonUsedVector3_1)
    {
        time = 5f;
    }

    private void StopTime()
    {
        hasDied = true;
    }
}
