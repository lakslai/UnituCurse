using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenVK : MonoBehaviour
{
    // ���������� ��� ������� ������
    public void OpenVk()
    {
        // �������� "https://vk.com" �� ���������� URL ���������
        string vkUrl = "https://vk.com/club224926224";

        // ��������� URL �� ������� �������� ��� ���������� �� ���������
        Application.OpenURL(vkUrl);
    }

    public void ExitGame()
    {
        // ����� �� ����
        Application.Quit();
        Debug.Log("���� ��� ??");
    }
}
