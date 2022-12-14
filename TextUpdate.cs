using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdate : MonoBehaviour
{
    public enum UpdateSourse
    {
        Gold,
        Health,
        Mana
    }

    public UpdateSourse sourse = UpdateSourse.Gold;

    [SerializeField] private Text text;

    private void Start()
    {
        if (text == null)
        {
            text = GetComponentInChildren<Text>();
        }

        switch (sourse)
        {
            case UpdateSourse.Gold: 
                TDPlayer.OnGoldUpdate += UpdateText;
                break;
            case UpdateSourse.Health:
                TDPlayer.OnHealthUpdate += UpdateText;
                break;
            case UpdateSourse.Mana:
                TDPlayer.OnManaUpdate += UpdateText;
                break;
        }
        
    }

    private void UpdateText(int sourseValue)
    {
        text.text = sourseValue.ToString();
    }

    private void OnDestroy()
    {
        switch (sourse)
        {
            case UpdateSourse.Gold:
                TDPlayer.OnGoldUpdate -= UpdateText;
                break;
            case UpdateSourse.Health:
                TDPlayer.OnHealthUpdate -= UpdateText;
                break;
            case UpdateSourse.Mana:
                TDPlayer.OnManaUpdate -= UpdateText;
                break;
        }
    }

}
