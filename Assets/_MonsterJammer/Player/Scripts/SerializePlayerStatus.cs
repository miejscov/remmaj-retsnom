using System.IO;
using UnityEngine;

public class SerializePlayerStatus : MonoBehaviour
{
    public PlayerStatusScript _playerStatus;
    public LevelControlScript _levelControl;

    public int _numberOfLives;
    public int _energy;
    public int _score;
    public int _currentLevel;

    private void Start()
    {
        //_playerStatus = GetComponent<PlayerStatusScript>();
        LoadObject();
    }

    public void SavePlayerStatus()
    {
        _playerStatus = GetComponent<PlayerStatusScript>();

        var outputPath = Application.persistentDataPath + @"/PlayerStatus.json";

        _energy = _playerStatus.GetAmountOfEnergy();
        _numberOfLives = _playerStatus.GetNumberOfLives();
        _score = _playerStatus.GetPlayerScore();

        _levelControl = GameObject.Find("LevelControl").GetComponent<LevelControlScript>();
        _currentLevel = _levelControl.GetCurrentLevel();

        var writter = new StreamWriter(outputPath);
        writter.WriteLine(JsonUtility.ToJson(this));
        writter.Close();
    }

    public void LoadObject()
    {
        _playerStatus = GetComponent<PlayerStatusScript>();

        var inputPath = Application.persistentDataPath + @"/PlayerStatus.json";
        var reader = new StreamReader(inputPath);
        var stringJson = reader.ReadToEnd();
        JsonUtility.FromJsonOverwrite(stringJson, this);
        reader.Close();

        _playerStatus.SetNumberOfLives(_numberOfLives);
        _playerStatus.SetPlayerEnergy(_energy);
        _playerStatus.SetPlayerScore(_score);


        _levelControl = GameObject.Find("LevelControl").GetComponent<LevelControlScript>();
        _levelControl.SetCurrentLevel(_currentLevel);
    }
}
