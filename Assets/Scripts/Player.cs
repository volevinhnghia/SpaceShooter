using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _HP = 5;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _triplelaserPrefab;
    [SerializeField]
    private Vector3 spawPos = new Vector3(0,1.05f,0);
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _nextFire = 0.0f;
    

    private SpawnManager _spawnManager;
    private UIManager _uiManager;

    private bool _isTripleShotActive = false;
    private bool _isSpeedBuffActive = false;
    private bool _isShieldBuffActive = false;

    private GameObject Shield;
    private GameObject leftEngine, rightEngine;
    private AudioSource _laserSound;

    private int _score = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("SpawnManage null!!");
        }
        _laserSound = GameObject.Find("LaserSound").GetComponent<AudioSource>();
        if (_laserSound == null)
        {
            Debug.LogError("_laserSound null!!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        playerBound();
        if (Input.GetKey(KeyCode.Space) && Time.time > _nextFire)
        {
            fire();
        }
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
    }

    void playerBound()
    {
        if (transform.position.y >= 6)
        {
            transform.position = new Vector3(transform.position.x, 6, 0);
        }
        else if (transform.position.y <= -4)
        {
            transform.position = new Vector3(transform.position.x, -4, 0);
        }
        if (transform.position.x >= 9)
        {
            transform.position = new Vector3(9, transform.position.y, 0);
        }
        else if (transform.position.x <= -9)
        {
            transform.position = new Vector3(-9, transform.position.y, 0);
        }
    }
    void fire() 
    {
        _nextFire = Time.time + _fireRate;
        _laserSound.Play();
        if (_isTripleShotActive)
        {
            Instantiate(_triplelaserPrefab, transform.position + spawPos, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + spawPos, Quaternion.identity);
        }
    }

    public void setTriggerTripleShot(bool isTrigger)
    {
        _isTripleShotActive = isTrigger;
        StartCoroutine(BuffTripleShotDuration(5.0f));
    }
    public void setTriggerShield(bool isTrigger)
    {
        Shield = this.gameObject.transform.GetChild(0).gameObject;
        _isShieldBuffActive = isTrigger;
        Shield.SetActive(isTrigger);
        if (isTrigger)
        {
            StartCoroutine(BuffShieldDuration(5.0f));
        }
    }
    public void setTriggerSpeedBuff(bool isTrigger, float mul)
    {
        _isSpeedBuffActive = isTrigger;
        switch (isTrigger)
        {
            case true:
                _speed *= mul;
                StartCoroutine(BuffSpeedDuration(5.0f, mul));
                break;
            case false:
                _speed /= mul;
                break;
        }
        
    }
    public void setTriggerEngine(bool isTrigger) 
    {
        leftEngine = this.gameObject.transform.GetChild(2).gameObject;
        rightEngine = this.gameObject.transform.GetChild(3).gameObject;
        switch (_HP)
        {
            case 2:
                leftEngine.SetActive(isTrigger);
                break;
            case 1:
                rightEngine.SetActive(isTrigger);
                break;
        }
    }
    public void Damaged(int dmg)
    {
        if (_isShieldBuffActive)
        {
            return;
        }
        _HP = _HP - dmg;
        _uiManager.UpdateLiveImage(_HP);
        setTriggerEngine(true);
        if (_HP <= 0) 
        {
            _HP = 0;
            _uiManager.gameOver(true);
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    IEnumerator BuffTripleShotDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        setTriggerTripleShot(false);

    }
    IEnumerator BuffSpeedDuration(float duration, float mul)
    {
        yield return new WaitForSeconds(duration);
        setTriggerSpeedBuff(false, mul);
    }
    IEnumerator BuffShieldDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        setTriggerShield(false);
    }

    public void addScore(int score) 
    {
        _score = _score + score;
        _uiManager.UpdateScore(_score);
    }

}
