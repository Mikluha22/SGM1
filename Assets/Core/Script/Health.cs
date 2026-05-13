using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float deathAnimationTime = 2.0f; // запас на случай ошибок, основное удаление через событие
    private Animator animator;
    private bool isDead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        // ваша логика получения урона...
        // при health <= 0:
        if (/* health <= 0 && */ !isDead)
        {
            isDead = true;
            Die();
        }
    }

    private void Die()
    {
        // Отключаем управление, коллизию, чтобы персонаж не двигался и сквозь него нельзя было пройти  // пример
        GetComponent<Collider>().enabled = false;
        animator.SetTrigger("Die");
        // Дополнительно можно через секунду-две всё же уничтожить, если событие не сработает (запасной вариант)
        // Destroy(gameObject, deathAnimationTime);
    }

    // Этот метод будет вызываться Animation Event в конце ролика смерти
    public void OnDeathAnimationEnd()
    {
        Destroy(gameObject);
    }
}
