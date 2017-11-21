using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
	public Canvas Canvas;
	private Text [] _texts;
	private PlayerStatusScript _playerStatus;
	private LevelControlScript _levelControl;

	private void Awake()
	{
		_levelControl = GameObject.Find("LevelControl").GetComponent<LevelControlScript>();
		_texts = Canvas.gameObject.GetComponentsInChildren <Text> ();
	}

	private void Update () {
		ShowGui();
        if (_playerStatus == null)

        {
            _playerStatus = GameObject.Find("Player1(Clone)").GetComponent<PlayerStatusScript>();
        }
    }

	private void ShowGui()
	{
		if (_playerStatus == null) return;
		var numberOfLives = _playerStatus.GetNumberOfLives();

		_texts[0].text = "Lives: " + numberOfLives;
		_texts[1].text = "Energy: " + _playerStatus.GetAmountOfEnergy();
		_texts[2].text = "Diamonds: " + _playerStatus.GetDiamondsAmount();
		_texts[3].text = "Total score: " + _playerStatus.GetPlayerScore();
		_texts[4].text = "Level: "+ _levelControl.GetCurrentLevel();
	}
}
