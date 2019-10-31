using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShooter
{

    bool IsDead();

    float GetReactionTime();

    string GetCharacterName();

    Sprite GetThumb();

    Vector3 GetPosition();

    CharacterStats GetCharacterStats();

    #region Combat

    void OnAttack(IDamageable target);

    #endregion

}
