using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grond : MonoBehaviour
{
    private int Speed;
    [SerializeField] private bool First;
    [SerializeField] private float Distance;
    private Rigidbody2D rd;
    private float Timer;
    [SerializeField] private Walls2 Wall;
    private float _Timer;
    void Start()
    {
        Speed = Wall.GetSpeed();
        Timer = Distance / Speed;
        rd = GetComponent<Rigidbody2D>();
        rd.velocity = new Vector2(-Speed, 0);
        _Timer = Timer;
        if (First) { _Timer = Timer * 0.5f;}
    }

    private void Update()
    {
        _Timer -= Time.deltaTime;
        if (_Timer <= 0)
        {
            _Timer = Timer;
            rd.transform.position = new Vector2(18, -6.9f);
        }
    }
}
