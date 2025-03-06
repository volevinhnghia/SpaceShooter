using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _dmg = 1;

    private Player _player;
    private Animator _animator;
    private AudioSource _explodeSound;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Players NULL");
        }
        _animator = GetComponent<Animator>();
        if (_animator == null) 
        {
            Debug.LogError("Animator NULL");
        }
        _explodeSound = GameObject.Find("ExplodeSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -8)
        {
            transform.position = new Vector3(Random.Range(-9,9), 10, 0);
        }
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
            _animator.SetTrigger("onEnemyDead");
            this.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(this.gameObject,2.8f);
        }
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player)
            {
                _player.addScore(10);
            }
            _animator.SetTrigger("onEnemyDead");
            this.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(this.gameObject,2.8f);
        }
        _explodeSound.Play();
    }
}
