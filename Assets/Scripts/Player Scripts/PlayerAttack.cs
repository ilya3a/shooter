using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager weaponManager;

    public float fireRate = 15f;
    private float nextTimeToFire;
    public float damage = 20f;

    private Animator zoomFPCameraAnimator;
    private Animator zoomMainCameraAnimator;
    private bool isZoomed = false;
    private Camera mainCamera;

    private GameObject crosshair;
    private bool isAiming;

    [SerializeField]private GameObject arrowPrefab, spearPrefab;
    [SerializeField]private Transform arrowSpearStartPosition;
    [SerializeField] private LayerMask layerMaskToAtaack; 


    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
        zoomFPCameraAnimator = transform.Find(Tags.LOOK_ROOT_TAG).transform.Find(Tags.ZOOM_CAMERA_TAG).GetComponent<Animator>();
        crosshair = GameObject.FindGameObjectWithTag(Tags.CROSSHAIR_TAG);

        mainCamera = Camera.main;
        zoomMainCameraAnimator = mainCamera.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WeaponShoot();
        ZoomInAndOut();
    }

    private void ZoomInAndOut()
    {
        if (weaponManager.GetCurrentWeapon().eWeaponAim == WeaponAim.AIM)
        {//if we need to zoom the weapon
            if (Input.GetMouseButtonDown(1))
            {
                zoomFPCameraAnimator.Play(AnimationTags.ZOOM_IN_ANIM);
                zoomMainCameraAnimator.Play(AnimationTags.ZOOM_IN_ANIM);
                isZoomed = true;
                //crosshair.SetActive(false);
            }
            if (Input.GetMouseButtonUp(1))
            {
                zoomFPCameraAnimator.Play(AnimationTags.ZOOM_OUT_ANIM);
                zoomMainCameraAnimator.Play(AnimationTags.ZOOM_OUT_ANIM);
                isZoomed = false;
                //crosshair.SetActive(true);
            }
        }
        if (weaponManager.GetCurrentWeapon().eWeaponAim == WeaponAim.SELF_AIM)
        {//self aim its has to aim for shoot
            if (Input.GetMouseButtonDown(1))
            {
                weaponManager.GetCurrentWeapon().Aim(true);
                isAiming = true;
            }
            if (Input.GetMouseButtonUp(1))
            {
                weaponManager.GetCurrentWeapon().Aim(false);
                isAiming = false;
            }
        }
    }

    private void WeaponShoot()
    {
        if(weaponManager.GetCurrentWeapon().eWeaponFireType == WeaponFireType.MULTIPLE)
        {//if we have the assault riffle
            if (Input.GetMouseButton(0) && Time.time > nextTimeToFire)
            {//if we press and hold left mouse click AND 
             //if time is bigger than the nextTimeTofire
                nextTimeToFire = Time.time + 1f / fireRate;
                weaponManager.GetCurrentWeapon().ShootAnimation();
                BulletFired();

            }
        }
        else
        {//if we have weapon that not an assault riffle
            if (Input.GetMouseButtonDown(0))
            {//if pressed left mouse click
                if (weaponManager.GetCurrentWeapon().tag == Tags.AXE_TAG)
                {//Handle Axe
                    weaponManager.GetCurrentWeapon().ShootAnimation();
                }
                if (weaponManager.GetCurrentWeapon().eWeaponBulletType == WeaponBulletType.BULLET)
                {//handle shoot
                    weaponManager.GetCurrentWeapon().ShootAnimation();
                    BulletFired();
                }
                else
                {//we have arrow or spear 
                    if (isAiming)
                    {
                        weaponManager.GetCurrentWeapon().ShootAnimation();
                        isAiming = false;

                        if (weaponManager.GetCurrentWeapon().eWeaponBulletType == WeaponBulletType.ARROW)
                        {//throw arrow
                            ThrowArrowOrSpear(WeaponBulletType.ARROW);
                        }
                        if (weaponManager.GetCurrentWeapon().eWeaponBulletType == WeaponBulletType.SPEAR)
                        {//throw spear
                            ThrowArrowOrSpear(WeaponBulletType.SPEAR);
                        }
                    }
                }
            }

        }
    }
    private void ThrowArrowOrSpear(WeaponBulletType type)
    {
        switch (type)
        {
            case WeaponBulletType.ARROW:
                //create arrow
                GameObject arrow = Instantiate(arrowPrefab);
                //set start position
                arrow.transform.position = arrowSpearStartPosition.position;
                //reffer the arrow script and launch the arrow
                arrow.GetComponent<ArrowSpearScript>().Luanch(mainCamera);
                break;

            case WeaponBulletType.SPEAR:

                GameObject spear = Instantiate(spearPrefab);
                spear.transform.position = arrowSpearStartPosition.position;

                spear.GetComponent<ArrowSpearScript>().Luanch(mainCamera);
                break;
        }

    }
    private void BulletFired()
    {
        RaycastHit hit;
        float distanceShotgun = 30f;
        float distance = 100f;
        //tells us if the bullet hit something (lower distance on shotgun)
//         if(weaponManager.GetCurrentWeapon().gameObject.tag == Tags.SHOTGUN_TAG)
//         {
//             if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, distanceShotgun,layerMaskToAtaack))
//             {
//                 if (hit.transform.gameObject.tag == Tags.ENEMY_TAG)
//                 {
//                     hit.transform.GetComponent<HealthScript>().ApllyDamage(damage);
//                 }
//             }
//         }
//         else
//         {

            if (Physics.Raycast(mainCamera.transform.position,mainCamera.transform.forward, out hit))
            {
            print("we hit " + hit.transform.tag +" " + hit.transform.name);
                 if (hit.transform.tag == Tags.ENEMY_TAG)
                 {
                    hit.transform.GetComponent<HealthScript>().ApllyDamage(damage);
                 }
            }
        /*}*/
        
    }
}
