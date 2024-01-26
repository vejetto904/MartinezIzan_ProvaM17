using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonRotation : MonoBehaviour
{
    public Vector3 _maxRotation;
    public Vector3 _minRotation;
    private float offset = -51.6f;
    public GameObject ShootPoint;
    public GameObject Bullet;
    public float ProjectileSpeed = 0f;
    public float MaxSpeed;
    public float MinSpeed;
    public GameObject PotencyBar;
    private float initialScaleX;

    private void Awake()
    {
        initialScaleX = PotencyBar.transform.localScale.x;
    }

    void Update()
    {
        var mousePos = Input.mousePosition;
        var dist = mousePos - ShootPoint.transform.position;
        var ang = Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg + offset;
        transform.rotation = Quaternion.Euler(0, 0, ang);

        if (Input.GetMouseButton(0))
        {
            if (ProjectileSpeed < MaxSpeed)
            {
                ProjectileSpeed += 4f * Time.deltaTime;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            var projectile = Instantiate(Bullet, ShootPoint.transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(dist.normalized.x * ProjectileSpeed, dist.normalized.y * ProjectileSpeed);
            ProjectileSpeed = 0;

        }

        CalculateBarScale();
    }

    public void CalculateBarScale()
    {
        PotencyBar.transform.localScale = new Vector3(Mathf.Lerp(0, initialScaleX, ProjectileSpeed / MaxSpeed), PotencyBar.transform.localScale.y, PotencyBar.transform.localScale.z);
    }
}
