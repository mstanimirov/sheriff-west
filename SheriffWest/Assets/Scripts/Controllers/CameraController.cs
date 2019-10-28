using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [Header("Reference:")]
    public Camera mainCamera;

    [Header("Settings:")]
    public int speed;
    public Vector3 offset;

    #region Get/Set Methods

    public Transform Target { get; set; }

    #endregion

    private void Awake()
    {

        if (instance != null)
            Destroy(gameObject);

        instance = this;

    }

    private void Update()
    {

        if (Target == null)
            return;

        if (transform.position != Target.position)
        {

            Vector3 desiredPosition = Target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, speed * Time.deltaTime);

            transform.position = smoothedPosition;

        }

    }

    #region Camera Shake

    public void Shake(float duration, float magnitude)
    {

        StartCoroutine(Shaker(duration, magnitude));

    }

    public IEnumerator Shaker(float duration, float magnitude)
    {

        float elapsed = 0.0f;

        Vector3 originalPos = mainCamera.transform.localPosition;
        Vector3 newPosition = mainCamera.transform.localPosition;

        while (elapsed < duration)
        {

            float x = Random.Range(-1f, 1f) * magnitude;
            float z = Random.Range(-1f, 1f) * magnitude;

            newPosition = new Vector3(x, originalPos.y, z);

            mainCamera.transform.localPosition = newPosition;
            elapsed += Time.deltaTime;

            yield return null;

        }

        mainCamera.transform.localPosition = originalPos;

    }

    #endregion
}
