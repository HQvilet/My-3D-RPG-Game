using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EditorAttributes;
using GameSaveLoadSystem;
using MEC;
using Mutant;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class InGameEventHandler : Singleton<InGameEventHandler>
{
    public enum GameMode
    {
        BOSS_FIGHT,
        ENDLESS
    }
    class EventTimeStamp
    {
        public EventTimeStamp(int time, Action action)
        {
            this.time = time;
            this.action = action; 
        }
        public int time;
        public bool done;
        public Action action;
    }

    [Header("Game content")]
    [HideInInspector] public GameMode gameMode;
    public MutantBehaviour bossRef;
    public EntityComponent playerRef;
    public bool isInBattle;

    [Header("Spawn Data")]
    public GameObject _zombiePrefap;
    public GameObject _archerPrefap;

    public List<Transform> spawners;
    public float spawnRadius = 10f;
    public Transform enemyContainer;

    [Header("Sound & VFX")]
    public GameObject _spawnVFX;

    public Action OnPlayerEnter = delegate { };
    public Action OnPlayerDead = delegate { };
    public Action OnBossDead = delegate { };
    public UnityEvent OnGameResetState;

    public CountdownTimer bossFightTimer;
    public StopwatchTimer endlessTimer;

    // [Header("UI")]
    // public TextMeshProUGUI timerTextMesh;
    
    [Header("Boss Fight Config")]
    public float bossFightDuration;
    List<EventTimeStamp> eventTimeStamps = new();
    int eventStepCount;
    public bool isBossDead;

    [Header("Endless Config")]
    public float zombieSpawnRate = 0.7f;
    public float spawnRate = 20f;
    public int amount = 20;

    void Start()
    {
        isBossDead = GameDataManager.GetLoadedData().isBossDead;

        gameMode = isBossDead ? GameMode.ENDLESS : GameMode.BOSS_FIGHT;

        bossFightTimer = new CountdownTimer(bossFightDuration);
        endlessTimer = new StopwatchTimer();
        EnableEndlessEvent();

        playerRef.damageableObject.OnEntityDied += () => OnPlayerDead.Invoke();
        bossRef.entity.damageableObject.OnEntityDied += () => OnBossDead.Invoke();

        OnPlayerEnter += () =>
        {
            GameUIManager.Instance.SetTimerVisibility(true);
            AudioManager.Instance.SwitchToBossMusic();
        };
        
        OnPlayerDead += () =>
        {
            GameLose();
        };

        OnBossDead += () =>
        {
            isBossDead = true;
            GameWin();
            ClearEnemies();
        };

        EnableEndlessEvent();
        AddInGameBossFightEvents();

    }

    void EnableEndlessEvent()
    {
        void DoSpawnByRate()
        {
            int z_count = Convert.ToInt32((zombieSpawnRate + Random.Range(-0.1f, 0.1f)) * amount);
            SpawnRateByAmount(_zombiePrefap, z_count);
            SpawnRateByAmount(_archerPrefap, amount - z_count);
        }

        float ct = 3f;
        endlessTimer.OnTick += (t) =>
        {
            GameUIManager.Instance.SetTimer(Convert.ToInt32(t));
            ct -= Time.deltaTime;
            if(ct <= 0)
            {
                ct = spawnRate + Random.Range(-3f, 3f);
                DoSpawnByRate();
            }
        };
    }

    void Update()
    {
        bossFightTimer.Tick(Time.deltaTime);
        endlessTimer.Tick(Time.deltaTime);
    }

    void AddInGameBossFightEvents()
    {
        bossFightTimer.OnTimerFinish += () =>
        {
            GameUIManager.Instance.SetBossHealthBarVisibility(true);
            bossRef.SetBossPhase();
        };
        
        if(eventTimeStamps.Count <= 0)
            eventTimeStamps.AddRange(new []{
                // new EventTimeStamp(3*60, () =>
                // {
                    
                // }),
                new EventTimeStamp(3*60, () =>
                {
                    SpawnRateByAmount(_zombiePrefap, 35);
                    SpawnRateByAmount(_archerPrefap, 8);
                    bossRef.Roaring();
                }),
                new EventTimeStamp(6*60, () =>
                {
                    bossRef.SetPhase_3();
                    SpawnRateByAmount(_zombiePrefap, 18);
                    SpawnRateByAmount(_archerPrefap, 8);
                    bossRef.Roaring();
                }),
                new EventTimeStamp(8*60, () =>
                {
                    SpawnRateByAmount(_zombiePrefap, 20);
                    SpawnRateByAmount(_archerPrefap, 4);
                    bossRef.Roaring();
                }),
                new EventTimeStamp(10*60, () =>
                {
                    SpawnRateByAmount(_zombiePrefap, 15);
                    SpawnRateByAmount(_archerPrefap, 3);
                    bossRef.Roaring();
                }),
                new EventTimeStamp(12*60, () =>
                {
                    bossRef.SetPhase_2();
                    SpawnRateByAmount(_zombiePrefap, 13);
                    SpawnRateByAmount(_archerPrefap, 3);
                    bossRef.Roaring();
                }),
                new EventTimeStamp(13*60, () =>
                {
                    SpawnRateByAmount(_zombiePrefap, 13);
                    SpawnRateByAmount(_archerPrefap, 2);
                    bossRef.Roaring();
                }),
                new EventTimeStamp(14*60, () =>
                {
                    SpawnRateByAmount(_zombiePrefap, 10);
                    bossRef.Roaring();
                }),
                new EventTimeStamp(14*60 + 50, () =>
                {
                    SpawnRateByAmount(_zombiePrefap, 8);
                    bossRef.Roaring();
                }),
            });

        eventStepCount = eventTimeStamps.Count - 1;
        bossFightTimer.OnTick += t =>
        {
            GameUIManager.Instance.SetTimer(Convert.ToInt32(t));
            if(eventStepCount < 0)
                return;

            if(t <= eventTimeStamps[eventStepCount].time && !eventTimeStamps[eventStepCount].done)
            {
                eventTimeStamps[eventStepCount].action.Invoke();
                eventTimeStamps[eventStepCount--].done = true;
            }
        };
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.isBossDead = isBossDead;
    }

    public void SpawnEnemy(List<Transform> spawnPoints, GameObject pref)
    {
        Vector3 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)].position + MyUtils.RandomizeVector3() * spawnRadius;
        EntityPooling.Instance.GetOrInstantiateGameObject(pref, spawnPoint, Quaternion.Euler(0, Random.Range(0, 360f), 0), enemyContainer);
        Instantiate(_spawnVFX, spawnPoint, Quaternion.identity);
    }


    public void SpawnRateByAmount(GameObject enemyType, int amount)
    {
        List<Transform> farSpawnPoints = spawners.OrderByDescending(p => Vector3.Distance(playerRef.transform.position, p.position)).Take(5).ToList();

        IEnumerator<float> DelaySpawn(int amount)
        {
            for(int i = 0; i < amount; ++i)
            {
                SpawnEnemy(farSpawnPoints, enemyType);
                yield return Timing.WaitForSeconds(Random.Range(0.2f, 1f));
            }
        }

        Timing.RunCoroutine(DelaySpawn(amount));
    }

    public void OnPlayerEnterCombatZone()
    {
        if(!isInBattle)
        {
            OnPlayerEnter.Invoke();
            if(gameMode == GameMode.BOSS_FIGHT)
            {
                bossRef.gameObject.SetActive(true);
                bossFightTimer.Start();
            }
            else
            {
                endlessTimer.Start();
            }
            isInBattle = true;
        }
    }

    public void GameWin()
    {
        bossFightTimer.Stop();
        GameManager.Instance.SaveGame();
        GameUIManager.Instance.ShowWinningPanel();
    }

    public void GameLose()
    {
        bossFightTimer.Stop();
        GameManager.Instance.SaveGame();
        GameUIManager.Instance.ShowLosingPanel();
    }

    public void ClearEnemies()
    {
        foreach(var enemy in enemyContainer.GetComponentsInChildren<EntityComponent>())
        {
            // Destroy(enemy.gameObject);
            EntityPooling.Instance.AddToPool(enemy.gameObject);
        }
    }

    public void GameReset()
    {
        void ResetEnemyState()
        {
            bossRef.ResetStateBehaviour();
            bossRef.gameObject.SetActive(false);
            ClearEnemies();
        }

        void ResetEventState()
        {
            eventStepCount = eventTimeStamps.Count - 1;
            foreach(EventTimeStamp timeStamp in eventTimeStamps)
            {
                timeStamp.done = false;
            }
        }

        isInBattle = false;
        bossFightTimer.Reset();
        endlessTimer.Stop();
        endlessTimer.Reset();
        
        gameMode = isBossDead ? GameMode.ENDLESS : GameMode.BOSS_FIGHT;
        OnGameResetState.Invoke();

        GameUIManager.Instance.SetBossHealthBarVisibility(false);
        GameUIManager.Instance.ClearGameResultUI();
        
        ResetEnemyState();
        ResetEventState();
    }

    void OnDestroy()
    {
        Timing.KillCoroutines();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        spawners.ForEach(a =>
        {
            Gizmos.DrawWireSphere(a.position, spawnRadius);
        });
    }
}
