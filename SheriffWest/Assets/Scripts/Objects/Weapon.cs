using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [Header("General Settings:")]
    public Transform shotPoint;
    public ShooterType shooterType;

    public MuzzleFlash muzzleFlash;

    #region Private Vars

    private AudioManager audioManager;

    #endregion

    public enum ShooterType
    {

        Enemy,
        Player

    }

    private void Start()
    {

        audioManager = AudioManager.instance;

    }

    public void Shoot()
    {

        muzzleFlash.Show();

        if (audioManager)
            audioManager.PlaySound(Constants.s_pistolShot);

        CameraController.instance.Shake(.15f, .1f);
        ObjectPooler.instance.GetFromPool(shooterType == ShooterType.Enemy ? Constants.bulletPoolTagE : Constants.bulletPoolTagP, shotPoint.position, shotPoint.rotation);

    }

}
