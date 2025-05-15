using System.Collections;
using System.Collections.Generic;
using Naninovel;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private List<TileSO> _tilesData;
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private GameObject _tileSpawnGrid;
    [SerializeField] private AudioSource _pairAudioSource;

    private Tile _firstSelected;
    private Tile _secondSelected;

    private int _pairsFound;
    private int _totalPairs;

    private bool _canClick = true;

    public void Start()
    {
        Init();
    }

    private void Init()
    {
        List<TileSO> tilePool = new List<TileSO>();
        foreach (var tile in _tilesData)
        {
            tilePool.Add(tile);
            tilePool.Add(tile);
        }

        _totalPairs = tilePool.Count / 2;

        for (int i = 0; i < tilePool.Count; i++)
        {
            TileSO temp = tilePool[i];
            int randomIndex = Random.Range(i, tilePool.Count);
            tilePool[i] = tilePool[randomIndex];
            tilePool[randomIndex] = temp;
        }

        for (int i = 0; i < tilePool.Count; i++)
        {
            GameObject go = Instantiate(_tilePrefab, _tileSpawnGrid.transform);
            Tile tile = go.GetComponent<Tile>();
            tile.SetTileData(tilePool[i], this);
        }
    }

    public void OnTileClicked(Tile clickedTile)
    {
        if (!_canClick || clickedTile.IsRevealed())
            return;

        clickedTile.Reveal();

        if (_firstSelected == null)
        {
            _firstSelected = clickedTile;
        }
        else if (_secondSelected == null)
        {
            _secondSelected = clickedTile;
            _canClick = false;

            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(1f);

        if (_firstSelected.GetTileId() == _secondSelected.GetTileId())
        {
            _pairsFound++;
            _pairAudioSource.Play();
            if (_pairsFound == _totalPairs)
            {
                LoadAndPlay();
            }
        }
        else
        {
            _firstSelected.Hide();
            _secondSelected.Hide();
        }

        _firstSelected = null;
        _secondSelected = null;
        _canClick = true;
    }

    private void LoadAndPlay()
    {
        var player = Engine.GetService<IScriptPlayer>();
        player.PlayFromLabel("GameWon");
    }
}

