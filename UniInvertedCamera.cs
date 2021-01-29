using System.Collections;
using UnityEngine;

[RequireComponent (typeof (Camera))]
public class UniInvertedCamera : MonoBehaviour {
    [SerializeField]
    Camera cam;
    [SerializeField]
    Vector3 scale = new Vector3 (-1, 1, 1);
    public void CameraReverse () {
        StartCoroutine (SetCoroutine ());
    }
    private void OnPreCull () {
        if (cam == null) return;

        cam.ResetWorldToCameraMatrix ();
        cam.ResetProjectionMatrix ();
        cam.projectionMatrix = cam.projectionMatrix * Matrix4x4.Scale (scale);
    }
    void OnPreRender () {
        if (cam != null)
            GL.invertCulling = true;
    }

    void OnPostRender () {
        if (cam != null)
            GL.invertCulling = false;
    }
    IEnumerator SetCoroutine () {
        if (cam == null) {
            scale = new Vector3 (-1, 1, 1);
            yield return new WaitForEndOfFrame ();
            cam = GetComponent<Camera> ();
        } else {
            scale = new Vector3 (1, 1, 1);
            yield return new WaitForEndOfFrame ();
            cam = null;
        }
    }
}
