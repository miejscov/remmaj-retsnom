using TMPro;
using UnityEngine;

public class PlayerGameOverScript : MonoBehaviour
{

    public Transform canvas;
    private GameObject _player;

    public void GameOver()
    {
        _player = GameObject.Find("Player1(Clone)");
        PlayerStatusScript _playerStatus = _player.GetComponent<PlayerStatusScript>();

        canvas.gameObject.SetActive(true);

        GameObject.FindGameObjectWithTag("Points").GetComponent<TextMeshProUGUI>().text = "Score: " + _playerStatus.GetPlayerScore();

    }

    public void Restart()
    {
        ButtonContolScript.isStarting = true;
        LevelControlScript _levelControl = GameObject.Find("LevelControl").GetComponent<LevelControlScript>();
        _player.GetComponent<PlayerStatusScript>().ResetPlayerStatus();
        _player.GetComponent<PlayerStatusScript>().SetPlayerAlive();
        _player.GetComponent<PlayerAnimationControlScript>().PlayerIdle();
        _levelControl.SetCurrentLevel(1);
        _levelControl.SetCurrentSetting(1);
        _levelControl.ResetLevel();
        canvas.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
