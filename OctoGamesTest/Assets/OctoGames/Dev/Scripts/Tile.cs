using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Sprite _backSprite;
    
    private Image _tileImage;
    private int _tileId;
    private Sprite _frontSprite;
    private bool _isRevealed;
    private TileManager _tileManager;
    private AudioSource _audioSource;

    private void Awake()
    {
        _tileImage = GetComponent<Image>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetTileData(TileSO tileData, TileManager manager)
    {
        _tileId = tileData.tileId;
        _frontSprite = tileData.tileSprite;
        _tileManager = manager;
        Hide();
    }

    public int GetTileId()
    {
        return _tileId;
    }

    public void Reveal()
    {
        _isRevealed = true;
        _tileImage.sprite = _frontSprite;
    }

    public void Hide()
    {
        _isRevealed = false;
        _tileImage.sprite = _backSprite;
    }

    public bool IsRevealed()
    {
        return _isRevealed;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_isRevealed)
        {
            _tileManager.OnTileClicked(this);
            _audioSource.Play();
        }
    }
}

