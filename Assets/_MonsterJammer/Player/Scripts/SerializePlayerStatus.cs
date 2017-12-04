using System.IO;
using UnityEngine;

public class SerializePlayerStatus : MonoBehaviour
{
	private PlayerStatusScript _playerStatus;
	private LevelControlScript _levelControl;
	
	private int _numberOfLives;
	private int _energy;
	private int _score;
	private int _currentLevel;

	private void Start()
	{
	    _playerStatus = GetComponent<PlayerStatusScript>();
    	 LoadObject ();
    }
    
    public void SavePlayerStatus()
    {
        if (_playerStatus == null)
            _playerStatus = GetComponent<PlayerStatusScript>();

        var outputPath = Application.persistentDataPath + @"/PlayerStatus.json";
	    
	    _energy = _playerStatus.GetAmountOfEnergy();
	    _numberOfLives = _playerStatus.GetNumberOfLives();
	    _score = _playerStatus.GetPlayerScore();
	    
		_levelControl = GameObject.Find("LevelControl").GetComponent<LevelControlScript>();
	    _currentLevel = _levelControl.GetCurrentLevel();
	    
	    var writter = new StreamWriter (outputPath);
    	writter.WriteLine (JsonUtility.ToJson (this));
    	writter.Close ();
   	}

	private void LoadObject()
   	{
   		var inputPath = Application.persistentDataPath + @"/PlayerStatus.json";
   		var reader = new StreamReader (inputPath);
   		var stringJson = reader.ReadToEnd();
   		JsonUtility.FromJsonOverwrite (stringJson, this);
   		reader.Close();
		   
		_playerStatus.SetPlayerEnergy(_energy);
		_playerStatus.SetNumberOfLives(_numberOfLives);
		_playerStatus.SetPlayerScore(_score);
		   
		_levelControl = GameObject.Find("LevelControl").GetComponent<LevelControlScript>();
		_levelControl.SetCurrentLevel(_currentLevel);
    }
}
