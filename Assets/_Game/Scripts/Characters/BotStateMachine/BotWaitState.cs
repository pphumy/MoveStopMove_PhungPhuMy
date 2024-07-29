using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotWaitState : BotBaseState
{
    public override void OnEnter(BotStateMachine bot)
    {
        bot.agent.speed = 0;
        bot.botAnimator.SetBool(Constant.ANIM_IS_IDLE, true);
    }

    public override void OnExecute(BotStateMachine bot)
    {
        if (LevelManager.Ins.GetGameState() == Constant.GameState.PLAY)
        {
            bot.SwitchState(bot.MoveState);
        }
    }

    public override void OnExit(BotStateMachine bot) { }
}
