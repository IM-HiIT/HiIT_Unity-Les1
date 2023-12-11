using TMPro;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    [SerializeField] Manager2 mane;
    [SerializeField] TMP_Text text;
    [SerializeField] private int sprong;

    private Rigidbody2D rd;
    private int punten = 0;

    // Start is called before the first frame update
    void Start(){ rd = GetComponent<Rigidbody2D>(); }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rd.velocity = new Vector2(0,sprong);
        }
        text.text = "Punten: " + punten; 
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Punt")) { punten++; }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall")) { mane.HitWall(punten); }
    }
}
