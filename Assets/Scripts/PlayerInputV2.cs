using System;
using UnityEngine;

public class PlayerInputV2 : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private string _CoinTag = "Coin";
    private int _score;
    private Animator _animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //_animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Animator Component not found on player");
        }

    }

    // Update is called once per frame
    void Update()
    {
        bool isMoving = false;

        if (Input.GetKey(KeyCode.W))
        {
            print("Ik heb W ingedrukt");
            transform.position += new Vector3(0, 1, 0) * Time.deltaTime * _speed;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = new Vector3(1, 1, 1);
            isMoving = true;

        }
        else if (Input.GetKey(KeyCode.A))
        {
            print("ik heb A ingedrukt");
            transform.position -= new Vector3(1, 0, 0) * Time.deltaTime * _speed;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = new Vector3(-1, 1, 1);
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            print("ik heb S ingedrukt");
            transform.position += new Vector3(0, -3, 0) * Time.deltaTime * _speed;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = new Vector3(1, 1, 1);
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            print("ik heb D ingedrukt");
            transform.position += new Vector3(1, 0, 0) * Time.deltaTime * _speed;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = new Vector3(1, 1, 1);
            isMoving = true;
        }
        _animator.SetBool("IsMoving", isMoving);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CoinValue coinValue;
        if (collision.gameObject.CompareTag(_CoinTag) && collision.gameObject.TryGetComponent<CoinValue>(out coinValue))
        {
            _score += coinValue.GetScoreWorth();
            Destroy(collision.gameObject);
            print("Score: " + _score);
        }
    }
}

