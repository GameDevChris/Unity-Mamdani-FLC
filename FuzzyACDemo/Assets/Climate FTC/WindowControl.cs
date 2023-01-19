using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowControl : MonoBehaviour
{
    public OutputController windowOutput;
    public float minimumY = -1;
    public float maximumY = 0;
    public float currentOpenPercentage = 0;
    public float currentY = 0;
    public Transform windowToMove;
    public float lerpDuration = 2; 
    bool lerping = false;
    private Coroutine lerpCorouting;
    void Start()
    {
        currentY = windowToMove.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Abs(windowOutput.outputValue - currentOpenPercentage) > 0.1)
        {
            if (lerping)
            {
                StopCoroutine(lerpCorouting);
                lerping = false;
            }
            currentOpenPercentage = windowOutput.outputValue;
            float newY = Mathf.Lerp(minimumY, maximumY, windowOutput.outputValue / 100);
            Vector3 newPosition = new Vector3(windowToMove.localPosition.x, newY, windowToMove.localPosition.z);
            Vector3 originalPosition = windowToMove.localPosition;
            lerpCorouting = StartCoroutine(Lerp(originalPosition, newPosition));
            windowToMove.localPosition = Vector3.Lerp(originalPosition, newPosition, Time.deltaTime);
            currentY = newY;
        }
    }
    
    IEnumerator Lerp(Vector3 oldPos, Vector3 newPos)
    {
        lerping = true;
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            windowToMove.localPosition = Vector3.Lerp(oldPos, newPos, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        lerping = false;
    }
    
}
