using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerEssence : MonoBehaviour
{
    public AudioManager AudioManager;
    public TextMesh PlayerLivesText;
    public GameObject GameOverText;
    public bool IsGameOver;
    const float Y_MIN_LIMIT = -4.5f;
    const float Y_MAX_LIMIT = 4.5f;
    Vector3 MovementSpeed = new Vector3(0, 20), _deltaPos;
    ScoreController _scoreController;
    int _lives = 3;
    Animator _animator;
    float _lastVerticalAxis = 0;
    EssenceWebClient _webClient;

    private void Awake()
    {
        _webClient = GameObject.Find("GlobalScriptsText").GetComponent<EssenceWebClient>();
        _animator = GetComponent<Animator>();
        AudioManager = GameObject.Find("AudioManagerText").GetComponent<AudioManager>();
        _scoreController = GameObject.Find("GlobalScriptsText").GetComponent<ScoreController>();
        GameOverText = GameObject.Find("GameOverText");
        GameOverText.SetActive(false);
    }

    private void Start()
    {
        PlayerLivesText.text = _lives.ToString();
    }

    void Update()
    {
        if (IsGameOver)
            return;

        if(Input.GetButtonDown("Fire1"))
        {
            _webClient.TopScores();
        }

        if(_lastVerticalAxis != Input.GetAxis("Vertical"))
        {
            _lastVerticalAxis = Input.GetAxis("Vertical");
            _animator.SetFloat("VerticalAxis", _lastVerticalAxis);
        }
        
        _deltaPos = MovementSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
        gameObject.transform.Translate(_deltaPos);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                                                    Mathf.Clamp(gameObject.transform.position.y, Y_MIN_LIMIT, Y_MAX_LIMIT),
                                                    gameObject.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.gameObject.tag)
        {
            case "Blue":
                _scoreController.IncrementScore(EssenceType.Blue);
                break;
            case "Green":
                _scoreController.IncrementScore(EssenceType.Green);
                break;
            case "Orange":
                _scoreController.IncrementScore(EssenceType.Orange);
                break;
            case "Purple":
                _scoreController.IncrementScore(EssenceType.Purple);
                break;
            case "Red":
                _scoreController.IncrementScore(EssenceType.Red);
                break;
            case "Yellow":
                _scoreController.IncrementScore(EssenceType.Yellow);
                break;
            case "Enemy":
                _lives--;
                PlayerLivesText.text = _lives.ToString();
                if(_lives == 0)
                {
                    // Game over!
                    IsGameOver = true;
                    GameOverText.SetActive(true);

                    _webClient.SaveScore();
                }
                AudioManager.PlaySoundEffect(AudioManager.SoundEffect.Explode);
                Destroy(other.gameObject);
                return;
        }
        AudioManager.PlaySoundEffect(AudioManager.SoundEffect.Capture);
        Destroy(other.gameObject);
    }
}
