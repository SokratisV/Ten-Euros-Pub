using System;
using UnityEngine;

namespace Pub
{
    [Serializable]
    public class PlayerScore
    {
        [SerializeField] public string PlayerName;
        [SerializeField] public int Score;
    }
}