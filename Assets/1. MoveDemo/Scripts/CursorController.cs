using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
  
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public Texture2D defaultCursor;
    public Texture2D telekinesisCursor;
    public Texture2D cursorInfo;
    public int size = 50; // размер курсора по ширине и высоте
    public enum ProjectMode { Project3D = 0, Project2D = 1 };
    public ProjectMode mode = ProjectMode.Project3D;
    private Vector2 offset;
    private Texture2D cursor;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = false;
        //Cursor.SetCursor(defaultCursor, hotSpot, cursorMode);
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enviroment"))
            {
                offset = new Vector2(-size / 2, -size / 2);
                cursor = telekinesisCursor;
            }
            else
            {
                offset = new Vector2(-size / 2, -size / 2);
                cursor = defaultCursor;
            }
        }
    }

    void OnGUI()
    {
        Vector2 mousePos = Event.current.mousePosition;
        GUI.depth = 999; // поверх остальных элементов
        GUI.Label(new Rect(mousePos.x + offset.x, mousePos.y + offset.y, size, size), cursor);
    }

}
