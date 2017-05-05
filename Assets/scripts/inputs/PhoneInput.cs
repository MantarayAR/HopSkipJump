using UnityEngine;
using System.Collections;

public class PhoneInput : MonoBehaviour
{
    private Matrix4x4 calibrationMatrix;
    private Vector3 wantedDeadzone = Vector3.zero;
    private double factor = 3;

    void Start()
    {
        Input.gyro.enabled = true;

        // Zero out the gyro scope
        wantedDeadzone = Input.gyro.userAcceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(
            new Vector3(0f, 0f, -1f),
            wantedDeadzone
        );
        Matrix4x4 matrix = Matrix4x4.TRS(
            Vector3.zero,
            rotateQuaternion,
            new Vector3(1f, 1f, 1f)
        );

        calibrationMatrix = matrix.inverse;
    }

    void OnDestroy()
    {
        Input.gyro.enabled = false;
    }

    // TODO detect large variants (instead of just transformed.z)
    // Determine the standard deviation and consider a jump
    // 3 standard deviations away
    void Update()
    {
        Quaternion deviceQuaternion = GyroToUnity(Input.gyro.attitude);
        Vector3 acceleration = this.calibrationMatrix.MultiplyVector(Input.gyro.userAcceleration);
        Vector3 transformed = AccelerationToUnity(Quaternion.Inverse(deviceQuaternion) * acceleration);

        double stdev = 0.4;

        if (transformed.z > factor * stdev)
        {
            // TODO the user has jumped
            Debug.Log(string.Format("Transformed acceleration {0}", transformed.z));
        }
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    private static Vector3 AccelerationToUnity(Vector3 v)
    {
        return new Vector3(v.x, -v.y, -v.z);
    }
}
