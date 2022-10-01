using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MSingleton<ChunkSpawner>, IGameEventsHandler
{
    public Transform container;
    public Chunk startChunk;
    public List<Chunk> chunkPrefabs;

    [Header("Chunk Generation")]
    public int maxChunkSize;
    public float chunkSpawnThreshold;

    private Queue<Chunk> currentChunks = new Queue<Chunk>();
    public float TriggerSpawnPosition => GetEndPoint(globalCoords: true).x - chunkSpawnThreshold;

    private Chunk lastChunk;
    private bool spawningFirstChunk;

    private void Awake()
    {
        SubscribeGameEvents();
    }

    private void InitSpawner()
    {
        Debug.Log("Spawner Initialized");

        for (int i = 0; i < currentChunks.Count; i++)
            Remove();

        lastChunk = null;

        spawningFirstChunk = true;

        for (int i = 0; i < maxChunkSize; i++)
            Spawn();
    }

    public void Spawn()
    {
        if (currentChunks.Count == maxChunkSize)
            Remove();

        var chunkPrefab = chunkPrefabs[Random.Range(0, chunkPrefabs.Count)];

        if (spawningFirstChunk)
        {
            chunkPrefab = startChunk;
            spawningFirstChunk = false;
        }

        var chunk = Instantiate(chunkPrefab, container);
        chunk.transform.localPosition = GetEndPoint(globalCoords: false) + (Vector2.right * chunk.size.x / 2f);

        currentChunks.Enqueue(chunk);
        lastChunk = chunk;
    }

    public void Remove()
    {
        if (currentChunks.Count > 0)
        {
            var removedChunk = currentChunks.Dequeue();
            removedChunk.Dispose();

            if (currentChunks.Count == 1)
                lastChunk = currentChunks.Peek();
        }
    }

    private Vector2 GetEndPoint(bool globalCoords = false)
    {
        Vector2 lastChunkPos = Vector2.zero;

        if (lastChunk != null)
        {

            lastChunkPos = globalCoords ? lastChunk.transform.position : lastChunk.transform.localPosition;
            lastChunkPos.x += (lastChunk.size.x / 2f);
        }

        return lastChunkPos;
    }

    public void SubscribeGameEvents()
    {
        GameEvents.OnGameLoad += OnGameLoad;
        GameEvents.OnGameStarted += OnGameStarted;
        GameEvents.OnGameFailed += OnGameFailed;
        GameEvents.OnGameRecovered += OnGameRecovered;
    }

    public void OnGameLoad()
    {
        InitSpawner();
    }

    public void OnGameStarted()
    {
        Player.Instance.controller.OnReachedChunkSpawnPoint += Spawn;
    }

    public void OnGameFailed()
    {
        Player.Instance.controller.OnReachedChunkSpawnPoint -= Spawn;
    }

    public void OnGameRecovered()
    {
        throw new System.NotImplementedException();
    }
}
