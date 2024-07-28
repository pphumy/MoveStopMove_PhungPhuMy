using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Basic attributes
    public Transform bulletTransform;
    public Transform bulletRenderTransform;
    public MeshRenderer meshRend;

    // Origins
    internal GameObject originWeapon;
    internal CharacterBoundary originCharBound;
    internal Character originCharacter;

    // Movement
    internal Vector3 directionVector = Vector3.zero;
    private Vector3 originPos;

    // Basic variables
    public float range;
    public float speed;

    void Update()
    {
        if (directionVector != Vector3.zero)
        {
            // Bullet is in valid range
            if (Vector3.Distance(bulletTransform.position, originPos) <= range * originCharacter.GetScale() * originCharacter.GetRange())
            {
                bulletTransform.position += directionVector * speed * Time.deltaTime * originCharacter.GetScale();
                SpecialMove();
            }
            // Bullet is outside valid range
            else
            {
                originWeapon.SetActive(true);
                SimplePool.Despawn(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHAR_MODEL))
        {
            if (other.gameObject.GetInstanceID() != originCharacter.gameObject.GetInstanceID())
            {
                IHit newIHit = Cache.Ins.GetIHitFromGameObj(other.gameObject);
                if (newIHit != null)
                {
                    OnExecuteHit(newIHit);
                }
            }
        }

        else if (other.gameObject.CompareTag(Constant.OBSTACLE))
        {
            OnExecuteHit(null);
        }
    }

    // Execute a hit
    private void OnExecuteHit(IHit newIHit)
    {
        if (newIHit != null)
        {
            originWeapon.SetActive(true);
            originCharacter.IncreaseScore(Random.Range(1, 3));
            newIHit.GetHit(originCharacter);
            SimplePool.Despawn(bulletTransform.gameObject);
        }
    }

    // Specific movement for each type of bullet
    protected virtual void SpecialMove() { }

    // Initialize skin for each bullet
    public virtual void InitSkin(WeaponSkinID SkinID) { }

    #region SetInitValues
    public void SetOriginWeapon(GameObject weapon)
    {
        originWeapon = weapon;
    }

    public void SetOriginCharacter(Character character)
    {
        originCharacter = character;
        originPos = originCharacter.transform.position;
    }

    public void SetOriginCharBound(CharacterBoundary charBound)
    {
        originCharBound = charBound;
    }

    public void SetDirectionVector(Vector3 vector)
    {
        directionVector = vector;
    }
    #endregion

}
