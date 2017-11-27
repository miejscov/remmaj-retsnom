using UnityEngine;

public class SettingsControlScript : MonoBehaviour
{
	private int _currentSetting;
	public GameObject[] Setting1;
	public GameObject[] Setting2;
	public GameObject[] Setting3;
	public GameObject[] Setting4;

	public int SetCurrentSetting
	{
		set {_currentSetting = value;}
	}

	//test
	public GameObject[] GetSetting()
	{
		GameObject[] setting;
		if (_currentSetting == 1)
			setting = Setting1;
		if (_currentSetting == 2)
			setting = Setting2;
		else setting = Setting1;
		return setting;
	}
}
