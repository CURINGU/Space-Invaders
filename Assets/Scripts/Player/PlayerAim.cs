using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private PlayerMovement player;
    public Transform renderObject;
    private PlayerSoundFeedback soundFeedback;

    [Header("Laser")]
    public GameObject laserPf;
    public Transform leftFirePoint;
    public Transform rightFirePoint;
    public bool isReloading;
    public float reloadTime = 0;
    public float maxReloadTime;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerMovement>();
        soundFeedback = GetComponentInChildren<PlayerSoundFeedback>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
        {
            reloadTime += Time.deltaTime;
            if (reloadTime >= maxReloadTime)
            {
                reloadTime = 0;
                isReloading = false;
            }
        }

        if (Input.GetMouseButton(0) && player.canMove && !isReloading)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        isReloading = true;
        Instantiate(laserPf, leftFirePoint.position, leftFirePoint.parent.rotation);
        Instantiate(laserPf, rightFirePoint.position, rightFirePoint.parent.rotation);
        soundFeedback.PlaySound(PlayerSoundType.shootSound);
    }
}
