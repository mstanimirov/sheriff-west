using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public int levelAt;

    public PlayerData(GameManager gameManager)
    {

        levelAt = gameManager.levelAt;

    }

}
