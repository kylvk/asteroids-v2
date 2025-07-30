using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip shootSoundClip;
    //    public int HealthMax = 3;
    //    public int HealthCurrent;


    private AudioSource audioSource;

    public Bullet bulletPrefab;

    public float EnginePower = 10f;
    public float TurnPower = 10f;

    private Rigidbody2D rb2D;

    private Bounds screenBounds;



    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        //HealthCurrent = HealthMax;



        screenBounds = new Bounds();
        screenBounds.Encapsulate(Camera.main.ScreenToWorldPoint(Vector3.zero));
        screenBounds.Encapsulate(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f)));
 
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

        if (rb2D.position.x > screenBounds.max.x + 0.5f)
        {
            rb2D.position = new Vector2(screenBounds.min.x - 0.5f, rb2D.position.y);
        }
        else if (rb2D.position.x < screenBounds.min.x - 0.5f)
        {
            rb2D.position = new Vector2(screenBounds.max.x + 0.5f, rb2D.position.y);
        }
        else if (rb2D.position.y > screenBounds.max.y + 0.5f)
        {
            rb2D.position = new Vector2(rb2D.position.x, screenBounds.min.y - 0.5f);
        }
        else if (rb2D.position.y < screenBounds.min.y - 0.5f)
        {
            rb2D.position = new Vector2(rb2D.position.x, screenBounds.max.y + 0.5f);
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
        SoundManager.instance.PlaySoundClip(shootSoundClip, transform, 1f);


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
