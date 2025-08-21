using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum Upgrade
{
    Single, Twin, Heavy, FullAuto
}

public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip shootSoundClip;
    [SerializeField] private AudioClip emptySoundClip;
    [SerializeField] private AudioClip defaultReloadSoundClip;
    [SerializeField] private AmmoUI ammoUI;
    //    public int HealthMax = 3;
    //    public int HealthCurrent;



    public Upgrade CurrentUpgrade = 0;

    public SpriteRenderer SpriteRenderer;

    public Transform FiringPoint;

    public Transform TwinFiringPointA;

    public Transform TwinFiringPointB;


    public int currentAmmo = 10, maxAmmo = 10;

    //full auto bs
    public float fireRate = 0.1f;
    private float nextFireTime;



    //private AudioSource audioSource;

    public Bullet bulletPrefab;



    public float EnginePower = 10f;
    public float TurnPower = 10f;

    private Rigidbody2D rb2D;

    private Bounds screenBounds;

    //ammo ui visibility, may move to game manager idk

    public AmmoUI AmmoUI;


    //reload vars
    private bool canShoot = true;
    private bool canReload = true;

    //upgrade vars
    // public bool hasTwinshot = true;

    //public bool hasTwinShot = false;
    //public bool hasInstantReload = true;
    //public bool hasFullAuto = true;


    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        //HealthCurrent = HealthMax;
        canShoot = true;
        canReload = true;

        //hasTwinShot = false;


        screenBounds = new Bounds();
        screenBounds.Encapsulate(Camera.main.ScreenToWorldPoint(Vector3.zero));
        screenBounds.Encapsulate(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f)));

        // SpriteRenderer.color = ColorX.GetRandomColor();

    }
    private void Update()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        ApplyThrust(vert);
        ApplyTorque(horiz);


        //fullauto shooting
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime && CurrentUpgrade == Upgrade.FullAuto)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
        
        //normal shooting
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //CurrentUpgrade = Twin;
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


        if (currentAmmo > 0 && canShoot && CurrentUpgrade != Upgrade.Twin)

        {
            SoundManager.instance.PlaySoundClip(shootSoundClip, transform, 1f);


            Bullet bullet = Instantiate(this.bulletPrefab, FiringPoint.transform.position, FiringPoint.transform.rotation);
            bullet.Project(FiringPoint.transform.up);
            currentAmmo--;
        }
        else if (currentAmmo > 0 && canShoot && CurrentUpgrade == Upgrade.Twin)
        {
            Bullet bullet = Instantiate(this.bulletPrefab, TwinFiringPointA.transform.position, TwinFiringPointA.transform.rotation);
            bullet.Project(TwinFiringPointA.transform.up);

            SoundManager.instance.PlaySoundClip(shootSoundClip, transform, 1f);
            StartCoroutine(TwinshotShootRoutine());
            StartCoroutine(TwinshotSoundRoutine());

            currentAmmo -= 2;
        }
        else
        {
            SoundManager.instance.PlaySoundClip(emptySoundClip, transform, 1f);
        }
    }

    //twinshotsound?
    public IEnumerator TwinshotSoundRoutine()
    {
        //SoundManager.instance.PlaySoundClip(shootSoundClip, transform, 1f);
        yield return new WaitForSeconds(0.05f);
        SoundManager.instance.PlaySoundClip(shootSoundClip, transform, 1f);

    }

    //twinshot shoot
    public IEnumerator TwinshotShootRoutine()
    {
        yield return new WaitForSeconds(0.05f);
        Bullet bullet = Instantiate(this.bulletPrefab, TwinFiringPointB.transform.position, TwinFiringPointB.transform.rotation);
        bullet.Project(TwinFiringPointB.transform.up);
    }


    //RELOADING
    public void Reload()
    {
        if (currentAmmo < maxAmmo && canReload)
        {
            StartCoroutine(Reloading());
            StartCoroutine(AmmoUI.AmmoUIRoutine());
        }
        else
        {
            SoundManager.instance.PlaySoundClip(emptySoundClip, transform, 1f);
        }
    }

    public IEnumerator Reloading()
    {
        SoundManager.instance.PlaySoundClip(defaultReloadSoundClip, transform, 1f);
        canShoot = false;
        canReload = false;
        yield return new WaitForSeconds(defaultReloadSoundClip.length);
        canShoot = true;
        canReload = true;
        int reloadAmount = maxAmmo - currentAmmo; // how many bullets to reload ammo
        //reloadAmount = (currentAmmo - reloadAmount) >= 0 ? reloadAmount : currentAmmo;
        currentAmmo += reloadAmount;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            Debug.Log("hit");
            rb2D.linearVelocity = Vector3.zero;
            rb2D.angularVelocity = 0f;

            this.gameObject.SetActive(false);

            FindAnyObjectByType<GameManager>().PlayerDied();
        }
        //SHOP ENTRY COLLISION SHIT
        if (collision.gameObject.tag == "ShopEntry")
        {
            ShopManager.instance.EnterShop();
            Debug.Log("killed momentum");
            rb2D.linearVelocity = Vector3.zero;
            rb2D.angularVelocity = 0f;

        }
        if (collision.gameObject.tag == "ShopExit")
        {
            Debug.Log("Shop exited, have a nice day");
            rb2D.linearVelocity = Vector3.zero;
            rb2D.angularVelocity = 0f;

        }
    }



//    old health garbage
//    public void TakeDamage(int damage)
//    {
//        HealthCurrent -= damage;
//        if (HealthCurrent <= 0)
//        {
//            Debug.Log("Dead");
//        }
//    }
}
