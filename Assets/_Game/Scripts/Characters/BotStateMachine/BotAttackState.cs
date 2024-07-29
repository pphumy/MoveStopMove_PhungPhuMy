using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAttackState : BotBaseState
{
    private float timeCounter;

    public override void OnEnter(BotStateMachine bot)
    {
        timeCounter = 0;
        bot.agent.speed = 0;
        bot.botAnimator.SetBool(Constant.ANIM_IS_ATTACK, true);
        bot.Attack();
    }

    public override void OnExecute(BotStateMachine bot)
    {
        timeCounter += Time.deltaTime;
        if (timeCounter > bot.timeAttack)
            bot.SwitchState(bot.MoveState);
    }

    public override void OnExit(BotStateMachine bot)
    {
        bot.botAnimator.SetBool(Constant.ANIM_IS_ATTACK, false);
    }
}
