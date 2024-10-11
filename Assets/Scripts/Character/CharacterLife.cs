using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterLife: MonoBehaviour, IHasLife
{
    public event Action<PlayerInput> onPlayerDeath;

    [SerializeField] private TextMeshProUGUI textLifePoints;
    [SerializeField] private int lifePoints = 5;
    private bool isAlive = true;

    [SerializeField] private float timeOfImmunity = 1f;
    private bool isImmune = false;
    private Timer timer;

    [SerializeField] private AudioClip[] takeDamageAudioClips;
    
    private void Start()
    {
        UpdateTextLifePoints();

        GameObject timerObject = new GameObject("Timer");
        timer = timerObject.AddComponent<Timer>();
        timer.SetDuration(timeOfImmunity);
        timer.onTimerEnd += Tim_onTimerEnd;
    }

    private void UpdateTextLifePoints()
    {
        if (textLifePoints == null)
        {
            return;
        }
        textLifePoints.text = lifePoints.ToString();
    }
    private void StartTimeOfImmunity()
    {
        isImmune = true;
        timer.StartTimer();
    }

    private void Tim_onTimerEnd()
    {
        isImmune = false;
    }

    private void KillPlayer()
    {
        if(isAlive)
        {
            Debug.Log("Player Killed");
            isAlive = false;
            onPlayerDeath?.Invoke(this.GetComponent<PlayerInput>());

            timer.onTimerEnd -= Tim_onTimerEnd;
            Destroy(timer.gameObject);

            GetComponent<CharacterInputManager>().DisablePlayerInput();

            var chbal = GetComponentsInChildren<CharacterBalance>();
            for (int i = 0; i < chbal.Length; i++)
            {
                chbal[i].DisableBalance();
            }

            GetComponent<CharacterCollisionController>().DisableCollisionTriggerWithPlayers();
        }
    }

    public void DecreaseLifePoints(int amount)
    {
        if (!isImmune && isAlive && GameManager.isGameStarted)
        {
            lifePoints -= amount;
            StartTimeOfImmunity();
            UpdateTextLifePoints();

            SoundFXManager.Instance.PlayRandomSoundFXClip(takeDamageAudioClips, this.transform, 1f);

            if (lifePoints <= 0)
            {
                KillPlayer();
            }
        }
    }
}
