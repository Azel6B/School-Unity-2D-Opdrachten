using System.Collections.Generic;
using UnityEngine;


public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _CoinPrefab;
    [SerializeField] private List<Transform> _CoinSpawnLocations = new List<Transform>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(Transform location in _CoinSpawnLocations)
        {
            Instantiate(_CoinPrefab, location.position, location.rotation);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
