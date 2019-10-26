using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [Header("General Settings:")]
    public Transform shotPoint;

    #region Constants

    private readonly string bulletPoolTag = "Bullet";

    #endregion

    public void Shoot() {

        ObjectPooler.instance.GetFromPool(bulletPoolTag, shotPoint.position, shotPoint.rotation);

    }

}
