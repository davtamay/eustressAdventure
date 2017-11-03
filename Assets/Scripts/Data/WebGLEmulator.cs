using UnityEngine;
using System;
using System.Collections.Generic;



/// Provides mouse-controlled head tracking emulation in the Unity editor.
public class WebGLEmulator : MonoBehaviour {
	private const string AXIS_MOUSE_X = "Mouse X";
	private const string AXIS_MOUSE_Y = "Mouse Y";


	// Simulated neck model.  Vector from the neck pivot point to the point between the eyes.
	private static readonly Vector3 m_neckOffset = new Vector3(0, 0.075f, 0.08f);

	// Use mouse to emulate head in the editor.
	// These variables must be static so that head pose is maintained between scene changes,
	// as it is on device.
	private static float m_mouseX = 0;
	private static float m_mouseY = 0;
	private static float m_mouseZ = 0;

	private bool m_isRecenterOnlyController = false;


	[Tooltip("Camera to track")]
	public Camera m_camera;

	private bool isPlaying = true;
	void Start()
	{
		
		if (m_camera == null)
		{
			m_camera = Camera.main;
		}
	}

	void Update()
	{

		if(Input.GetKeyDown(KeyCode.Escape))
			isPlaying = !isPlaying;

		Quaternion rot;
		bool rolled = false;
		//10/24/17



	if (isPlaying) {
	m_mouseX += Input.GetAxis(AXIS_MOUSE_X) * 5;
	if (m_mouseX <= -180) {
	m_mouseX += 360;
	} else if (m_mouseX > 180) {
	m_mouseX -= 360;
	}
	m_mouseY -= Input.GetAxis(AXIS_MOUSE_Y) * 2.4f;
	m_mouseY = Mathf.Clamp(m_mouseY, -85, 85);
	} 



	else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) {
			rolled = true;
			m_mouseZ += Input.GetAxis(AXIS_MOUSE_X) * 5;
			m_mouseZ = Mathf.Clamp(m_mouseZ, -85, 85);
		}
		if (!rolled) {
			// People don't usually leave their heads tilted to one side for long.
			m_mouseZ = Mathf.Lerp(m_mouseZ, 0, Time.deltaTime / (Time.deltaTime + 0.1f));
		}
		rot = Quaternion.Euler(m_mouseY, m_mouseX, m_mouseZ);
		var neck = (rot * m_neckOffset - m_neckOffset.y * Vector3.up) * m_camera.transform.lossyScale.y;

		Vector3 camPosition = m_camera.transform.position;
		camPosition.y = neck.y;
		m_camera.transform.localPosition = neck;
		m_camera.transform.localRotation = rot;
	}



	public void Recenter()
	{
		
		if (m_isRecenterOnlyController)
		{
		return;
		}
		m_mouseX = m_mouseZ = 0;  // Do not reset pitch, which is how it works on the phone.
		m_camera.transform.localPosition = Vector3.zero;
		m_camera.transform.localRotation = new Quaternion(m_mouseX, m_mouseY, m_mouseZ, 1);
	
	}
}
