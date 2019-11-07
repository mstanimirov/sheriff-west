using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [Header("General Settings:")]
    public Transform shotPoint;
    public ShooterType shooterType;

    public GameObject flash;
    public GameObject burst;
    public GameObject smoke;

    public enum ShooterType
    {

        Enemy,
        Player

    }

    public void Shoot()
    {

        flash.SetActive(false);
        burst.SetActive(false);
        smoke.SetActive(false);

        flash.SetActive(true);
        burst.SetActive(true);
        smoke.SetActive(true);

        CameraController.instance.Shake(.15f, .1f);
        ObjectPooler.instance.GetFromPool(shooterType == ShooterType.Enemy ? Constants.bulletPoolTagE : Constants.bulletPoolTagP, shotPoint.position, shotPoint.rotation);

    }

}
