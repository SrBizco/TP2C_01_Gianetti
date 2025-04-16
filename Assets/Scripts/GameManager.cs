using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    private int totalBadCivilians = 0;
    private int remainingBadCivilians = 0;
    private DroneHealth playerHealth; 

    private void Start()
    {
        
        totalBadCivilians = 0;
        foreach (var civilian in FindObjectsOfType<CivilianFSM>())
        {
            if (civilian.civilianType == CivilianType.Bad)
            {
                totalBadCivilians++;
            }
        }

        remainingBadCivilians = totalBadCivilians;

        
        playerHealth = FindObjectOfType<DroneHealth>();
    }

    
    public void EliminateBadCivilian()
    {
        remainingBadCivilians--;
        Debug.Log("Civil malo eliminado. Puntos: " + score);
    }

    
    public void IncreaseScore()
    {
        score++;
        Debug.Log("Puntos por enemigo eliminado: " + score);
    }

    
    public void DecreaseScore()
    {
        score--;
        Debug.Log("Puntos por civil bueno eliminado: " + score);
    }

    
    public void PlayerDied()
    {
        Debug.Log("¡Has muerto! La partida ha terminado.");
    }

    private void Update()
    {
        CheckVictoryCondition();
        CheckDefeatCondition();
    }

    private void CheckVictoryCondition()
    {
        if (remainingBadCivilians <= 0)
        {
            Debug.Log("¡Victoria! Has eliminado a todos los civiles malos.");
        }
    }

    private void CheckDefeatCondition()
    {
        if (playerHealth != null && playerHealth.CurrentHealth <= 0)
        {
            Debug.Log("¡Derrota! Has perdido.");
        }
    }
}