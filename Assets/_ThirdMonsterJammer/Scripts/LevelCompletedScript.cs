﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletedScript : MonoBehaviour
{
    public Canvas canvas;
    private Text[] _texts;

    public static int numberOfDiamonds, score;

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
        try
        {
            int numberOfLives = GameObject.Find("Player1(Clone)").GetComponent<PlayerStatusScript>().GetNumberOfLives();
            _texts[1].text = "Collected diamonds: " + numberOfDiamonds;
            _texts[2].text = "Collected score: " + score;
            _texts[3].text = "Lives: " + numberOfLives;

            canvas.gameObject.SetActive(true);
            _isCanvasShowed = true;
        }
        catch
        {
            Time.timeScale = 1;
        }
    }

    private void HideCanvas()
    {
        numberOfDiamonds = 0;
        score = 0;
        canvas.gameObject.SetActive(false);
        _isCanvasShowed = false;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (_isCanvasShowed)
        {
            if (Input.anyKeyDown)
            {
                HideCanvas();
            }
        }
    }

    public static void AddDiamond()
    {
        numberOfDiamonds += 1;
    }

    public static void AddPoints(int points)
    {
        score += points;
    }
}