using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pub
{
    [System.Serializable]
    public class PlayerScoreListWrapper //for json read/write
    {
        public List<PlayerScore> Scores;
    }

    [CreateAssetMenu(fileName = "Score Tracker", menuName = "Pub/Score Tracker")]
    public class ScoreTracker : ScriptableObject
    {
        [SerializeField] private MatchEndEvent matchEndEvent;

        private List<PlayerScore> _scores;

        public void Init()
        {
            matchEndEvent.OnMatchEnd += UpdateScore;
            _scores = LoadScore() ?? new List<PlayerScore>();
        }

        public void SaveScore() => SaveScore(_scores);

        private void UpdateScore(int score)
        {
            var playerName = PlayerPrefs.GetString("PlayerName");
            _scores.Add(new PlayerScore { Score = score, PlayerName = playerName });
            _scores.Sort((x, y) => y.Score.CompareTo(x.Score));
        }

        public IEnumerable<PlayerScore> GetScores(int howManyTopScores = 0) =>
            howManyTopScores == 0 ? _scores : _scores.Take(howManyTopScores);

        private static void DeleteSave()
        {
            if (System.IO.File.Exists($"{Application.persistentDataPath}/scores.json") == false) return;
            System.IO.File.Delete($"{Application.persistentDataPath}/scores.json");
        }

        private static void SaveScore(List<PlayerScore> scores)
        {
            var wrapper = new PlayerScoreListWrapper { Scores = scores };
            var json = JsonUtility.ToJson(wrapper);
            System.IO.File.WriteAllText($"{Application.persistentDataPath}/scores.json", json);
        }

        private static List<PlayerScore> LoadScore()
        {
            if (System.IO.File.Exists($"{Application.persistentDataPath}/scores.json") == false) return null;
            var jsonFile = System.IO.File.ReadAllText($"{Application.persistentDataPath}/scores.json");
            var result = JsonUtility.FromJson<PlayerScoreListWrapper>(jsonFile);
            return result?.Scores;
        }
    }
}