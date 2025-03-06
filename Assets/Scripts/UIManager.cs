using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text[] _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Image _liveImage;
    [SerializeField]
    private string _scoreContent;
    [SerializeField]
    private Sprite[] _liveSprite;

    private GameManager _gameManager;

    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = _scoreContent + " " + 0;
        _player = GameObject.Find("Player").GetComponent<Player>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null) 
        {
            Debug.LogError("GameManager Null");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = _scoreContent + " " + playerScore;
    }
    public void UpdateLiveImage(int currentLive) 
    {
        _liveImage.sprite = _liveSprite[currentLive];
    }
    public void gameOver(bool check)
    {
        _gameManager.gameOver();
        _gameOverText[0].enabled = check;
        _restartText.gameObject.SetActive(check);
        StartCoroutine(GameOverFlickerRoutine(0.5f));
    }

    IEnumerator GameOverFlickerRoutine(float duration)
    {
        while (true)
        {
            _gameOverText[0].gameObject.SetActive(false);
            _gameOverText[1].gameObject.SetActive(true);
            yield return new WaitForSeconds(duration);
            _gameOverText[0].gameObject.SetActive(true);
            _gameOverText[1].gameObject.SetActive(false);
            yield return new WaitForSeconds(duration);
        }
    }
      
}
