using UnityEngine;
public class Barrier : MonoBehaviour
{
    public GameObject Player;
    public GameObject Particle;
    public GameObject Restart;
    public float Speed;
    public string Namelevel;
    void Update()
    {
        transform.Rotate(0.0f, Speed, 0.0f); //непрерывное вращение
    }
    void OnTriggerEnter(Collider other) 
    {
        if (other.name == "Body")
        {
            Instantiate(Particle,other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(Player.GetComponent<Move>());
            Invoke("Restart2", 2.0f);
        }
    }
    void Restart2()
    {
        Restart.SetActive(true);
    }
}