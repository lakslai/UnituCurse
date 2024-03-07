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
        // ��������� ������ ������, ��������, �������� ������ ������
        Debug.Log("Player died");

        // �������� ����� �� ���� ��������, ����� ���������� ������ ������
        photonView.RPC("HandleDeath", RpcTarget.All, attacker);
    }

    [PunRPC]
    void HandleDeath(Photon.Realtime.Player attacker)
    {
        // �������������� ������ ��������� ������, ������� ������ ���� ��������� �� ���� ��������
        Debug.Log("Handling death on all clients");
    }
}
