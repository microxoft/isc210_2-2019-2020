using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class Game
    {
		public enum eDifficulty
		{
			Easy,
			Moderate,
			Hard
		}

        #region "Singleton"
        private static Game _currentGame;

		public static Game CurrentGame
		{
			get 
			{
				if (_currentGame == null)
				{
					_currentGame = new Game();
					_currentGame.PlayerName = "Miguel Moronta";
					_currentGame.MusicVolume = 100f;
					_currentGame.EffectsVolume = 100f;
					_currentGame.Difficulty = eDifficulty.Moderate;
				}
				return _currentGame; 
			}
			set { _currentGame = value; }
		}
		#endregion

		#region "Attributes"
		public string PlayerName;
		public float MusicVolume;
		public float EffectsVolume;
		public eDifficulty Difficulty;
		string _localPath = Application.persistentDataPath + "/lastGameState.json";
		#endregion

		#region "Behaviours"
		public void SaveCurrentState()
		{
			File.WriteAllText(_localPath, JsonUtility.ToJson(CurrentGame));
		}

		public void LoadCurrentState()
		{
			if(File.Exists(_localPath))
			{
				CurrentGame = JsonUtility.FromJson<Game>(File.ReadAllText(_localPath));
			}
		}
		#endregion
	}
}
