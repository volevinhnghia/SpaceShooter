using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject _explodePrefab;
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private int _dmg = 1;

    private Player _player;
    private SpawnManager _spawnManager;
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Players NULL");
        }
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.transform.name == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damaged(_dmg);
            }
        }
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            _spawnManager.startSpawn();
            if (_player)
            {
                _player.addScore(10);
            }
        }
        GameObject newObj = Instantiate(_explodePrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
