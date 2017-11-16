using System.IO;
using UnityEngine;

public class SerializePlayerStatus : MonoBehaviour
{
	private PlayerStatusScript _playerStatus;
	
	public int NumberOfLives;
	public int Energy;
	public int Diamonds;
	public int Score;

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
	    
	    Energy = _playerStatus.GetAmountOfEnergy();
	    Diamonds = _playerStatus.GetDiamondsAmount();
	    NumberOfLives = _playerStatus.GetNumberOfLives();
	    Score = _playerStatus.GetPlayerScore();
        
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
		   
		_playerStatus.SetPlayerEnergy(Energy);
		_playerStatus.SetDiamondAmount(Diamonds);
		_playerStatus.SetNumberOfLives(NumberOfLives);
		_playerStatus.SetPlayerScore(Score);
    }
}
