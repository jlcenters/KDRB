using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UI : MonoBehaviour
{

    public Image hpFill;
    public Image spFill;
    public TextMeshProUGUI wallet;
    public Player player;
    public int amountToDefeat;
    public int amountDefeated;
    public int currentScene;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        currentScene = 1;
        Debug.Log(currentScene);
    }

    public void NextLevel()
    {
        currentScene += 1;
        SceneManager.LoadScene(currentScene);
        Debug.Log(currentScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdjustHP()
    {
        hpFill.fillAmount = (float)player.hp / (float)player.maxHp;
    }

    public void AdjustWallet()
    {
        wallet.text = player.wallet.ToString();
    }
}
