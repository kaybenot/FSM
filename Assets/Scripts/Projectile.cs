using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;
    private Vector2 direction;
    private float lifeTime;
    private float start;
    private Agent agent;

    public void SetState(float _speed, Vector2 _direction, float _lifeTime, Agent _agent)
    {
        speed = _speed;
        direction = _direction;
        lifeTime = _lifeTime;
        agent = _agent;
        start = Time.time;
    }

    private void Update()
    {
        if (start + lifeTime < Time.time)
        {
            Destroy(gameObject);
        }

        transform.position += (Vector3)direction * speed * Time.deltaTime;
        if (Vector2.Distance(agent.Position, transform.position) < 0.5f)
        {
            agent.Damage(1);
            Destroy(gameObject);
        }
    }
}
