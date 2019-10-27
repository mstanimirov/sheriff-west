using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [Header("General Settings:")]
    public Transform shotPoint;
    public ShooterType shooterType;

    public enum ShooterType
    {

        Enemy,
        Player

    }

    public void Shoot()
    {

        ObjectPooler.instance.GetFromPool(shooterType == ShooterType.Enemy ? Constants.bulletPoolTagE : Constants.bulletPoolTagP, shotPoint.position, shotPoint.rotation);

    }

}
