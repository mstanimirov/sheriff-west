using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    
    Vector3 GetPosition();

    #region Damage

    void TakeDamage(int amount);

    #endregion

}
