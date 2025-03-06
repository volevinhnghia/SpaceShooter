using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Moving();   
    }

    void Moving() 
    {
        Vector3 direction = new Vector3(0, 1, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        if (transform.position.y >= 8)
        {
            Destroy(this.gameObject);
            if (transform.parent)
            {
                Destroy(this.transform.parent.gameObject);
            }
        }
    }
}
