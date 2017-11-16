using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameOverScript : MonoBehaviour
{

    public Transform canvas;

    public void GameOver()
    {
        canvas.gameObject.SetActive(true);
    }

    public void Restart()
    {
        GameObject.Find("Player1(Clone)").GetComponent<PlayerStatusScript>().ResetPlayerStatus();
        GetComponent<ButtonContolScript>().Load("Scene001");
    }
}
