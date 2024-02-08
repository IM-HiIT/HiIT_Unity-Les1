using Cikoria.EggTimer;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Grond : MonoBehaviour
{
    [SerializeField] private bool First;
    [SerializeField] private float Distance;
    [SerializeField] private Walls2 Wall;
    private EggTimerAction Egg;
    private Rigidbody2D rd;
    private float Timer; 
    private float _Timer;

    void Start()
    {
        float Speed = Wall.GetSpeed();
        rd = GetComponent<Rigidbody2D>();

        rd.velocity = new Vector2(-Speed, 0);
        Timer = Distance / Speed;
        _Timer = Timer;
        if (First) _Timer *= 0.5f;
    }
    private void Update()
    {
        _Timer -= Time.deltaTime;
        if(_Timer < 0) 
        { 
            rd.transform.position = new Vector2(18, -6.9f);
            _Timer = Timer;
        }
    }
}
