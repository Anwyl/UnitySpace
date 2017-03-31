using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {


    public static GameMaster gm;

    [SerializeField]
    private int maxLives = 4;

    private static int _remainingLives = 1;
    public static int RemainingLives

    {
        get { return _remainingLives; }

    }
    void Awake()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GControl").GetComponent<GameMaster>();
        }
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 2;
    public Transform spawnPrefab;
    public string spawnSoundName;

    [SerializeField]
    private GameObject GameOverUI;


  

    void Start()
    {
        _remainingLives = maxLives;



     
    }


    public /*static*/ void EndGame()
    {
        GameOverUI.SetActive(true);
    }

    public IEnumerator _RespawnPlayer()
    {
        
        yield return new WaitForSeconds(spawnDelay);

        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        GameObject clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
        Destroy(clone, 3f);
    }

    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
       
        _remainingLives -= 1;
        if(_remainingLives <= 0)
        {
            gm.EndGame();
        }
        else
        {
            gm.StartCoroutine(gm._RespawnPlayer());
        }
    }

    

    public static void KillEnemy(Enemy enemy)
    {
        gm._KillEnemy(enemy);
    }
    public void _KillEnemy(Enemy _enemy)
    {
        GameObject _clone = Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity) as GameObject;
        Destroy(_clone, 5f);
        Destroy(_enemy.gameObject);
    }

   

}