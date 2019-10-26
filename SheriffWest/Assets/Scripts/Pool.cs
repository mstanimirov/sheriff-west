using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{

    [HideInInspector]
    public static Pool instance;

    public GameObject bullet;

    private void Awake()
    {

        if (instance != null)
            Destroy(gameObject);

        instance = this;

    }

    public GameObject GetBullet(Vector3 position, Quaternion rotation) {

        bullet.transform.position = position;
        bullet.transform.rotation = rotation;

        bullet.SetActive(true);

        return bullet;

    }

    public void ReturnBullet() {

        bullet.SetActive(false);
        bullet.transform.position = Vector3.zero;

    }

}
