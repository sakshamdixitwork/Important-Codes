using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameDataSaverScript : MonoBehaviour
{
	public static GameDataSaverScript instance;

    private void Awake()
    {
		if(instance == null)
        {
			instance = this;
			DontDestroyOnLoad(this.gameObject);
        }
        else
        {
			Destroy(this.gameObject);
        }
    }

	[System.Serializable]
	public class GameDataSaverClass
	{
		public int CoinCount;
		public int Lives;
		public Level LevelData = new Level();
		public bool IsFirstBool;
		public bool PlaySound;
		public bool PlayMusic;

		public GameDataSaverClass(int _CoinCount, int _Lives, Level _LevelData, bool _IsFirstBool, bool _PlaySound, bool _PlayMusic)
		{
			CoinCount = _CoinCount;
			Lives = _Lives;
			LevelData = _LevelData;
			IsFirstBool = _IsFirstBool;
			PlaySound = _PlaySound;
			PlayMusic = _PlayMusic;
		}
	}

	[System.Serializable]
	public class Level
	{
		public List<LevelData> Levels = new List<LevelData>();
	}

	[System.Serializable]
	public class LevelData
	{
		public int LevelNumber;
		public bool isUnlocked;
		public int StarAwarded;
	}


	public void SaveGameData()
	{
		Level saveLevel = new Level();

		saveLevel.Levels.Clear();
		for (int i = 0; i < DataCtrl.instance.data.levelData.Length; i++)
		{
			LevelData levelData = new LevelData();
			levelData.LevelNumber = DataCtrl.instance.data.levelData[i].levelNumber;
			levelData.isUnlocked = DataCtrl.instance.data.levelData[i].isUnlocked;
			levelData.StarAwarded = DataCtrl.instance.data.levelData[i].starsAwarded;
			saveLevel.Levels.Add(levelData);
		}

		GameDataSaverClass gameDataSaverClass = new GameDataSaverClass(DataCtrl.instance.data.coinCount, DataCtrl.instance.data.lives, saveLevel, DataCtrl.instance.data.isFirstBoot, DataCtrl.instance.data.playSound, DataCtrl.instance.data.playMusic);

		SaveUpdateGameData(gameDataSaverClass);
	}

	public void SaveUpdateGameData(GameDataSaverClass gameData)
	{

		string json = JsonUtility.ToJson(gameData);

		Debug.Log("SavedPath: " + Application.persistentDataPath);
		File.WriteAllText(Application.persistentDataPath + "/Save.txt", json);
	}

	public void LoadGameData()
	{
		if (File.Exists(Application.persistentDataPath + "/Save.txt"))
		{
			string json = File.ReadAllText(Application.persistentDataPath + "/Save.txt");
			GameDataSaverClass gameDataSaverClass = JsonUtility.FromJson<GameDataSaverClass>(json);
			DataCtrl.instance.data.coinCount = 0;
			Debug.Log("gamedata.coinCount: " + DataCtrl.instance.data.coinCount);

			DataCtrl.instance.data.coinCount = gameDataSaverClass.CoinCount;
			DataCtrl.instance.data.lives = gameDataSaverClass.Lives;

			for (int i = 0; i < DataCtrl.instance.data.levelData.Length; i++)
			{
				DataCtrl.instance.data.levelData[i].levelNumber = gameDataSaverClass.LevelData.Levels[i].LevelNumber;
				DataCtrl.instance.data.levelData[i].isUnlocked = gameDataSaverClass.LevelData.Levels[i].isUnlocked;
				DataCtrl.instance.data.levelData[i].starsAwarded = gameDataSaverClass.LevelData.Levels[i].StarAwarded;
			}

			DataCtrl.instance.data.isFirstBoot = gameDataSaverClass.IsFirstBool;
			DataCtrl.instance.data.playMusic = gameDataSaverClass.PlayMusic;
			DataCtrl.instance.data.playSound = gameDataSaverClass.PlaySound;
		}
	}

	
}
