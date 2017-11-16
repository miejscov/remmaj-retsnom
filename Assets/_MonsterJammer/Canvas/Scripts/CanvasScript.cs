using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
	public Canvas Canvas;
	private Text [] _texts;
	private PlayerStatusScript _playerStatus;

	private void Awake()
	{
		_texts = Canvas.gameObject.GetComponentsInChildren <Text> ();
//		_playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatusScript>();
	}

	private void Update () {
		ShowGui();
        if (_playerStatus == null)

        {

            _playerStatus = GameObject.Find("Player1(Clone)").GetComponent<PlayerStatusScript>();

            Debug.Log("Canvas p[layer null");

        }
    }

	private void ShowGui()
	{
		if (_playerStatus == null) return;
		_texts[0].text = "Lives: " + _playerStatus.GetNumberOfLives();
		_texts[1].text = "Energy: " + _playerStatus.GetAmountOfEnergy();
		_texts[2].text = "Diamonds: " + _playerStatus.GetDiamondsAmount();
		_texts[3].text = "Total score: " + _playerStatus.GetPlayerScore();
		_texts[4].text = "Level: 1";
	}
}
