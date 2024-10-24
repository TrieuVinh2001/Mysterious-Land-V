using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacter : MonoBehaviour
{
    private SelectedCharacter selectedChar;
    [SerializeField] private LayerMask layerArea;

    private bool isDragging = false;
    private Vector3 startPosition;
    private float timeClick;

    private void Start()
    {
        selectedChar = GetComponent<SelectedCharacter>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
            isDragging = false;
        }

        if (Input.GetMouseButton(0))
        {
            timeClick += Time.deltaTime;

            if (Vector3.Distance(startPosition, Input.mousePosition) > 5 || timeClick > 2f)
            {
                isDragging = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            timeClick = 0;

            if (!isDragging)
            {
                ClickArea();
            }
        }
    }

    private void ClickArea()
    {
        if (GameManager.instance.countCharacter < GameManager.instance.maxCountCharacter)//khi nhấn vào khu vực thả quân
        {
            if(selectedChar.prefab != null)
            {
                SpawnChar();
            }
        }
    }

    private void SpawnChar()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, 0, layerArea);//xác định điểm nhấn dựa vào raycast

        if (hit.collider != null && hit.collider.TryGetComponent<AreaSpawn>(out AreaSpawn area))
        {
            int coinCharacter = selectedChar.prefab.GetComponent<CharacterBase>().GetCharacterSO().coin;
            if (coinCharacter > GameManager.instance.coin)
                return;

            Spawn(selectedChar.prefab, area.posSpawn, area.gameObject, coinCharacter);//Tạo quân dựa vào thứ tự trong list, vị trí, gameobject cha
            
            for(int i = 0; i<selectedChar.cards.Length; i++)
            {
                if(selectedChar.prefab == selectedChar.cards[i].GetComponent<CardClick>().prefab)
                {
                    selectedChar.cards[i].GetComponent<CardClick>().CoolDown();
                }
            }

            selectedChar.prefab = null;
            StartCoroutine(BackGroundArea(area.gameObject.transform.GetChild(0).gameObject));
        }
    }

    private void Spawn(GameObject prefab, Vector2 posSpawn, GameObject areaParent, int coinCharacter)
    {
        GameObject newChar = Instantiate(prefab, new Vector3(posSpawn.x, posSpawn.y + Random.Range(-0.3f, 0.3f), Random.Range(-9f, 0f)) , Quaternion.identity);
        newChar.transform.parent = areaParent.transform; //đưa gameobject làm con của gameobject areaParent 

        GameManager.instance.ChangeCoin(-coinCharacter);
        GameManager.instance.ChangeCountCharacter(1);
    }

    IEnumerator BackGroundArea(GameObject area)
    {
        area.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        area.SetActive(false);
    }
}
