using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotIdleState : BotBaseState
{
    private float timeCounter;

    public override void EnterState(BotStateMachine bot)
    {
        timeCounter = 0;
        bot.agent.speed = 0;
        bot.botAnimator.SetBool(Constant.ANIM_IS_IDLE, true);
    }

    public override void UpdateState(BotStateMachine bot)
    {
        timeCounter += Time.deltaTime;
        if (timeCounter > bot.timeIdle)
        {
            timeCounter = 0;
            bot.SwitchState(bot.MoveState);
        }
        else
        {
            if (FindTargetCharacter(bot))
            {
                bot.SwitchState(bot.AttackState);
            }
        }
    }

    public override void ExitState(BotStateMachine bot)
    {
        bot.botAnimator.SetBool(Constant.ANIM_IS_IDLE, false);
    }

    private bool FindTargetCharacter(BotStateMachine bot)
    {
        GameObject targetCharacter = bot.charBound.GetTargetCharacter();
        if (targetCharacter != null)
        {
            Vector3 directToTarget = targetCharacter.transform.position - bot.botModel.position;
            RotateToTargetCharacter(directToTarget, bot.botModel);

            timeCounter = 0;
            return true;
        }
        return false;
    }

    private void RotateToTargetCharacter(Vector3 directToTarget, Transform botModel)
    {
        float xPos = directToTarget.x;
        float zPos = directToTarget.z;

        float angleToRotate = Mathf.Rad2Deg * Mathf.Atan2(xPos, zPos);
        botModel.rotation = Quaternion.AngleAxis(angleToRotate, Vector3.up);
    }


}
