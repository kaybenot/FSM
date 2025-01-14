using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;
    private Vector2 direction;
    private Agent agent;

    public void SetState(float _speed, Vector2 _direction, Agent _agent)
    {
        speed = _speed;
        direction = _direction;
        agent = _agent;
    }

    public bool CollidesWithWall()
    {
        foreach (var wall in World.Instance.Walls)
        {
            if (wall.Bounds.Contains(transform.position))
            {
                return true;
            }
        }

        return false;
    }

    private void Update()
    {
        if (CollidesWithWall())
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
