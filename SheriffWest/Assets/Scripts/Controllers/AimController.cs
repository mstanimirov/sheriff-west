using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{

    [Header("General Settings:")]
    public float speed;

    #region Private Vars

    private bool updatePosition;
    private Transform target;

    #endregion

    private void Update()
    {

        if (target)
        {

            if (updatePosition)
            {

                if (transform.position != target.position)
                {

                    Vector3 desiredPosition = target.position;
                    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, speed * Time.deltaTime);

                    transform.position = smoothedPosition;

                }
                else
                    updatePosition = false;

            }

        }

    }

    public void SetTarget(Transform newTarget)
    {

        target = newTarget;

    }

    public void UpdatePosition()
    {

        updatePosition = true;

    }

}
