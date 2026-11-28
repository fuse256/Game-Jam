using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PowerUpCard : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text description;
    [SerializeField] private Button button;
    [SerializeField] private Image background;
    [SerializeField] private Sprite commonBackground;
    [SerializeField] private Sprite rareBackground;
    [SerializeField] private Sprite epicBackground;
    [SerializeField] private Sprite legendaryBackground;

    private PowerUp powerUp;
    private Action<PowerUp> onSelected;

    public void SetUp(PowerUp item, Action<PowerUp> selected)
    {
        powerUp = item;
        onSelected = selected;

        title.text = item.displayName;
        background.sprite = item.rarity switch
        {
            Rarity.Common => commonBackground,
            Rarity.Rare => rareBackground,
            Rarity.Epic => epicBackground,
            Rarity.Legendary => legendaryBackground,
            _ => commonBackground
        };
        icon.sprite = item.icon;
        description.text = item.description;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onSelected?.Invoke(powerUp));
    }
}
