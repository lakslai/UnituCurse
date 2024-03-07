using UnityEngine;
using Photon.Pun;

public class Projectile : MonoBehaviourPun
{
    private int damage;
    private Photon.Realtime.Player owner;
    public float speed = 10f;

    public void Initialize(int damage, Photon.Realtime.Player owner)
    {
        this.damage = damage;
        this.owner = owner;
        // Запускаем движение пули вперед
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        // Проверяем столкновение с игроком
        if (other.CompareTag("Player"))
        {
            // Применяем урон к игроку
            other.GetComponent<PlayerHealth>().TakeDamage(damage, owner);
            // Уничтожаем пулю
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
