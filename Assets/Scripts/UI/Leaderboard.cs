using System.Collections.Generic;
using UnityEngine;

namespace Pub
{
    public class Leaderboard : MonoBehaviour
    {
        [SerializeField] private LeaderboardEntry leaderBoardEntryPrefab;
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform leaderboardListParent;
        [SerializeField] private GameData gameData;
        [SerializeField] private ScoreTracker scoreTracker;

        private void GenerateEntriesUi(IEnumerable<PlayerScore> entries, LeaderboardEntry _,
            Transform parent)
        {
            foreach (var entry in entries)
            {
                var entryUi = Instantiate(prefab, parent);
                entryUi.GetComponent<LeaderboardEntry>().SetValues(entry.PlayerName, entry.Score, null);
            }
        }

        private void OnEnable()
        {
            GenerateEntriesUi(scoreTracker.GetScores(gameData.LeaderboardEntriesShown), leaderBoardEntryPrefab,
                leaderboardListParent);
        }
    }
}