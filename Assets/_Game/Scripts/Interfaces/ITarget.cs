using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITarget
{
    public bool CanBeTargeted();

    public void DisableLockTarget();

    public void EnableLockTarget();
}
