using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{

    string GetName();

    Vector3 GetPosition();

    #region Damage

    void TakeDamage();

    #endregion

}
