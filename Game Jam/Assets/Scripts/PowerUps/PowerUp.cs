using UnityEngine;


public enum Rarity
{
    Common,
    Rare,
    Epic, 
    Legendary
}









[CreateAssetMenu(fileName = "PowerUpScript", menuName = "Scriptable Objects/PowerUpScript")]
public class PowerUp : ScriptableObject
{
    public string displayName;
    [TextArea] public string description;
    public Rarity rarity;
    public Sprite icon;
}
