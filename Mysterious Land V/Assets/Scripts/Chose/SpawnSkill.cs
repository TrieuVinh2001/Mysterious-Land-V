using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSkill : MonoBehaviour
{
    [SerializeField] private SelectedCharacter selectedChar;
    [SerializeField] private LayerMask layerArea;

    private bool isDragging = false;
    private Vector3 startPosition;
    private float timeClick;
    

    private void Update()
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
                if (!selectedChar.isSkill)
                    return;
                else
                {
                    SpawnSkillOnArea();
                }
            }
        }
    }

    private void SpawnSkillOnArea()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, 0, layerArea);//xác định điểm nhấn dựa vào raycast

        if (hit.collider != null/* && hit.collider.TryGetComponent<AreaSpawn>(out AreaSpawn area)*/)
        {
            if(hit.collider.TryGetComponent<AreaSpawn>(out AreaSpawn area))
            {
                Vector3 pos = new Vector3(mousePos2D.x, area.posSpawn.y, 0);
                GameObject skill = Instantiate(selectedChar.skillPrefab, pos, Quaternion.identity);
                skill.GetComponent<SkillBase>().posTarget = pos;
                skill.transform.parent = area.transform; //đưa gameobject làm con của gameobject areaParent

                GetComponent<CardSkillClick>().CoolDown();
                selectedChar.isSkill = false;
            }
            //else if (hit.collider.TryGetComponent<CharacterBase>(out CharacterBase charBase))
            //{
            //    AreaSpawn areaSpawn = charBase.gameObject.transform.parent.GetComponent<AreaSpawn>();
            //    Vector3 pos = new Vector3(mousePos2D.x, areaSpawn.posSpawn.y, 0);
            //    GameObject skill = Instantiate(selectedChar.skillPrefab, pos, Quaternion.identity);
            //    skill.GetComponent<SkillBase>().posTarget = pos;
            //    skill.transform.parent = areaSpawn.transform; //đưa gameobject làm con của gameobject areaParent

            //    GetComponent<CardSkillClick>().CoolDown();
            //    selectedChar.isSkill = false;
            //}

        }
        

    }
}
