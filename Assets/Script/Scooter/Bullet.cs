using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage { get; private set; }
    private Vector3 _direct;
    private float _speed;
    private float _maxFlyDistanse;

    private float _currentFlyDistanse;

    public void Init(float damage, Vector3 direct, float speed, float maxFlyDistanse)
    {
        _currentFlyDistanse = 0f;
        Damage = damage;
        _direct = direct;
        _speed = speed;
        _maxFlyDistanse = maxFlyDistanse;
    }

    void Update()
    {
        float delta = _speed * Time.deltaTime;
        _currentFlyDistanse += delta;
        transform.Translate(_direct * delta);

        if (_currentFlyDistanse >= _maxFlyDistanse)
            Destroy(gameObject);
        
    }
}
