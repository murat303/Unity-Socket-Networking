using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager ins;

    public GameObject startMenu;
    public InputField userNameField;

    public GameObject gameplay;
    public Text txtHealth;
    public Text txtBomb;
    public Text txtKill;
    public Text txtKilled;

    private void Awake()
    {
        if (ins == null) ins = this;
        else if (ins != this) Destroy(this);
    }

    public void ConnectToServer()
    {
        startMenu.SetActive(false);
        gameplay.SetActive(true);
        userNameField.interactable = false;
        Client.instance.ConnectToServer();
    }
}
