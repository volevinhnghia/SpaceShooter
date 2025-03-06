using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject[] _powerBuff;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _buffContainer;

    private IEnumerator coroutine;
    private bool _stopSpawn = false;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void startSpawn() 
    {
        coroutine = SpawnEnemyRoutine(5, _enemyPrefab, _enemyContainer);
        StartCoroutine(coroutine);
        StartCoroutine(SpawnBuffRoutine(7, _buffContainer));
    }
    IEnumerator SpawnEnemyRoutine(float waitTime, GameObject prefab, GameObject container)
    {
        yield return new WaitForSeconds(3.0f);
        Vector3 spawPos = new Vector3(Random.Range(-9f, 9f), 6f, 0);
        while (!_stopSpawn)
        {
            GameObject newObj = Instantiate(prefab, spawPos, Quaternion.identity);
            newObj.transform.parent = container.transform;
            yield return new WaitForSeconds(waitTime);
        }
    }
    IEnumerator SpawnBuffRoutine(float waitTime,  GameObject container)
    {
        yield return new WaitForSeconds(3.0f);
        Vector3 spawPos = new Vector3(Random.Range(-9f, 9f), 6f, 0);
        while (!_stopSpawn)
        {
            GameObject newObj = Instantiate(_powerBuff[Random.Range(0,3)], spawPos, Quaternion.identity);
            newObj.transform.parent = container.transform;
            yield return new WaitForSeconds(waitTime);
        }
    }
    public void OnPlayerDeath() 
    { 
        _stopSpawn = true;
    }
}
