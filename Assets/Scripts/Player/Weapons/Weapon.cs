using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    [SerializeField] float fireRate = 0;
    [SerializeField] float damage = 10;
    [SerializeField] LayerMask whatToHit;

    [SerializeField] Transform bulletTrailPref;
    [SerializeField] Transform muzzleFlashPref;

    float timeToSpawnEffect = 0;
    float effectSpawnRate = 10;

    float timeToFire = 0;
    Transform shootPoint;
	// Use this for initialization
	void Awake () {
        shootPoint = transform.Find("ShootPoint");
        if(shootPoint == null)
        {
            Debug.LogError("No Shootpoint? WHAT?!");
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if(Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
	}

    void Shoot()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 shootPointPosition = new Vector2(shootPoint.position.x, shootPoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(shootPointPosition, mousePosition - shootPointPosition, 100, whatToHit);

        if(Time.time >= timeToSpawnEffect)
        {
            Effect();
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }

        Debug.DrawLine(shootPointPosition, (mousePosition-shootPointPosition)*100, Color.cyan);
        if(hit.collider != null)
        {
            Debug.DrawLine(shootPointPosition, hit.point, Color.red);
            Debug.Log("We hit " + hit.collider.name + " and did " + damage + " damage!");
        }
    }

    void Effect()
    {
        Instantiate(bulletTrailPref, shootPoint.position, shootPoint.rotation);
        Transform clone = Instantiate(muzzleFlashPref, shootPoint.position, shootPoint.rotation) as Transform;
        clone.parent = shootPoint;
        float size = Random.Range(0.2f, 0.5f);
        clone.localScale = new Vector3(size, size, size);
        Destroy(clone.gameObject, 0.02f);
    }   

}
