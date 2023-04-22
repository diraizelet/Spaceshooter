using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_manager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyprefab;
    [SerializeField]
    private GameObject _enemycontainer;

    private bool _stopspawning = false;
    [SerializeField]
    private GameObject[] _powerups;

    
    public void StartSpawning()
    {
        //Debug.LogError("StartSpawning initiated");
        StartCoroutine(spawn());
        StartCoroutine(Spawnpoweruproutines());
    }

    IEnumerator spawn()
    {
        // yeild return null; //wait for one frame and goes to next line
        //Debug.LogError("Spawn");
        // yeild return new WaitForSeconds(5.0f);
        yield return new WaitForSeconds(3.0f);
        while(_stopspawning != true)
        {
            Vector3 posttospawn = new Vector3(Random.Range(-9.05f, 9.28f), 18.3f, 0);
            GameObject newenemy = Instantiate(_enemyprefab, posttospawn, Quaternion.identity);
            newenemy.transform.parent= transform;
            yield return new WaitForSeconds(4.0f);
        }
    }

    IEnumerator Spawnpoweruproutines()
    {
        yield return new WaitForSeconds(3.0f);
        while(_stopspawning == false)
        {
            int randompowerup = Random.Range(0, 3);
            Vector3 postospawn = new Vector3(Random.Range(-9.05f, 9.28f), 18.3f, 0);
            Instantiate(_powerups[randompowerup] , postospawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }
    public void onplayerdeath()
    {
        _stopspawning = true;
    }
}
