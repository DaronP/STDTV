using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] jugadoresPrefabs;
    public Transform[] puntosParaSpawn;
    public float gameTime = 180f;
    public int frameRate;

    private Jugador[] jugadores;
    private float timeEllapsed = 0f;

    private void Start()
    {
        jugadores = new Jugador[4];
        StartCoroutine(CrearJugadores());
        // testing
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = frameRate;
    }

    private void Update()
    {
        timeEllapsed += Time.deltaTime;
        if (timeEllapsed > gameTime)
        {
            // game finished
            GameFinished();
            timeEllapsed = 0f;
            Debug.Log("GAME FINISHED!");
        }
    }

    private IEnumerator CrearJugadores()
    {
        int r;
        Vector2 pos;
        // crear jugadores
        for(int i = 0; i < 4; i++)
        {
            float x = Random.Range(puntosParaSpawn[0].position.x, puntosParaSpawn[1].position.x);
            float y = Random.Range(puntosParaSpawn[0].position.y, puntosParaSpawn[1].position.y);
            pos = new Vector2(x, y);
            r = Random.Range(0, jugadoresPrefabs.Length);
            GameObject go = Instantiate(jugadoresPrefabs[r], pos, Quaternion.identity);
            yield return new WaitForSeconds(1f);
            jugadores[i] = go.GetComponent<Jugador>();
            jugadores[i].playerNumber = i;
        }
    }

    private void GameFinished()
    {
        Time.timeScale = 0f;
        float playerScore = 0f;
        for(int i = 0; i < 4; i++)
        {
            try
            {
                playerScore = jugadores[i].vida.golpeDado / jugadores[i].vida.golpeRecivido;
            }
            catch(System.Exception e)
            {
                playerScore = 0f;
            }
            Debug.Log("Player " + i + " score-> " + playerScore);
        }
    }
}
