using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShooter
{

    bool IsDead();

    float GetReactionTime();

    string GetName();

    Vector3 GetPosition();

    #region Combat

    void OnAttack(IDamageable target);

    #endregion

}
