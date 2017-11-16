using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathCanvasScript : MonoBehaviour
{
    public Canvas canvas;
    private Text[] _texts;

    private PlayerStatusScript _playerStatus;
    private GameObject _player;

    private bool _isCanvasShowed = false;
    private GameObject _buttonControl;
    private float _timer = 0f;

    private void Awake()
    {
        _buttonControl = GameObject.Find("ButtonCtrl");
        _texts = canvas.gameObject.GetComponentsInChildren<Text>();
    }

    public void ShowCanvas()
    {
        _player = GameObject.Find("Player1(Clone)");
        _playerStatus = _player.GetComponent<PlayerStatusScript>();
        if (_playerStatus != null)
        {
            _texts[1].text = "Diamonds: " + _playerStatus.GetDiamondsAmount();
            _texts[2].text = "Total score: " + _playerStatus.GetPlayerScore();
            _texts[3].text = "Lives: " + _playerStatus.GetNumberOfLives();
        }

        canvas.gameObject.SetActive(true);
        _isCanvasShowed = true;
    }

    private void HideCanvas()
    {
        canvas.gameObject.SetActive(false);
        _isCanvasShowed = false;
    }

    private void Update()
    {
        if (_isCanvasShowed)
        {
            if (Input.anyKeyDown)
            {
                HideCanvas();
                if (_playerStatus.GetNumberOfLives() <= 0)
                {
                    _buttonControl.GetComponent<PlayerGameOverScript>().GameOver();
                }
                else
                    ButtonContolScript.LoadStatic("Scene001");
            }
        }
    }
}
