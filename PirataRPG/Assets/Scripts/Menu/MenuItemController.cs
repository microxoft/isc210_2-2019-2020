using Assets.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuItemController : MonoBehaviour
{
    const float _HOVERSCALEFACTOR = 1.4f;
    MenuController _menuController;

    private void Awake()
    {
        _menuController = GameObject.Find("GlobalScriptsText").GetComponent<MenuController>();
    }

    public void OnMouseEnter()
    {
        transform.localScale *= _HOVERSCALEFACTOR;
    }

    public void OnMouseExit()
    {
        transform.localScale /= _HOVERSCALEFACTOR;
    }

    public void OnMouseUp()
    {
        if (_menuController.IsCanvasActive())
            return;

        switch(gameObject.name)
        {
            case "PlayMenuItem":
                SceneManager.LoadScene("ExplorationLevel");
                break;
            case "OptionsMenuItem":
                _menuController.UpdateOptionsGUI();
                _menuController.ShowGameOptions();
                break;
            case "ExitMenuItem":
                Application.Quit();
                break;
        }
    }

    public void OkDialog()
    {
        _menuController.HideGameOptions();
    }

    public void CancelDialog()
    {
        Game.CurrentGame.LoadCurrentState();
        _menuController.HideGameOptions();
    }

    public void OnPlayerNameChanged(InputField input)
    {
        Game.CurrentGame.PlayerName = input.text;
    }

    public void OnMusicVolumeChanged(Slider slider)
    {
        Game.CurrentGame.MusicVolume = slider.value;
    }

    public void OnEffectsVolumeChanged(Slider slider)
    {
        Game.CurrentGame.EffectsVolume = slider.value;
    }

    public void OnDifficultyValueChanged(Dropdown dropdown)
    {
        Game.CurrentGame.Difficulty = (Game.eDifficulty)dropdown.value;
    }
}
