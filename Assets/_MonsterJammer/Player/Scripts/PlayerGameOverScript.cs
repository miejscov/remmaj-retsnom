using TMPro;
using UnityEngine;

public class PlayerGameOverScript : MonoBehaviour
{

    public Transform canvas;

    public void GameOver()
    {
        GameObject _player = GameObject.Find("Player1(Clone)");
        PlayerStatusScript _playerStatus = _player.GetComponent<PlayerStatusScript>();

        canvas.gameObject.SetActive(true);

        GameObject.FindGameObjectWithTag("Diamonds").GetComponent<TextMeshProUGUI>().text = "Diamonds: " + _playerStatus.GetDiamondsAmount();
        GameObject.FindGameObjectWithTag("Points").GetComponent<TextMeshProUGUI>().text = "Points: " + _playerStatus.GetPlayerScore();

    }

    public void Restart()
    {
        GameObject.Find("Player1(Clone)").GetComponent<PlayerStatusScript>().ResetPlayerStatus();
        GetComponent<ButtonContolScript>().Load("Scene001");
    }
}
