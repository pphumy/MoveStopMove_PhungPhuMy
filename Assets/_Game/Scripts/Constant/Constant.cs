using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constant
{
    // Joystick
    public const string HORIZONTAL_AXIS = "Horizontal";
    public const string VERTICAL_AXIS = "Vertical";

    // Anim Boolean
    public const string ANIM_IS_IDLE = "IsIdle";
    public const string ANIM_IS_DEAD = "IsDead";
    public const string ANIM_IS_ATTACK = "IsAttack";
    public const string ANIM_IS_WIN = "IsWin";
    public const string ANIM_IS_DANCE = "IsDance";

    // Gameplay
    public const string TAG_CHAR_MODEL = "CharModel";
    public const string TAG_CHARACTER = "Character";
    public const string TAG_BULLET = "Bullet";
    public const string OBSTACLE = "Obstacle";

    // Sound + Vibrate
    public const string SOUND_ON = "SoundOn";
    public const string VIBRATE_ON = "VibrateOn";

    // Game State
    public enum GameState { PAUSE, PLAY, CHOOSESKIN, END};

    // Gift Buff
    public enum GiftBuff { SPEED, RANGE, SCALE };

    // New Item Type
    public enum NewItemType { HAT, PANT, SHIELD };

    // Item Equip State
    public enum ItemState
    {
        Lock = 0,
        NotEquip = 1,
        NotEquipOneTime = 2,
        Equip = 3,
        EquipOneTime = 4,
    }

    // Item Unlock State
    public enum ItemUnlockOneTime
    {
        Used = 0,
        NotUsed = 1
    }

    // Save Path
    public const string PLAYER_DATA_PATH = "saveFile.json";
    public const string ITEM_STATE_PATH = "itemUnlockState.json";
}
