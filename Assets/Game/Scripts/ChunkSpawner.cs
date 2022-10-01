using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MSingleton<ChunkSpawner>
{
    public Transform container;
    public List<Chunk> chunkPrefabs;

    [Header("Chunk Generation")]
    public int maxChunkSize;
    public float chunkSpawnThreshold;

    private Stack<Chunk> currentChunks = new Stack<Chunk>();
    public float TriggerSpawnPosition => GetEndPoint().x - chunkSpawnThreshold;

    private Chunk LastChunk => currentChunks.Peek();

    private void Awake()
    {
        for (int i = 0; i < maxChunkSize; i++)
            Spawn();
    }

    public void Spawn()
    {
        if (currentChunks.Count == maxChunkSize)
            Remove();

        var chunk = Instantiate(chunkPrefabs[Random.Range(0, chunkPrefabs.Count)], container);
        chunk.transform.localPosition = GetEndPoint() + (Vector2.right * chunk.size.x / 2f);

        currentChunks.Push(chunk);
    }

    public void Remove()
    {
        if (currentChunks.Count == 0)
            return;

        var removedChunk = currentChunks.Pop();
        Destroy(removedChunk.gameObject);
    }

    private Vector2 GetEndPoint()
    {
        Vector2 lastChunkPos = Vector2.zero;

        if (currentChunks.Count != 0)
        {
            lastChunkPos = LastChunk.transform.position;
            lastChunkPos.x += (LastChunk.size.x / 2f);
        }


        return lastChunkPos;
    }

}
