using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{

    [Header("General Settings: ")]
    public int levelToLoad;

    public void LoadLevel() {

        GameManager.instance.LoadLevel(levelToLoad);

    }

}
