using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BotBaseState
{
    public abstract void OnEnter(BotStateMachine bot);

    public abstract void OnExecute(BotStateMachine bot);

    public abstract void OnExit(BotStateMachine bot);
}
