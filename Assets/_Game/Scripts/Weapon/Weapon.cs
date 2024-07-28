using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject weapon;
    public Bullet bulletPrefab;

    public MeshRenderer meshRend;
    protected WeaponSkinID skinID;

    public virtual void InitSkin(WeaponSkinID _skinId) {  }

    public void Attack(Transform charTransform, Character character)
    {
        Vector3 newAngles = bulletPrefab.transform.eulerAngles + Vector3.up * charTransform.eulerAngles.y;
        Vector3 newPosition = charTransform.position + Vector3.up * 1f + charTransform.forward * 1f;
        Bullet newBullet = SimplePool.Spawn(bulletPrefab.gameObject, newPosition, Quaternion.Euler(newAngles)).GetComponent<Bullet>();

        // Set Origin Values
        newBullet.SetOriginWeapon(weapon);
        newBullet.SetOriginCharacter(character);
        newBullet.SetOriginCharBound(character.charBound);
        newBullet.SetDirectionVector(charTransform.forward);
        newBullet.InitSkin(skinID);
        newBullet.transform.localScale = bulletPrefab.transform.localScale * character.GetScale();
    }
}
