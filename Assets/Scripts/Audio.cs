using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Audio : MonoBehaviour {

    AudioSource audioSource;

    public List<AudioClip> slashes = new List<AudioClip>();
    public List<AudioClip> burns = new List<AudioClip>();
    public List<AudioClip> hits = new List<AudioClip>();
    public List<AudioClip> fires = new List<AudioClip>();
    public List<AudioClip> heals = new List<AudioClip>();

    public AudioClip whetstone;
    public AudioClip select;
    public AudioClip coin;
    public AudioClip levelup;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomSlash() {
        audioSource.PlayOneShot(slashes[Random.Range(0, slashes.Count)]);
    }
    public void PlayRandomBurn() {
        audioSource.PlayOneShot(burns[Random.Range(0, burns.Count)]);
    }
    public void PlayRandomHit() {
        audioSource.PlayOneShot(hits[Random.Range(0, hits.Count)]);
    }
    public void PlayRandomFire() {
        audioSource.PlayOneShot(fires[Random.Range(0, fires.Count)]);
    }
    public void PlayRandomHeal() {
        audioSource.PlayOneShot(heals[Random.Range(0, heals.Count)]);
    }

    public void PlayWhetstone() {
        audioSource.PlayOneShot(whetstone);
    }
    public void PlaySelect() {
        audioSource.PlayOneShot(select);
    }
    public void PlayCoin() {
        audioSource.PlayOneShot(coin);
    }
    public void PlayLevelup() {
        audioSource.PlayOneShot(levelup);
    }

    public void PlaySoundAccordingToWeapon(Weapon weapon) {
        if(weapon is WeaponFireStaff) {
            PlayRandomFire();
        }
        else if(weapon is WeaponSteelSword) {
            PlayRandomSlash();
        }
        else if(weapon is WeaponWoodenBow) {
            PlayRandomHit();
        }
    }

    public void PlaySoundAccordingToDamageType(DamageType damageType) {
        if(damageType == DamageType.Fire) {
            PlayRandomBurn();
        }
    }
}
