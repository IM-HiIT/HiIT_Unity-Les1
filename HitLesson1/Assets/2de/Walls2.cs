using UnityEngine;

public class Walls2 : MonoBehaviour
{
    [SerializeField] private int Speed;
    private Rigidbody2D rd;
    [SerializeField] private float Timer;
    private float _Timer;
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        rd.velocity = new(-Speed,0);
        _Timer = Timer;
    }

    void Update()
    {
        _Timer -= Time.deltaTime;
        if(_Timer <= 0)
        {
            _Timer = Timer;
            int r = Random.Range(-3, 4);
            rd.transform.position = new(10,r);
        }
    }
    public float GetTimer() {  return Timer; }
}
