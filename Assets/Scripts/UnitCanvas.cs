using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UnitCanvas : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI barText;

    [SerializeField]
    private Image barImage;

    private Unit Unit;

    public void Init(Unit unit)
    {
        Unit = unit;
        RedrawHealthBar();
    }

    public void RedrawHealthBar()
    {
        barText.text = Unit.Health + " / " + Unit.MaxHealth;
        barImage.fillAmount = (float)Unit.Health / Unit.MaxHealth;
    }
}
