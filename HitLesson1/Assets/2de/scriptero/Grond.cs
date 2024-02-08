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
    private Rigidbody2D rd;
    private float Timer;

    void Start()
    {
        float Speed = Wall.GetSpeed();
        rd = GetComponent<Rigidbody2D>();

        rd.velocity = new Vector2(-Speed, 0);
        Timer = Distance / Speed;

        if (First) Respawn(Timer * 0.5f);
        else Respawn(Timer);
    }
    private void Respawn(float _Timer)
    {
        EggTimer.Instance.Execute(() =>
        {
            rd.transform.position = new Vector2(18, -6.9f);
        })
        .WithDelay(_Timer)
        .OnFinish(() =>
        {
             Respawn(Timer);
        });
    }
}
