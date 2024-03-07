using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenVK : MonoBehaviour
{
    // Вызывается при нажатии кнопки
    public void OpenVk()
    {
        // Замените "https://vk.com" на актуальный URL Вконтакте
        string vkUrl = "https://vk.com/club224926224";

        // Открываем URL во внешнем браузере или приложении по умолчанию
        Application.OpenURL(vkUrl);
    }

    public void ExitGame()
    {
        // Выход из игры
        Application.Quit();
        Debug.Log("Ждем вас ??");
    }
}
