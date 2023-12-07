using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int speed; //spreekt wel voor zichzelf zegt gewoon hoe snel de palen gaan;
    [SerializeField] private float timer; // de tijd om van het scherm af te gaan;

    private float _timer; // deze telt af en kan gereset worden door timer. en de _ is omdat ie local is;
    private Rigidbody2D rd;

    private void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        rd.velocity = new Vector2(-speed, 0);
        _timer = timer; // zet begin timer;
    }
    void Update()
    {
        if (_timer < 0) // als de timer 0 is gaat ie in actie;
        {
            int rand = Random.Range(-3, 4); // zorgd dat de hoogte van het gat random word;
            transform.position = new Vector2(11f, rand); // verplaats de torens naar en begin positie;
            _timer = timer; // reset timer;
        }
        _timer -= Time.deltaTime; // telt af naar 0;
    }
}
    