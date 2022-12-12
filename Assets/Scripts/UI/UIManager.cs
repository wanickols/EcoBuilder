using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Button startServerButton;

    [SerializeField]
    private Button startHostButton;

    [SerializeField]
    private Button startClientButton;

    [SerializeField]
    private TextMeshProUGUI startInGameText;


    private void Awake()
    {
        Cursor.visible = true;
    }

    private void Update()
    {

    }

    private void Start()
    {
        startHostButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartHost())
            {
                Debug.Log("Host Started");
            }
            else
            {
                Debug.Log("Host Could not be Started");
            }
        });

        startServerButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartServer())
            {
                Debug.Log("Server Started");
            }
            else
            {
                Debug.Log("Server Could not be Started");
            }
        });

        startClientButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartClient())
            {
                Debug.Log("Client Started");
            }
            else
            {
                Debug.Log("Client Could not be Started");
            }
        });
    }
}

