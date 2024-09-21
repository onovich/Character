using UnityEngine;

public class TestEntity : MonoBehaviour {

    public Vector3 v = new Vector3(1, 0, 0);

    void Start() {
        var s1 = transform.TransformDirection(v);
        var s2 = transform.rotation * v;
        Debug.Log("s1 = " + s1 + " s2 = " + s2);
    }

}