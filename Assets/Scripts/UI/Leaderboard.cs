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
        [SerializeField] private List<Sprite> coinSprites;

        private void GenerateEntriesUi(IEnumerable<PlayerScore> entries, LeaderboardEntry prefab, Transform parent)
        {
            int counter = 0;
            foreach (var entry in entries)
            {
                var selectedSprite = SelectSprite(counter);
                var entryUi = Instantiate(prefab, parent);
                entryUi.GetComponent<LeaderboardEntry>().SetValues(entry.PlayerName, entry.Score, selectedSprite);
                counter++;
            }
        }

        private Sprite SelectSprite(int counter)
        {
            Sprite selectedSprite;
            if (coinSprites.Count == 0) selectedSprite = null;
            else if (counter < coinSprites.Count) selectedSprite = coinSprites[counter];
            else selectedSprite = coinSprites[coinSprites.Count - 1];
            return selectedSprite;
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