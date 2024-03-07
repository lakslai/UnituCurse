using UnityEngine;
using Photon.Pun;
using System.Collections;

public class Weapon : MonoBehaviourPunCallbacks
{
    public int magazineSize = 30;
    public float fireRate = 0.1f;
    public float reloadTime = 2f;
    public int damage = 10;

    public GameObject projectilePrefab;
    public Transform firePoint;

    private int currentAmmo;
    private bool isReloading = false;

    void Start()
    {
        currentAmmo = magazineSize;
    }

    void Update()
    {
        if (!photonView.IsMine)
            return;

        if (isReloading)
            return;

        if (Input.GetButtonDown("Fire1"))
        {
            photonView.RPC("Shoot", RpcTarget.AllViaServer);
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < magazineSize)
        {
            StartCoroutine(Reload());
        }
    }

    [PunRPC]
    void Shoot()
    {
        if (currentAmmo > 0)
        {
            GameObject projectile = PhotonNetwork.Instantiate(projectilePrefab.name, firePoint.position, firePoint.rotation);
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.Initialize(damage, photonView.Owner);

            currentAmmo--;
            StartCoroutine(ShootDelay());
        }
        else
        {
            StartCoroutine(Reload());
        }
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(fireRate);
    }

    IEnumerator Reload()
    {
        if (isReloading)
            yield break;

        isReloading = true;
        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = magazineSize;
        isReloading = false;
    }
}
