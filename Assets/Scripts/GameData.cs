﻿using UnityEngine;

namespace Pub
{
    [CreateAssetMenu(fileName = "Game Data", menuName = "Pub/New Game Data")]
    public class GameData : ScriptableObject
    {
        [Min(1)] public float InitialRoundTimer = 30;
        [Min(5)] public int MaxNumberOfCoins = 20;
    }
}