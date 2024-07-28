using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBoundary : MonoBehaviour
{
    public Transform charBoundTransform;
    public Character character;
    private float charBaseRange;

    private List<GameObject> targetCharacters;
    public bool isPlayer;

    private void OnEnable()
    {
        OnInit();
    }

    public void OnInit()
    {
        targetCharacters = new List<GameObject>();
        charBaseRange = 6.5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHAR_MODEL))
        {
            ITarget newITarget = Cache.Ins.GetITargetFromGameObj(other.gameObject);
            if (newITarget != null && newITarget.CanBeTargeted())
            {
                if (!targetCharacters.Contains(other.gameObject))
                {
                    targetCharacters.Add(other.gameObject);
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
            if (newITarget != null)
            {
                if (isPlayer)
                {
                    targetCharacters.Remove(other.gameObject);
                    newITarget.DisableLockTarget();
                }
            }
        }
    }

    public GameObject GetTargetCharacter()
    {
        if (targetCharacters.Count == 0) return null;
        float shortestDistance = 100f;
        float newDistance;
        GameObject targetCharacter = null;

        for (int i = targetCharacters.Count - 1; i >= 0; i--)
        {
            GameObject currentChar = targetCharacters[i];

            if (!IsCharValid(currentChar))
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
