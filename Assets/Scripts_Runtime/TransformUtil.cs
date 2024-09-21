using UnityEngine;

public static class TransformUtil {

    public static Vector3 TransformDirection(Quaternion r, Vector3 v) {
        return r * v;
    }

}