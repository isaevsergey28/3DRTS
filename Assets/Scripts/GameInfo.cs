using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfo : MonoBehaviour
{
  
    [SerializeField] private Text _gameInfoText;

    public int playerCash { get; set; } = 200;

    public int gameRound { get; set; } = 0;

    public int meleeCount { get; set; } = 0;

    public int rangedCount { get; set; } = 0;

    private void Update()
    {
        _gameInfoText.text =  $"Money = {playerCash}; Round = {gameRound}; Melee = {meleeCount}; Ranged = {rangedCount};";
    }

}
