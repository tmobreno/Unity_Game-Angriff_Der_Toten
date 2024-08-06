using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [HideInInspector]
    private PlayerData data;

    private Camera mainCam;
    private Vector3 mousePos;
    private Transform bulletTrans;
    private bool canFire, canGrenade, isReloading;
    private float timer;

    [SerializeField]
    private GameObject sprite, grenade;

    // Start is called before the first frame update
    void Start()
    {
        canGrenade = true;
        InitializeComponents();
    }

    // Update is called once per frame
    void Update()
    {
        ControlUIMenus();

        if (data.isDead) { return; }
        if (UIPauseMenu.Instance.IsPaused) { return; }
        UpdateAim();

        UpdateFiring();

        // Weapon Swap
        if (Input.GetKeyDown(KeyCode.E))
        {
            data.SwapWeapon();
        }

        // Fire
        if(Input.GetMouseButton(0))
        {
            FireBullet();
        }

        if (Input.GetMouseButton(2))
        {
            ThrowGrenade();
        }

        // Reload
        if (Input.GetKey(KeyCode.R) && !isReloading)
        {
            isReloading = true;
            AudioManager.Instance.PlayGunSound(data.GetEquippedWeapon().type.Description + "_reload");
            StartCoroutine(WaitForReload(data.GetEquippedWeapon().reloadSpeed / data.reloadSpeedMod));
            StartCoroutine(data.GetEquippedWeapon().ReloadWeapon(data.reloadSpeedMod));
        }
    }

    private void InitializeComponents()
    {
        data = this.GetComponentInParent<PlayerData>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        bulletTrans = this.transform.GetChild(0).GetComponent<Transform>();
    }

    private void UpdateAim()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        sprite.transform.rotation = Quaternion.Euler(-rotZ, 90, -90);

        /// Add to update rotation for two handed weapons
        /// if (data.GetEquippedWeapon().GetWeaponType().Hands == 2) { sprite.transform.rotation = Quaternion.Euler(-rotZ, 90, -90); }
    }

    private void UpdateFiring()
    {
        if (!canFire)
        {
            timer += Time.deltaTime;
            if(timer > data.GetEquippedWeapon().firingTime)
            {
                canFire = true;
                timer = 0;
            }
        }
    }

    private void FireBullet()
    {
        int pellets = data.GetEquippedWeapon().type.Pellets;

        if (canFire && !isReloading && data.GetEquippedWeapon().CheckRemainingAmmo())
        {
            canFire = false;

            if (data.GetEquippedWeapon().type.Burst)
            {
                StartCoroutine(SpawnBurstBullets(pellets));
            }
            else
            {
                data.GetEquippedWeapon().ChangeRemainingAmmo(-1);
                SpawnBullets(pellets);
            }
        }
    }

    private void SpawnBullets(int pellets)
    {
        for (int i = 0; i < pellets; i++)
        {
            SpawnBullet();
        }
    }

    private IEnumerator SpawnBurstBullets(int pellets)
    {
        for (int i = 0; i < pellets; i++)
        {
            data.GetEquippedWeapon().ChangeRemainingAmmo(-1);
            SpawnBullet();
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void SpawnBullet()
    {
        float scatter = data.GetEquippedWeapon().type.Scatter;
        float scatterOffset = Random.Range(-scatter, scatter);

        GameObject bullet = Instantiate(data.GetEquippedWeapon().bullet, bulletTrans.position, Quaternion.identity);
        bullet.GetComponent<BulletMovement>().SetDamage(data.GetEquippedWeapon().damage);
        bullet.GetComponent<BulletMovement>().SetOffset(scatterOffset);
        bullet.GetComponent<BulletMovement>().SetPlayerData(data);
        AudioManager.Instance.PlayGunSound(data.GetEquippedWeapon().type.Description + "_fire");
    }

    private void ThrowGrenade()
    {
        if (!canGrenade) { return; }
        if (data.GrenadeCount == 0) { return; }
        data.UseGrenade();
        GameObject g = Instantiate(grenade, this.transform.position, this.transform.rotation);
        g.GetComponent<Grenade>().EndPosition = this.transform.GetChild(1).GetComponent<Transform>();
        canGrenade = false;
        StartCoroutine(WaitForGrenade(2f));
    }

    private IEnumerator WaitForGrenade(float time)
    {
        yield return new WaitForSeconds(time);
        canGrenade = true;
    }

    private IEnumerator WaitForReload(float time)
    {
        yield return new WaitForSeconds(time);
        isReloading = false;
    }

    private void ControlUIMenus()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            UIScoreboardData.Instance.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            UIScoreboardData.Instance.SetActive(false);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            if (!UIPauseMenu.Instance.CanPause) { return; }
            StartCoroutine(Pause());
        }
    }

    IEnumerator Pause()
    {
        UIPauseMenu.Instance.SetCanPause(false);
        UIPauseMenu.Instance.SetPaused(!UIPauseMenu.Instance.IsPaused);
        yield return new WaitForSecondsRealtime(0.5f);
        UIPauseMenu.Instance.SetCanPause(true);
    }
}
