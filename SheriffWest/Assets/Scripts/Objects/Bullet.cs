using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("Bullet Settings: ")]
    public float speed;
    public float lifeTime;

    public LayerMask whatIsShootable;

    #region Constants

    private readonly string bulletPoolTag = "Bullet";

    #endregion

    private void Update()
    {

        float moveDistance = speed * Time.deltaTime;

        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);

    }

    private void CheckCollisions(float moveDistance)
    {

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, moveDistance, whatIsShootable, QueryTriggerInteraction.Collide))
            OnHitObject(hitInfo);

    }

    private void OnHitObject(RaycastHit hitInfo)
    {

        IDamageable objToDamage = hitInfo.collider.GetComponent<IDamageable>();

        if (objToDamage != null)
            objToDamage.TakeDamage(-1);

        ObjectPooler.instance.ReturnToPool(bulletPoolTag, gameObject);

    }

}
