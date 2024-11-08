using UnityEngine;
using System.Collections;

public class Player_Rotation : MonoBehaviour
{
    private bool isRotating = false;
    private Quaternion targetRotation;

    // Update is called once per frame
    void Update()
    {
        if (isRotating) return;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            targetRotation = Quaternion.Euler(0, 0, 0); // Face front
            StartCoroutine(RotateToTarget());
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetRotation = Quaternion.Euler(0, 0, 90); // Rotate left 90 degrees
            StartCoroutine(RotateToTarget());
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetRotation = Quaternion.Euler(0, 0, -90); // Rotate right 90 degrees
            StartCoroutine(RotateToTarget());
        }
    }

    private IEnumerator RotateToTarget()
    {
        isRotating = true;
        Quaternion startRotation = transform.rotation;
        float timeElapsed = 0f;
        float duration = 0.1f; // Duration of the rotation

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            t = t * t * (3f - 2f * t); // Smoothstep easing function
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
        isRotating = false;
    }
}



