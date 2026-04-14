using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PositioningTools: EditorWindow
{

    static GameObject _cursor;


    // 
    //Cursor Creation
    //

    [MenuItem("Tools/PositioningTools/Create 3D Cursor", false)]
    static void CreateCursor(){
            _cursor = new GameObject("3D Cursor");
            _cursor.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.NotEditable;
    }

    [MenuItem("Tools/PositioningTools/Create 3D Cursor", true)]
    static bool ValidateCreateCursor()
    {
        if(_cursor == null)
        {
            return true;
        }

        return false;
    }

    [MenuItem("Tools/PositioningTools/Reset 3D Cursor", false)]
    static void ResetCursor(){
        if(_cursor != null)
            {
                _cursor.transform.position = Vector3.zero;
            }

    }

    [MenuItem("Tools/PositioningTools/Reset 3D Cursor", true)]
    static bool ValidateResetCursor()
    {
        if(_cursor != null)
        {
            return true;
        }
        return false;
    }



     [MenuItem("Tools/PositioningTools/Remove 3D Cursor", false)]
    static void RemoveCursor(){
        if(_cursor != null)
        {
            DestroyImmediate(_cursor);
        }

    }


     [MenuItem("Tools/PositioningTools/Remove 3D Cursor", true)]
     static bool ValidateRemoveCursor()
    {
        if(_cursor != null)
        {
            return true;
        }
        return false;
    }





    //
    //Cursor Functions
    //
    [MenuItem("CONTEXT/GameObjectToolContext/Positioning Tools/Move to Cursor", priority = -5)]
    [MenuItem("GameObject/Positioning Tools/Move to Cursor", priority = -5)]
    static void MoveToCursor(){
        if(_cursor != null)
            {
                
                Undo.RegisterCompleteObjectUndo(Selection.activeTransform, "Move to Cursor");
                Selection.activeTransform.position = _cursor.transform.position;
            }


    }
    [MenuItem("CONTEXT/GameObjectToolContext/Positioning Tools/Move Cursor to Object", priority = -5)]
    [MenuItem("GameObject/Positioning Tools/Move Cursor to Object", priority = -4)]
    static void MoveCursorTo(){
      if(_cursor != null)
            {
                Undo.RegisterCompleteObjectUndo(_cursor.transform, "Move Cursor to Object");
                _cursor.transform.position = Selection.activeTransform.position;
            }

    }



    



    [InitializeOnLoadMethod]
    static void Initialize()
    {
        SceneView.duringSceneGui += DrawCursorGizmo;

        _cursor = GameObject.Find("3D Cursor");
        if(_cursor != null)
        {
            _cursor.hideFlags = HideFlags.HideInHierarchy | HideFlags.NotEditable | HideFlags.HideInInspector;
        }
    
    }

    static void DrawCursorGizmo(SceneView sceneView)
    {
        if (_cursor == null)
        {
            // Try to find existing cursor
            _cursor = GameObject.Find("3D Cursor");
            return;
        }
        
        Handles.color = Color.green;
        Handles.DrawWireDisc(_cursor.transform.position, Vector3.up, 0.5f);
        Handles.DrawWireDisc(_cursor.transform.position, Vector3.right, 0.5f);
        Handles.DrawWireDisc(_cursor.transform.position, Vector3.forward, 0.5f);

        sceneView.Repaint();

    }

}
