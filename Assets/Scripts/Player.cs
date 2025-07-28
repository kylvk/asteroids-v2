using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
//    public int HealthMax = 3;
//    public int HealthCurrent;

    public Bullet bulletPrefab;

    public float EnginePower = 10f;
    public float TurnPower = 10f;

    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        //HealthCurrent = HealthMax;

    }
    private void Update()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        ApplyThrust(vert);
        ApplyTorque(horiz);

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    private void ApplyThrust(float amount)
    {
        Vector2 thrust = transform.up * EnginePower * Time.deltaTime * amount;
        rb2D.AddForce(thrust);
    }
    private void ApplyTorque(float amount)
    {
        float torque = amount * TurnPower * Time.deltaTime;
        rb2D.AddTorque(-torque);
    }
   private void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            rb2D.linearVelocity = Vector3.zero;
            rb2D.angularVelocity = 0f;

            this.gameObject.SetActive(false);

            FindAnyObjectByType<GameManager>().PlayerDied();
        }    
    }




//    public void TakeDamage(int damage)
//    {
//        HealthCurrent -= damage;
//        if (HealthCurrent <= 0)
//        {
//            Debug.Log("Dead");
//        }
//    }
}
