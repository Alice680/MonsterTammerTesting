using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBattleSprite : MonoBehaviour
{
    private float spawn_time;
    [SerializeField] private float life_time;

    private void Start()
    {
        spawn_time = Time.time;
    }

    private void Update()
    {
        if (Time.time - spawn_time > life_time)
            Destroy(gameObject);
    }
}