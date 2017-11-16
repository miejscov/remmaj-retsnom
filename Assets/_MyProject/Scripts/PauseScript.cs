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
}