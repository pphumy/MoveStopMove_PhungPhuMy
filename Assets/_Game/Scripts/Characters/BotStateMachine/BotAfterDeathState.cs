using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAfterDeathState : BotBaseState
{
    public override void EnterState(BotStateMachine bot) {  }

    public override void UpdateState(BotStateMachine bot)
    {
        if (bot.gameObject.activeInHierarchy == true)
            bot.SwitchState(bot.WaitState);
    }

    public override void ExitState(BotStateMachine bot) { }
}
