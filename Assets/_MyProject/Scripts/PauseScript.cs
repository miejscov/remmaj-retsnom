using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public Transform canvas;

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            Pause();
    }

    public void Pause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            canvas.gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
            canvas.gameObject.SetActive(true);
        }
    }



    public void RestartLevel()
    {
        GameObject player = GameObject.Find("Player1(Clone)");
        LevelControlScript level = GameObject.Find("LevelControl").GetComponent<LevelControlScript>();
        PlayerStatusScript status = player.GetComponent<PlayerStatusScript>();
        SerializePlayerStatus serialize = player.GetComponent<SerializePlayerStatus>();
        status.DeductPlayerLife();
        if (status.GetNumberOfLives() <= 0)
        {
            GetComponent<PlayerGameOverScript>().GameOver();
        }
        else
        {
            serialize.SavePlayerStatus();
            level.ResetLevel();
            Time.timeScale = 1f;
        }
        canvas.gameObject.SetActive(false);
    }
}