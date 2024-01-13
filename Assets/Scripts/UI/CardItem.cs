using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class CardItem : MonoBehaviour
{
    Transform UICharacter;

    public GameObject playerPrefab;
    private GameObject currentGameObject;

    private void Awake() {
        UICharacter = transform.Find("Character");
    }

    private void Start() {
        FillImage();
    }

    private void FillImage()
    {
        Sprite PrefabSprite = playerPrefab.GetComponent<SpriteRenderer>().sprite;
        UICharacter.GetComponent<Image>().sprite = PrefabSprite;
    }

    //鼠标开始拖拽
    public void OnBeginDrag(BaseEventData data)
    {
        PointerEventData pointerEventData = data as PointerEventData;
        currentGameObject = Instantiate(playerPrefab);
        Vector2 pointScreenPosition = GameManager.Instance.Camera.GetComponent<Camera>().ScreenToWorldPoint(pointerEventData.position);
        currentGameObject.transform.position = new Vector3(pointScreenPosition.x, pointScreenPosition.y, 0);

    }

    //鼠标拖拽过程
    public void OnDrag(BaseEventData data)
    {
        if (currentGameObject == null)
        {
            return;
        }
        PointerEventData pointerEventData = data as PointerEventData; 
        Vector2 pointScreenPosition = GameManager.Instance.Camera.GetComponent<Camera>().ScreenToWorldPoint(pointerEventData.position);
        currentGameObject.transform.position = new Vector3(pointScreenPosition.x, pointScreenPosition.y, 0);
    }

    //鼠标拖拽结束
    public void OnEndDrag(BaseEventData data)
    {
        Level currentLevel = GameManager.Instance.GetCurrentLevel();
        Grid grid = currentLevel.instantiateLevel.grid;

        PointerEventData pointerEventData = data as PointerEventData; 
        Vector3 pointWorldPosition = GameManager.Instance.Camera.GetComponent<Camera>().ScreenToWorldPoint(pointerEventData.position);
        Vector3Int pointGridPosition = grid.WorldToCell(pointWorldPosition);

        TileBase tile = currentLevel.instantiateLevel.Front_Tilemap.GetTile(pointGridPosition);
        if (tile == GameResources.Instance.PlayerPlatformTile)
        {
            Vector2 offset = grid.cellSize/2;
            Vector2 tilePosition = grid.CellToWorld(pointGridPosition);
            currentGameObject.transform.position = tilePosition + offset;
            currentGameObject.transform.SetParent(currentLevel.instantiateLevel.Front_Tilemap.transform);

            currentLevel.instantiateLevel.Front_Tilemap.SetTile(pointGridPosition, GameResources.Instance.HasPlayerInPlatformTile);
        }
        else
        {
            Destroy(currentGameObject);
            currentGameObject = null;
        }
    }
}
