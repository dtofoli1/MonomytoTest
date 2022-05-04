using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI points;
    public TextMeshProUGUI equippedWeapon;
    public TextMeshProUGUI maxAmmo;
    public TextMeshProUGUI currentAmmo;
    public TextMeshProUGUI info;
    public TextMeshProUGUI menuPointsText;
    public GameObject menuPoints;
    public GameObject menuPanel;
    public Button startButton;

    public void UpdateText(TextMeshProUGUI textObj, string text)
    {
        textObj.text = text;
    }

    public void ShowMenu()
    {
        menuPanel.SetActive(true);
    }

    public void HideMenu()
    {
        menuPanel.SetActive(false);
    }
}
