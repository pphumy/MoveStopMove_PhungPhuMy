using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotStateMachine : Character, ITarget, IHit
{
    public BotStateMachine bot;

    // StateMachine
    BotBaseState currentState;
    public BotIdleState IdleState = new BotIdleState();
    public BotMoveState MoveState = new BotMoveState();
    public BotAttackState AttackState = new BotAttackState();
    public BotDeathState DeathState = new BotDeathState();
    public BotWaitState WaitState = new BotWaitState();
    public BotAfterDeathState AfterDeathState = new BotAfterDeathState();
    private bool switchingState;

    // Bot Variables
    [Header("Bot Setting")]
    public Transform botTransform;
    public Transform botModel;
    public GameObject underUI;
    public Collider botCollider;

    // NavMeshAgent
    [Header("Nav Mesh Agent")]
    public NavMeshAgent agent;
    [Range(0, 100)] public float speedAgent;
    [Range(1, 500)] public float walkRadius;
    public float rotateSpeed;

    internal Vector3 directionVec;
    internal float angleToRotate;

    // Animator
    [Header("Animator")]
    public Animator botAnimator;

    // Weapon
    [Header("Weapon")]
    private Weapon weapon;

    // Time
    [Header("Time Setting")]
    public float timeIdle;
    public float timeAttack;
    public float timeDeath;
    public float timeMove;

    [Header("Weapon")]
    private WeaponID weaponID;
    private WeaponSkinID weaponSkinID;
    public Transform weaponHolder;

    [Header("Skin")]
    public BotSkin botSkin;

    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        botCollider.enabled = true;
        currentState = WaitState;
        currentState.EnterState(bot);
        switchingState = false;
        scale = 1;

        InitWeapon();
        botSkin.OnInit();
    }

    private void InitWeapon()
    {
        if (weapon != null)
        {
            Destroy(weapon.gameObject);
        }
        weaponID = (WeaponID)Random.Range(0, 3);
        weaponSkinID = (WeaponSkinID)Random.Range(0, 7);
        weapon = ItemController.Ins.SetWeapon(weaponID, weaponSkinID, weaponHolder);
    }

    private void Update()
    {
        if (!switchingState)
        {
            currentState.UpdateState(bot);
        }
    }

    public void SwitchState(BotBaseState state)
    {
        if (currentState != state)
        {
            switchingState = true;
            currentState.ExitState(bot);
            currentState = state;
            state.EnterState(bot);
            switchingState = false;
        }
    }

    public override void Death(Character killer)
    {
        botCollider.enabled = false;
        SwitchState(DeathState);
    }

    public bool CanBeTargeted()
    {
        if (currentState != DeathState || gameObject.activeSelf== false)
            return true;
        else
            return false;
    }

    public void DisableLockTarget()
    {
        underUI.gameObject.SetActive(false);
    }

    public void EnableLockTarget()
    {
        underUI.gameObject.SetActive(true);
    }

    public override void Attack()
    {
        StartCoroutine(ThrowWeapon());
    }

    IEnumerator ThrowWeapon()
    {
        yield return new WaitForSeconds(0.2f);
        weapon.Attack(bot.botModel, bot);
        weapon.gameObject.SetActive(false);
    }
}
