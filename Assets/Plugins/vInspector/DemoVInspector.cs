using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class DemoVInspector : MonoBehaviour
{
    // BUTTON
    [Button]
    public void HelloWorlds()
    {
        Debug.Log("Hello Worlds!");
    }
    [Button("Custom name")]
    public void RenamedButton() { }

    [Button(size = 50, color = "red")]
    public void BigRedButton() { }

    // TAB
    //  DICTIONARY
    [Tab("DICTIONARY")]
    public SerializedDictionary<string, Weapon> WeaponDictionary = new SerializedDictionary<string, Weapon>()
    {
        {"Gun", new Weapon {Damage = 5, Range = 100, FireRate = 1}},
        {"Rifle", new Weapon {Damage = 10, Range = 1000, FireRate = 10}},
        {"Banana", new Weapon {Damage = 0, Range = 0, FireRate = 0}}
    };
    public SerializedDictionary<string, Color> ColorDictionary = new SerializedDictionary<string, Color>()
    {
        {"Red", Color.red},
        {"Orange", new Color(1f, 0.647f, 0f)},
        {"Yellow", Color.yellow},
        {"Green", Color.green},
        {"Mint", new Color(0.588f, 0.996f, 0.871f)},
        {"Cyan", Color.cyan},
    };

    // SHOW/HIDE IF
    [Tab("SHOW & HIDE ")]
    public bool isBossAlive;
    [ShowIf("isBossAlive")]
    public int bossHealth = 500;
    [HideIf("isBossAlive")]
    public int bossDie = 10;

    // ENABLE/DISABLE IF
    [Tab("ENABLE & DISABLE")]
    public bool isPlayerEnabled;
    [EnableIf("isPlayerEnabled")]
    public int playerHealth = 100;
    [DisableIf("isPlayerEnabled")]
    public int playerDie = 0;

    // FOLDOUT
    [Tab("FOLDOUT")]
    [Foldout("Movement")]
    public float moveSpeed = 5.5f;
    public float runSpeed = 10.9f;
    public float jumpHeight = 2.8f;
    [Foldout("Stats")]
    public int healthStats = 100;
    public int staminaStats = 50;
    public int lives = 3;
    public bool isAlive = true;

    // SLIDER
    [Tab("SLIDER")]
    [MinMaxSlider(0, 2)]
    public Vector2 heightRange;

    [MinMaxSlider(0, 2)]
    public Vector2 widthRange;

    // VARIANTS
    [Tab("VARIANTS")]
    [Variants("Red", "Green", "Blue")]
    public string colorVariant;
    [Variants("Small", "Medium", "Large")]
    public string sizeVariant;

    // ONVALUECHANGED
    [Tab("ONVALUECHANGED")]
    public int level;
    public float health;
    public float mana;
    public float stamina;

    [OnValueChanged("level")]
    private void OnLevelChanged()
    {
        health = level * 10;
        mana = level * 100;
        stamina = level * mana * 10;
    }

    // READONLY
    [Tab("READONLY")]
    [ReadOnly]
    public string readOnlyString = "You can't edit me!";

    // SHOW
    [Tab("SHOW")]
    [ShowInInspector]
    private float privateFloat;
    [ShowInInspector]
    static float staticFloat;
    [ShowInInspector]
    private int propertyFloatG { get; }
    [ShowInInspector]
    private int propertyFloatGS { get; set; }
}

[System.Serializable]
public class Weapon
{
    public int Damage;
    public int Range;
    public int FireRate;
}

[System.Serializable]
public class ColorData
{
    public Color ColorValue;
}
