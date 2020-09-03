using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim
{
    NONE,
    SELF_AIM,
    AIM
}

public enum WeaponFireType
{
    SINGLE,
    MULTIPLE
}

public enum WeaponBulletType
{
    BULLET,
    ARROW,
    SPEAR,
    NONE
}
public class WeaponHandler : MonoBehaviour
{
    private Animator animator;

    public WeaponAim eWeaponAim;
    public WeaponFireType eWeaponFireType;
    public WeaponBulletType eWeaponBulletType;

    [SerializeField] private GameObject muzzleFlash;

    [SerializeField] private AudioSource shootSound, reloadSound;

    public GameObject attackPoint;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void ShootAnimation()
    {
        animator.SetTrigger(AnimationTags.SHOOT_TRIG);
    }
    public void Aim(bool canAim)
    {
        animator.SetBool(AnimationTags.AIM_PRAM, canAim);
    }
    void TurnOnMuzzleFlash()
    {
        muzzleFlash.SetActive(true);
    } 
    void TurnOffMuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }
    void PlayShootSound()
    {
        shootSound.Play();
    }
    void PlayReloadSound()
    {
        reloadSound.Play();
    }
    // works from the animation
    void TurnOnAttackPoint()
    {
        attackPoint.SetActive(true);
    }
    void TurnOffAttackPoint()
    {
        if (attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }
    }
}
