using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundSystem : MonoBehaviour
{
    [SerializeField] private GameObject _gamePanelInfo;
    private GameInfo _gameInfo;
    public bool _roundStart { get; set; } = false;
    public bool _isTimeBetweenRoundsStart { get; set; } = false;
    private float _timeToStartRound = 30f;

    [SerializeField] private Text _startRoundText;

    private void Start()
    {
        _gameInfo = _gamePanelInfo.GetComponent<GameInfo>();
    }

    private void Update()
    {
        if(!_isTimeBetweenRoundsStart)
        {
            StartCoroutine(StartRound());
        }
    }
    private IEnumerator StartRound()
    {
        _isTimeBetweenRoundsStart = true;
        yield return new WaitForSeconds(_timeToStartRound);
        StartCoroutine(ShowText());
        _roundStart = true;
        _gameInfo.gameRound++;
    }
    private IEnumerator ShowText()
    {
        _startRoundText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        _startRoundText.gameObject.SetActive(false);
    }
    public void AddMoney()
    {
        _gameInfo.playerCash += 100;
    }
}
