using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAfterDeathState : BotBaseState
{
    public override void OnEnter(BotStateMachine bot) {  }

    public override void OnExecute(BotStateMachine bot)
    {
        if (bot.gameObject.activeInHierarchy == true)
            bot.SwitchState(bot.WaitState);
    }

    public override void OnExit(BotStateMachine bot) { }
}
