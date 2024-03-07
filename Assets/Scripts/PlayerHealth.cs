using UnityEngine;
using Photon.Pun;

public class PlayerHealth : MonoBehaviourPun
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    [PunRPC]
    public void TakeDamage(int damage, Photon.Realtime.Player attacker)
    {
        if (!photonView.IsMine)
            return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die(attacker);
        }
    }

    void Die(Photon.Realtime.Player attacker)
    {
        // ќбработка смерти игрока, например, открытие экрана смерти
        Debug.Log("Player died");

        // ¬ызываем метод на всех клиентах, чтобы обработать смерть игрока
        photonView.RPC("HandleDeath", RpcTarget.All, attacker);
    }

    [PunRPC]
    void HandleDeath(Photon.Realtime.Player attacker)
    {
        // ƒополнительна€ логика обработки смерти, котора€ должна быть выполнена на всех клиентах
        Debug.Log("Handling death on all clients");
    }
}
