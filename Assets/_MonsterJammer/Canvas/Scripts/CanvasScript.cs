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
	}

	

	
	public void SetLives(int lives)
	{
		_texts[0].text = "Lives: " + lives;
		Canvas.ForceUpdateCanvases();
	}

	public void SetEnergy(int energy)
	{
		_texts[1].text = "Strength: " + energy;
		Canvas.ForceUpdateCanvases();
	}

	public void SetDiamonds(int diamonds)
	{
		_texts[3].text = "Diamonds: " + diamonds;
		Debug.Log("setdiamond canv");
		Canvas.ForceUpdateCanvases();
	}
	
	public void SetDiamondsLeft(int diamonds)
	{
		if (diamonds <= 0)
			diamonds = 0;
		
		_texts[2].text = "Diamonds left: " + diamonds;
		Canvas.ForceUpdateCanvases();
	}

	public void SetTotalScore(int score)
	{
		_texts[5].text = "Total score: " + score;
		Canvas.ForceUpdateCanvases();
	}

	public void SetLevel(int level)
	{
		_texts[4].text = "Level: " + level;
		Canvas.ForceUpdateCanvases();
	}
}
