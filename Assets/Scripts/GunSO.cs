using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Gun")]
public class GunSO : ScriptableObject
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 30f;
    public float UpwardForce = 30f;
    public int damage= 10;
}
