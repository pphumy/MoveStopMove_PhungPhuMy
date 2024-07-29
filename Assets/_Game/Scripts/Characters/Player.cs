using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : Character, ITarget
{
    public Player player;
    public static Player _player;
    public PlayerAtkRange atkRange;
    public Transform playerTransform;
    public Transform playerModel;
    public Rigidbody playerRb;
    public Collider playerCollider;
    public SphereCollider boundaryCollider;

    public UnityAction onHit;
    public UnityAction OnDeadRemove;

    public Canvas joystickCanvas;
    public JoytickController joystick;
    private float inputX;
    private float inputZ;

    private Vector3 movement;
    public float moveSpeed;
    public float rotateSpeed;
    private float angleToRotate;

    public Animator playerAnimator;

    private Weapon weapon;
    private WeaponID weaponID;
    public Transform weaponHolder;
    private WeaponSkinID weaponSkinID;

    private bool canAttack;
    private bool isDead;
    private bool isWin;
    private bool canRevive;

    public GameObject underUI;
    public Text getScoreText;


    public PlayerSkin playerSkin;
    public Indicator playerIndicator;
    private Character killer;
    public ParticleSystem increaseScaleEffect;

    private void Awake()
    {
        _player = this;
    }

    private void OnEnable()
    {
        OnInit();
    }

    public override void OnInit()
    {
        // Init joystick movement
        inputX = 0;
        inputZ = 0;
        movement = Vector3.zero;

        // Init Boolean
        canAttack = true;
        isDead = false;
        isWin = false;
        canRevive = true;

        // Init Skin
        InitWeapon();
        InitName();
        playerSkin.OnInit();

        // Init Animation
        playerAnimator.SetBool(Constant.ANIM_IS_WIN, false);
        playerAnimator.SetBool(Constant.ANIM_IS_DEAD, false);
        playerAnimator.SetBool(Constant.ANIM_IS_IDLE, true);

        // Init basic attribute
        playerCollider.enabled = true;
        movement = Vector3.zero;
        player.transform.rotation = Quaternion.Euler(0, 60, 0);
        charBound.transform.position = Vector3.zero;
        //atkRange.transform.position = Vector3.zero;

        scale = 1;
        score = 0;
        range = 1;
        scoreToScale = 2;
        getScoreText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (LevelManager.Ins.GetGameState() == Constant.GameState.PLAY)
        {
            if (!isDead && !isWin)
            {
                ActivateUI(true);
                if (LevelManager.Ins.GetRemainNumOfBots() == 0)
                {
                    Win();
                }
                else
                {
                    Move();
                }
            }
        }
        else
        {
            ActivateUI(false);
            if (LevelManager.Ins.GetGameState() == Constant.GameState.CHOOSESKIN)
            {
                ChooseSkinAnim();
            }
        }
    }

    private void FixedUpdate()
    {
        MoveCharacter(movement.normalized);
    }

    private void InitWeapon()
    {
        ChangeWeapon();
    }

    public void ChangeWeapon()
    {
        PlayerData data = PlayerDataController.Ins.LoadFromJson();
        weaponID = (WeaponID)data.weaponID;
        weaponSkinID = (WeaponSkinID)data.weaponSkinID;

        if (weapon != null)
            Destroy(weapon.gameObject);

        weapon = ItemController.Ins.SetWeapon(weaponID, weaponSkinID, weaponHolder);
    }

    public void InitName()
    {
        PlayerData data = PlayerDataController.Ins.LoadFromJson();
        SetName(data.name);
        playerIndicator.SetName();
    }

    private void ActivateUI(bool activate)
    {
        underUI.SetActive(activate);
        joystickCanvas.gameObject.SetActive(activate);
    }

    public override void Move()
    {
        inputX = joystick.inputHorizontal();
        inputZ = joystick.inputVertical();

        if (inputX == 0 && inputZ == 0)
        {
            Stop();
            Attack();
        }
        else
        {
            SetRotation();
            SetMovement();
        }
    }

    private void Stop()
    {
        movement = Vector3.zero;
        playerAnimator.SetBool(Constant.ANIM_IS_IDLE, true);
    }

    private void SetRotation()
    {
        angleToRotate = Mathf.Rad2Deg * Mathf.Atan2(inputX, inputZ);
        playerModel.rotation = Quaternion.RotateTowards(playerModel.rotation, Quaternion.AngleAxis(angleToRotate, Vector3.up), rotateSpeed * Time.deltaTime);
    }

    private void SetMovement()
    {
        StopAttack();
        movement = new Vector3(inputX, 0, inputZ);
        playerAnimator.SetBool(Constant.ANIM_IS_IDLE, false);
        canAttack = true;
    }

    private void MoveCharacter(Vector3 direction)
    {
        playerRb.velocity = direction * moveSpeed * Time.deltaTime * GetScale();
    }

    public override void Attack()
    {
        if (canAttack)
        {
            GameObject targetCharacter = charBound.GetTargetCharacter();
            if (targetCharacter != null)
            {
                GameObject target = targetCharacter.transform.parent.gameObject;
                target.transform.GetChild(2).gameObject.SetActive(true);
                canAttack = false;
                playerAnimator.SetBool(Constant.ANIM_IS_ATTACK, true);

                Vector3 directToTarget = targetCharacter.transform.position - playerTransform.position;
                RotateToTargetCharacter(directToTarget);

                StartCoroutine(ThrowWeapon());
                Invoke(nameof(StopAttack), 1f);
                SoundManager.Ins.PlayThrowSound();
            }
            

        }
    }

    IEnumerator ThrowWeapon()
    {
        yield return new WaitForSeconds(0.4f);
        if (!canAttack)
        {
            playerAnimator.SetBool(Constant.ANIM_IS_ATTACK, true);
            weapon.Attack(playerModel, player);
            weapon.gameObject.SetActive(false);
        }
    }

    private void StopAttack()
    {
        playerAnimator.SetBool(Constant.ANIM_IS_ATTACK, false);
    }

    private void RotateToTargetCharacter(Vector3 directToTarget)
    {
        float xPos = directToTarget.x;
        float zPos = directToTarget.z;

        angleToRotate = Mathf.Rad2Deg * Mathf.Atan2(xPos, zPos);
        playerModel.rotation = Quaternion.AngleAxis(angleToRotate, Vector3.up);
    }



    public override void Death(Character killerChar)
    {
        if (!isWin)
        {
            ActivateUI(false);

            isDead = true;
            movement = Vector3.zero;
            playerAnimator.SetBool(Constant.ANIM_IS_DEAD, true);
            playerCollider.enabled = false;
            killer = killerChar;

            if (!canRevive)
            {
                Lose();
            }
            else
            {
                UIManager.Ins.GetUI(UIID.UICGameplay).Close();
                UIManager.Ins.OpenUI(UIID.UICRevive);
            }
        }
    }

    public void Revive()
    {
        isDead = false;
        canAttack = false;
        canRevive = false;
        ActivateUI(true);
        playerCollider.enabled = true;
        playerAnimator.SetBool(Constant.ANIM_IS_DEAD, false);
        playerAnimator.SetBool(Constant.ANIM_IS_IDLE, true);
    }

    public void Lose()
    {
        LevelManager.Ins.Lose(killer);
        UIManager.Ins.GetUI(UIID.UICGameplay).Close();
        UIManager.Ins.OpenUI(UIID.UICFail);
        SoundManager.Ins.PlayLoseSound();

    }

    

    public bool CanBeTargeted()
    {
        return !isDead;
    }

    public void DisableLockTarget() { }

    public void EnableLockTarget() { }



    public void Win()
    {
        if (!isDead)
        {
            isWin = true;
            movement = Vector3.zero;
            playerAnimator.SetBool(Constant.ANIM_IS_WIN, true);
            ActivateUI(false);

            LevelManager.Ins.Win();
            UIManager.Ins.GetUI(UIID.UICGameplay).Close();
            UIManager.Ins.OpenUI(UIID.UICVictory);
            SoundManager.Ins.PlayVictorySound();

            // Reset Progress, Level, Rank
            PlayerData data = PlayerDataController.Ins.LoadFromJson();
            //data.progress = 0;
            data.level += 1;
            data.bestRank = LevelManager.Ins.GetLevelData(data.level).numOfBots + 1;
            PlayerDataController.Ins.SaveToJson(data);
        }
    }



    public PlayerSkin GetPlayerSkin()
    {
        return playerSkin;
    }

    public void ChooseSkinAnim()
    {
        playerAnimator.SetBool(Constant.ANIM_IS_DANCE, true);
    }

    public void ExitSkinAnim()
    {
        if (playerAnimator != null)
        {
            playerAnimator.SetBool(Constant.ANIM_IS_DANCE, false);
        }
    }


    
    public override void IncreaseRange(float increaseValue)
    {
        range *= increaseValue;
        boundaryCollider.radius *= increaseValue;
        underUI.transform.localScale *= increaseValue;

        if (increaseValue > 1)
        {
            SoundManager.Ins.PlaySizeUpSound();
        }
    }

    public void IncreaseRangeInTime(float time)
    {
        StartCoroutine(IEIncreaseRange(time));
    }

    public override void IncreaseScore(int increaseValue)
    {
        base.IncreaseScore(increaseValue);
        //StartCoroutine(ShowGetScore(increaseValue));
        CoinController.Ins.IncreaseCoins(increaseValue);
    }

    public override void IncreaseScale(float scaleRatio)
    {
        base.IncreaseScale(scaleRatio);
        if (scaleRatio > 1)
        {
            increaseScaleEffect.Play();
            SoundManager.Ins.PlaySizeUpSound();
        }
    }

    public void IncreaseScaleInTime(float time)
    {
        StartCoroutine(IEIncreaseScale(time));
    }

    public void IncreaseSpeed(float speedRatio)
    {
        moveSpeed *= speedRatio;
        if (speedRatio > 1)
        {
            SoundManager.Ins.PlaySizeUpSound();
        }
    }

    public void IncreaseSpeedInTime(float time)
    {
        StartCoroutine(IEIncreaseSpeed(time));
    }

    public override void MultipleScore(int multipleTime)
    {
        base.MultipleScore(multipleTime);
        CoinController.Ins.IncreaseCoins(score * (multipleTime - 1));
    }


    //IEnumerator ShowGetScore(int increaseScore)
    //{
    //    getScoreText.text = "+" + increaseScore.ToString();
    //    getScoreText.gameObject.SetActive(true);
    //    yield return new WaitForSeconds(1f);
    //    getScoreText.gameObject.SetActive(false);
    //}

    IEnumerator IEIncreaseRange(float time)
    {
        IncreaseRange(2f);
        yield return new WaitForSeconds(time);
        IncreaseRange(0.5f);
    }

    IEnumerator IEIncreaseSpeed(float time)
    {
        IncreaseSpeed(1.5f);
        yield return new WaitForSeconds(time);
        IncreaseSpeed(0.66f);
    }

    IEnumerator IEIncreaseScale(float time)
    {
        IncreaseScale(1.25f);
        yield return new WaitForSeconds(time);
        IncreaseScale(0.8f);
    }
}
