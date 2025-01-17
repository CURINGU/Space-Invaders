using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public GameObject cursorPrefab;
    private GameObject cursorInstance;

    void Start()
    {
        // Esconde o cursor do sistema
        SetCursorInvisible();

        cursorInstance = Instantiate(cursorPrefab);
    }

    void Update()
    {
        // Atualiza a posi��o da mira para seguir o cursor do mouse
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        cursorInstance.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);
    }

    public void SetCursorVisible()
    {
        Cursor.visible = true;
    }

    public void SetCursorInvisible()
    {
        Cursor.visible = false;
    }
}
