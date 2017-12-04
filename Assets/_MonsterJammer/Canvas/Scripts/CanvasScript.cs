using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
	public Canvas Canvas;
	private Text [] _texts;
	private PlayerStatusScript _playerStatus;

	private int _targetDiamondAmountInLevel;
	
	private void Awake()
	{
		_texts = Canvas.gameObject.GetComponentsInChildren <Text> ();
	}
	
	public int TargetDiamondAmountInLevel
	{
		set { _targetDiamondAmountInLevel = value; }
	}


	public void SetLives(int lives)
	{
		_texts[0].text = lives.ToString();
		Canvas.ForceUpdateCanvases();
	}

	public void SetEnergy(int energy)
	{
		_texts[1].text = energy.ToString();
		Canvas.ForceUpdateCanvases();
	}

	public void SetColectedDiamond(int value)
	{
		_texts[2].text = value + " / " + _targetDiamondAmountInLevel;
		Canvas.ForceUpdateCanvases();
	}
//	public void SetDiamondsLeft(int diamonds)
//	{
//		if (diamonds <= 0)
//			diamonds = 0;
//		
//		_texts[2].text = "Diamonds left: " + diamonds;
//		Canvas.ForceUpdateCanvases();
//	}

	public void SetTotalScore(int score)
	{
		_texts[4].text = score.ToString();
		Canvas.ForceUpdateCanvases();
	}

	public void SetLevel(int level)
	{
		_texts[3].text = "Level: " + level;
		Canvas.ForceUpdateCanvases();
	}

	public void SetCanvasVisibility(bool value)
	{
		Canvas.enabled = value;
	}
}
