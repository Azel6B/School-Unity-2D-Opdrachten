using UnityEngine;

public class CoinValue : MonoBehaviour
{
    [SerializeField] private int _scoreWorth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    public int GetScoreWorth()
    {
        return _scoreWorth;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

