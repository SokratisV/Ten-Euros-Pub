using System.Collections.Generic;
using UnityEngine;

namespace Pub
{
    public class Leaderboard : MonoBehaviour
    {
        [SerializeField] private LeaderboardEntry leaderBoardEntryPrefab;
        [SerializeField] private Transform leaderboardListParent;
        [SerializeField] private GameData gameData;
        [SerializeField] private ScoreTracker scoreTracker;

        private void GenerateEntriesUi(IEnumerable<PlayerScore> entries, LeaderboardEntry prefab, Transform parent)
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

        private void OnDisable()
        {
            foreach (Transform child in leaderboardListParent)
            {
                Destroy(child.gameObject);
            }
        }
    }
}