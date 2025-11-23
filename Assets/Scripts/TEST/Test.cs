using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    [SerializeField] private int _coinAmount = 10000;

    private DataSaver _saver;

    private void Awake()
    {
        _saver = new DataSaver();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            _saver.SetCoins(_coinAmount);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerPrefs.DeleteAll();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
