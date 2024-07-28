using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BotBaseState
{
    public abstract void EnterState(BotStateMachine bot);

    public abstract void UpdateState(BotStateMachine bot);

    public abstract void ExitState(BotStateMachine bot);
}
