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
        // ��������� �������� ���� ������
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        // ��������� ������������ � �������
        if (other.CompareTag("Player"))
        {
            // ��������� ���� � ������
            other.GetComponent<PlayerHealth>().TakeDamage(damage, owner);
            // ���������� ����
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
