using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;
    [SerializeField]
    private int _powerUpID;

    private AudioSource _powerUpSound;
    // Start is called before the first frame update
    void Start()
    {
        _powerUpSound = GameObject.Find("PowerUpSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -6) 
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player") 
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                _powerUpSound.Play();
                switch (_powerUpID)
                {
                    case 0:
                        player.setTriggerTripleShot(true);
                        break;
                    case 1:
                        player.setTriggerSpeedBuff(true, 5.0f);
                        break;
                    case 2:
                        player.setTriggerShield(true);
                        break;
                }
                
            }
            Destroy(this.gameObject);

        } 
        else
        {

        }
    }
}
