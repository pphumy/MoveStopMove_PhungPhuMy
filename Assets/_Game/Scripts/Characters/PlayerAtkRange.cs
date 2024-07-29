using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtkRange : MonoBehaviour
{
    public Transform charBoundTransform;
    public Character character;
    private float charBaseRange;

    private List<BotStateMachine> targetCharacters;
    public bool isPlayer;

    private void OnEnable()
    {
        OnInit();
    }

    public void OnInit()
    {
        targetCharacters = new List<BotStateMachine>();
        charBaseRange = 6.5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHAR_MODEL))
        {
            ITarget newITarget = Cache.Ins.GetITargetFromGameObj(other.gameObject);
            BotStateMachine bot = other.gameObject.GetComponent<BotStateMachine>();
            if (newITarget != null && newITarget.CanBeTargeted())
            {
                if (!targetCharacters.Contains(bot))
                {
                    targetCharacters.Add(bot);
                }
                if (isPlayer)
                {
                    newITarget.EnableLockTarget();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHAR_MODEL))
        {
            ITarget newITarget = Cache.Ins.GetITargetFromGameObj(other.gameObject);
            BotStateMachine bot = other.gameObject.GetComponent<BotStateMachine>();
            if (newITarget != null)
            {
                if (isPlayer)
                {
                    targetCharacters.Remove(bot);
                    newITarget.DisableLockTarget();
                }
            }
        }
    }

    public BotStateMachine GetTargetCharacter()
    {
        if (targetCharacters.Count == 0) return null;
        float shortestDistance = 100f;
        float newDistance;
        BotStateMachine targetCharacter = null;

        for (int i = targetCharacters.Count - 1; i >= 0; i--)
        {
            BotStateMachine currentChar = targetCharacters[i];
            currentChar.DisableLockTarget();
            if (!IsCharValid(currentChar.gameObject))
            {
                targetCharacters.Remove(currentChar);
                if (targetCharacters.Count == 0) return null;
            }
            else
            {
                newDistance = Vector3.Distance(charBoundTransform.position, targetCharacters[i].transform.position);
                if (newDistance < shortestDistance)
                {
                    shortestDistance = newDistance;
                    targetCharacter = currentChar;
                    targetCharacter.EnableLockTarget();

                }
            }
        }
        return targetCharacter;
    }

    private bool IsCharValid(GameObject currentChar)
    {
        ITarget newITarget = currentChar.gameObject.GetComponent<ITarget>();

        //  Out of range
        if (Vector3.Distance(charBoundTransform.position, currentChar.transform.position) >= (charBaseRange * character.GetScale() * character.GetRange()))
        {
            return false;
        }

        // Deactive Object
        if (currentChar.activeInHierarchy == false)
        {
            return false;
        }

        // Can not be targeted
        if (newITarget == null || !newITarget.CanBeTargeted())
        {
            return false;
        }

        return true;
    }
}
