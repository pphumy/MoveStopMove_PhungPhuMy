using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour, IHit
{
    // Character variables
    public Character character;
    public CharacterBoundary charBound;
    public SkinnedMeshRenderer bodyRend;

    // Basic variables
    protected float scale = 1;
    protected int score = 0;
    protected float range = 1;
    protected int scoreToScale = 2;

    protected new string name;

    private void Awake()
    {
        scale = 1;
        score = 0;
        range = 1;
        scoreToScale = 2;
    }

    private void OnEnable()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        scale = 1;
        charBound.transform.localScale = Vector3.one;
    }


    public virtual void IncreaseRange(float increaseValue) { }

    public virtual void IncreaseScale(float scaleRatio)
    {
        scale *= scaleRatio;
        if (scale >= 2)
        {
            scale = 2;
        }
        charBound.transform.localScale = Vector3.one * scale;
    }

    public virtual void IncreaseScore(int increaseValue)
    {
        score += increaseValue;

        scoreToScale -= increaseValue;

        if (scoreToScale <= 0)
        {
            IncreaseScale(1.1f);
            scoreToScale = 2;
        }
    }

    public virtual void MultipleScore(int multipleTime) {    }

    #region Get Variables
    public float GetScale()
    {
        return scale;
    }

    public float GetRange()
    {
        return range;
    }

    public int GetScore()
    {
        return score;
    }

    public string GetName()
    {
        return name;
    }

    public Material GetBodyMat()
    {
        return bodyRend.materials[0];
    }
    #endregion

    #region Set Variables
    public void SetScore(int newScore)
    {
        score = newScore;
        scoreToScale = 3;
    }

    public void SetName(string newName)
    {
        name = newName;
    }
    #endregion

    #region Gameplay
    public virtual void Move() { }

    public virtual void Attack() { }

    public virtual void Death(Character killer) { }

    public virtual void GetHit(Character killer)
    {
        Death(killer);
        SoundManager.Ins.PlayDieSound();
    }
    #endregion
}
