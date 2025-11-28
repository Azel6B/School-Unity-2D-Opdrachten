using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerInputV2 : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            print("Ik heb W ingedrukt");
            transform.position += new Vector3(0, 1, 0) * Time.deltaTime * _speed;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = new Vector3(1, 1, 1);

        }
            else if (Input.GetKey(KeyCode.A))
            {
                print("ik heb A ingedrukt");
                transform.position -= new Vector3(1, 0, 0) * Time.deltaTime * _speed;   
            transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (Input.GetKey(KeyCode.S)) {
                print("ik heb S ingedrukt");
                transform.position += new Vector3(0, -3, 0) * Time.deltaTime * _speed;  
            transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.localScale = new Vector3(1, 1, 1);
            }
        else if (Input.GetKey(KeyCode.D))
            {
                print("ik heb D ingedrukt");
                transform.position += new Vector3(1, 0, 0) * Time.deltaTime * _speed;
            transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision);
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
}

