using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerCanoncito : MonoBehaviour
{
    public GameObject ProgressBar;
    LineRenderer _trajectory;
    const float MINX = -8f, MAXX = 0;
    Vector3 _deltaPos, _mousePosition;
    float _speedX = 20f, _barValue;
    float _triggerSpeed = 30f, _triggerAngle;
    public GameObject CannonBallPrefab;
    const int _trajectoryVertices = 20;

    private void Awake()
    {
        _trajectory = GetComponent<LineRenderer>();
    }

    void Start()
    {
        _deltaPos = new Vector3();
        _trajectory.positionCount = _trajectoryVertices;
    }

    void Update()
    {
        // Position changing:
        _deltaPos.y = _deltaPos.z = 0;
        _deltaPos.x = Input.GetAxis("Horizontal") * _speedX * Time.deltaTime;
        //gameObject.transform.Translate(_deltaPos);
        /*gameObject.transform.position = new Vector3(
            Mathf.Clamp(gameObject.transform.position.x, MINX, MAXX),
            gameObject.transform.position.y,
            gameObject.transform.position.z);*/
        if(Input.GetAxis("Horizontal") != 0)
            //GetComponent<Rigidbody>().velocity = new Vector3(Input.GetAxis("Horizontal") * _speedX * Time.deltaTime, 0);
            // Otra forma:
            GetComponent<Rigidbody>().AddForce(new Vector3(Input.GetAxis("Horizontal") * 500f * Time.deltaTime, 0));
        if (Input.GetButtonDown("Jump"))
            //GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 15f);
            // Otra forma:
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 500f));


        // Calculating angle:
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _deltaPos.y = _mousePosition.y - gameObject.transform.position.y;
        _deltaPos.x = _mousePosition.x - gameObject.transform.position.x;

        if (_deltaPos.x < 0)
            _triggerAngle = Mathf.PI / 2;
        else if (_deltaPos.y < 0)
            _triggerAngle = 0;
        else
            _triggerAngle = Mathf.Atan(_deltaPos.y / _deltaPos.x);// * Mathf.Rad2Deg;

        if (Input.GetButton("Fire1"))
        {
            _barValue = Mathf.PingPong(Time.time, 1) * 100f;
            ProgressBar.GetComponent<ProgressBar>().BarValue = _barValue;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            Instantiate(CannonBallPrefab, gameObject.transform.position, Quaternion.identity).GetComponent<CannonBallBehaviour>().Shoot(_triggerSpeed * (_barValue / 100), _triggerAngle);
        }

        for(int i = 0; i < _trajectoryVertices; i++)
        {
            _trajectory.SetPosition(i, GetPosition((float)i/_trajectoryVertices, Mathf.Pow(_triggerSpeed, 2) * Mathf.Sin(_triggerAngle * 2) / Mathf.Abs(Physics.gravity.y)));
        }
    }

    /// <summary>
    /// Obtained from: https://en.wikipedia.org/wiki/Projectile_motion
    /// </summary>
    Vector3 GetPosition(float resolutionProportion, float xMax)
    {
        float xRelative = resolutionProportion * xMax;
        float yRelative = xRelative * Mathf.Tan(_triggerAngle) -
            (Mathf.Abs(Physics.gravity.y) * Mathf.Pow(xRelative, 2)) / 
            (2 * (_triggerSpeed * (_barValue / 100)) * (_triggerSpeed * (_barValue / 100)) * Mathf.Cos(Mathf.Pow(_triggerAngle, 2)));

        return new Vector3(xRelative, yRelative);
    }
}
