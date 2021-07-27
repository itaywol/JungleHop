using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public struct PlatformWithStats
{
    public GameObject platformPrefab;
    [Range( 1f, 3f )]
    public float scoreMultiplier;

}

public class PlatformController : MonoBehaviour
{
    private List<GameObject> _spawnedPlatforms = new List<GameObject>();
    private float _platformToughness = 1f;
    private bool _initalSpawned = false;
    private Vector2 _minBoundary, _maxBoundary;
    private GameObject[] _players;

    public GameObject PlatformHolder;
    [Range( 2, 10 )]
    public int _maxPlatformsAmount = 5;
    public PlatformWithStats[] platformsAndRarity;
    public float platformSpawnYOffset = 1f;

    

    private void Awake()
    {
        _players = GameObject.FindGameObjectsWithTag( "Player" );
        if (_players.Length <= 0) throw new Exception( "No Players found" );
        
    }

    private void Update()
    {
        foreach(GameObject platform in _spawnedPlatforms)
        {
            if(platform==null)
            {
                _spawnedPlatforms.Remove( platform );
            }
        }
    }

    private void setBoundaries()
    {
        _minBoundary = getGroupBounds( _players ).Item1;
    }

    private Tuple<Vector2, Vector2> getGroupBounds(GameObject[] gameObjects)
    {
        float minX, maxX, minY, maxY;
        minX = maxX = minY = maxY = 0;
        foreach(GameObject obj in gameObjects)
        {
            minX = Mathf.Min( minX, obj.transform.position.x );
            maxX = Mathf.Max( maxX, obj.transform.position.x );
            minY = Mathf.Min( minY, obj.transform.position.x );
            maxY = Mathf.Max( maxY, obj.transform.position.x );
        }

        return new Tuple<Vector2, Vector2>( new Vector2( minX, minY ), new Vector2( maxX, maxY ) );
    }

    public void spawnInitialPlatforms()
    {
        if (!_initalSpawned) return;

        int PLATFORMS_AVAILABLE = this.platformsAndRarity.Length;
        for (int i = 0; i <= 2; i++)
        {
            GameObject platformToSpawn = platformsAndRarity[Random.Range( 0, PLATFORMS_AVAILABLE - 1 )].platformPrefab;
            _spawnedPlatforms.Add( Instantiate( platformToSpawn ) );
        }
    }

    public void increaseToughness( float amount )
    {
        if (amount < 0) throw new System.Exception( "increaseToughness arg:amount should be above 0" );
        this._platformToughness = Mathf.Clamp( this._platformToughness - amount, 0.0001f, 1f );
    }

    private Vector3 randomLocation( float minX, float maxX, float minY, float maxY, float z = 0 )
    {
        float _xMinBoundary = Camera.main.transform.position.x - minX;
        float _xMaxBoundary = Camera.main.transform.position.x + maxX;
        float _yMinBoundary = minY;
        float _yMaxBoundary = maxY;

        return new Vector3( Random.Range( _xMinBoundary, _xMaxBoundary ), Random.Range( _yMinBoundary, _yMaxBoundary ), z );
    }
}
