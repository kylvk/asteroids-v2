using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip shootSoundClip;
    [SerializeField] private AudioClip emptySoundClip;
    //    public int HealthMax = 3;
    //    public int HealthCurrent;
    public int currentAmmo = 10, maxAmmo = 10;


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

        if(Input.GetKeyDown(KeyCode.R))
        {
            // add delay/loading bar for UI
            Reload();
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
    //SHOOTING
    private void Shoot()
    {
        if (currentAmmo > 0)

        {
            SoundManager.instance.PlaySoundClip(shootSoundClip, transform, 1f);


            Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
            bullet.Project(this.transform.up);
            currentAmmo--;
        }
        else
        {
            SoundManager.instance.PlaySoundClip(emptySoundClip, transform, 1f);
        }
    }

    //RELOADING
    public void Reload()
    {
        int reloadAmount = maxAmmo - currentAmmo; // how many bullets to reload ammo
        //reloadAmount = (currentAmmo - reloadAmount) >= 0 ? reloadAmount : currentAmmo;
        currentAmmo += reloadAmount;
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
