using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeathCanvasScript : MonoBehaviour
{
    public Canvas canvas;
    private TextMesh[] _texts;

    private PlayerStatusScript _playerStatus;
    private GameObject _player;

    private bool _isCanvasShowed = false;
    private GameObject _buttonControl;
    private float _timer = 0f;
    private bool keyDown;


    private void Awake()
    {
        _buttonControl = GameObject.Find("ButtonCtrl");
    }

    public void ShowCanvas()
    {
        keyDown = false;
        _player = GameObject.Find("Player1(Clone)");
        _playerStatus = _player.GetComponent<PlayerStatusScript>();
       
                canvas.gameObject.SetActive(true);

        if (_playerStatus != null)
        {
            GameObject.FindGameObjectWithTag("Diamonds").GetComponent<TextMeshProUGUI>().text = "Diamonds: " + _playerStatus.GetDiamondsAmount();
            GameObject.FindGameObjectWithTag("Points").GetComponent<TextMeshProUGUI>().text = "Score: " + _playerStatus.GetPlayerScore();
            GameObject.FindGameObjectWithTag("Lives").GetComponent<TextMeshProUGUI>().text = "Lives: " + _playerStatus.GetNumberOfLives();
        }
        _isCanvasShowed = true;
    }

    private void HideCanvas()
    {
        canvas.gameObject.SetActive(false);
        _isCanvasShowed = false;
    }

    private void Update()
    {
        if (!_isCanvasShowed) return;
        if (keyDown) return;
        if (Input.anyKeyDown)
        {
            keyDown = true;
            Invoke("KeyDown", 1f);
        }
    }

    private void KeyDown()
    {
        HideCanvas();
        if (_playerStatus.GetNumberOfLives() <= 0)
        {
            _buttonControl.GetComponent<PlayerGameOverScript>().GameOver();
        }
        else
        {
            _playerStatus.SetPlayerAlive();
            GameObject.Find("LevelControl").GetComponent<LevelControlScript>().ResetLevel();
            HideCanvas();
        }
//                    ButtonContolScript.LoadStatic("Scene001");
    }
    
    
}
