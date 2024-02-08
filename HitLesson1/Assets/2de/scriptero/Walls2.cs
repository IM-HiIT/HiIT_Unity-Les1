using Cikoria.EggTimer;
using UnityEngine;

public class Walls2 : MonoBehaviour
{
    [SerializeField] private int Speed;
    private Rigidbody2D rd;
    [SerializeField] private float Timer;
    private EggTimerAction Egg;

    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        rd.velocity = new Vector2(-Speed,0);
        Respawn();
    }

    private void Respawn()
    {
        Egg = EggTimer.Instance.Execute(() =>
        {
            int r = Random.Range(-3, 4);
            rd.transform.position = new Vector2(10, r);
        })
        .WithDelay(Timer)
        .OnFinish(() =>
        {
            Respawn();
        });
    }
    public float GetTimer() {  return Timer; }
    public int GetSpeed() { return Speed; }
    public void KillEgg()
    {
        EggTimer.Instance.Remove(Egg);
    }
}
