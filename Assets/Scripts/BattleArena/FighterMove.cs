using System.Collections;
using UnityEngine;

public class FighterMove : MonoBehaviour
{
    private float _rotateSpeed = 180f;
    private float _moveSpeed = 10f;

    public Coroutine StartMove(Transform target, float stoppingDistance = 0)
    {
        return StartCoroutine(MoveTo(target, stoppingDistance));
    }

    private IEnumerator MoveTo(Transform target, float stoppingDistance = 0)
    {
        while (Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            var distance = Vector2.Distance(transform.position, target.position);
            var distance2 = _moveSpeed * Time.deltaTime;

            transform.position = Vector2.Lerp(transform.position, target.position, distance2 / distance);

            //Vector2 direction = (target.position - transform.position).normalized;
            //transform.Translate(direction * _moveSpeed * Time.deltaTime);
            // 
            yield return null;
        }
    }

    public Coroutine StartLookAtRotation(Transform target)
    {
        Vector2 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector2.down);
        return StartCoroutine(RotateTo(rotation));
    }

    public Coroutine StartRotation(Quaternion targetRotation)
    {
        return StartCoroutine(RotateTo(targetRotation));
    }

    public IEnumerator RotateTo(Quaternion targetRotation)
    {
        while (!QuaternionApproximately(transform.rotation, targetRotation, 0.01f))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private bool QuaternionApproximately(Quaternion a, Quaternion b, float tolerance = 0.01f)
    {
        return Quaternion.Angle(a, b) < tolerance;
    }
}
