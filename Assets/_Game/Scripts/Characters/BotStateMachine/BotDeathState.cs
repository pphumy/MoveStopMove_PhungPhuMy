using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotDeathState : BotBaseState
{
    private float timeCounter = 0;

    public override void EnterState(BotStateMachine bot)
    {
        timeCounter = 0;
        bot.agent.speed = 0;
        bot.botAnimator.SetBool(Constant.ANIM_IS_DEAD, true);
        bot.DisableLockTarget();

        BotController.Ins.ClearBot();
    }

    public override void UpdateState(BotStateMachine bot)
    {
        timeCounter += Time.deltaTime;
        if (timeCounter > bot.timeDeath)
        {
            BotController.Ins.ReuseBot(bot.botTransform.gameObject);
            bot.SwitchState(bot.AfterDeathState);
            return;
        }
    }

    public override void ExitState(BotStateMachine bot) { }
}
