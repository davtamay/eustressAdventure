using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Timer", menuName = "CustomSO/Utility/TimerClass")]
public class TimerClass : ScriptableObject
{
	public bool isTimerRunning= false;
	private float timeElapsed= 0.0f;
	[SerializeField]private float currentTime= 0.0f;
	//INSERT Time Left Here To Show TEXT FORMAT
	public float timeToDisplay;
	private float lastTime= 0.0f;	
	[SerializeField]private float timeScaleFactor= 1.1f; // <-- If you need to scale time, change this!
	
	private string timeString;
	private string hour;
	private string minutes;
	private string seconds;
	private string mills;
	
	private int aHour;
	private int aMinute;
	private int aSecond;
	private int aMillis;
	private int tmp;
	private int aTime;
	
	private GameObject callback;

//	public void OnDisable(){
//
//		StopTimer ();
//		//ResetTimer ();
//		isTimerRunning = false;
//	//	timeToDisplay = 0;
//
//
//	}
//	void OnEnable(){
//
//		StopTimer ();
//		//ResetTimer ();
//
//	}
	public void UpdateTimer ()
	{
		// calculate the time elapsed since the last Update()
		timeElapsed=Mathf.Abs(Time.realtimeSinceStartup-lastTime);
	   	
		// if the timer is running, we add the time elapsed to the current time (advancing the timer)
		if(isTimerRunning)
		{
			currentTime+=(timeElapsed*timeScaleFactor)* Time.timeScale;
			timeToDisplay -= (timeElapsed * timeScaleFactor)* Time.timeScale;
	    }
		
		// store the current time so that we can use it on the next update
		lastTime=Time.realtimeSinceStartup;
	}
	
	public void StartTimer ()
	{
		// set up initial variables to start the timer
		isTimerRunning=true;
	    lastTime=Time.realtimeSinceStartup;

		//NEW
		UpdateTimer ();
	}
	    
	public void StopTimer ()
	{
		// stop the timer
		isTimerRunning=false;
		
		// carry out an update to the timer
		UpdateTimer();
	}
	    
	public void ResetTimer ()
	{
		// resetTimer will set the timer back to zero
		timeToDisplay = 0.0f;
	    timeElapsed=0.0f;
	    lastTime=0.0f;
	    currentTime=0.0f;
	    lastTime=Time.realtimeSinceStartup;
		
		// carry out an update to the timer
		UpdateTimer();
	}
	
	public string GetFormattedTime ()
	{	
		// carry out an update to the timer so it is 'up to date'
		UpdateTimer();
		
		// grab hours
		aHour = (int)timeToDisplay/3600;
		aHour=aHour%24;
		
		// grab minutes
		aMinute=(int)timeToDisplay/60;
		aMinute=aMinute%60;
		
	    // grab seconds
		aSecond=(int)timeToDisplay%60;
	        
	    // grab milliseconds
		aMillis=(int)(timeToDisplay*100)%100;
	        
	    // format strings for individual mm/ss/mills
		tmp=(int)aSecond;
	    seconds=tmp.ToString();
	    if(seconds.Length<2)
	            seconds="0"+seconds;
			
		tmp=(int)aMinute;
        minutes=tmp.ToString();
        if(minutes.Length<2)
            minutes="0"+minutes;
			
		tmp=(int)aHour;
		hour=tmp.ToString();
		if(hour.Length<2)
            hour="0"+hour;
		
		tmp=(int)aMillis;
        mills=tmp.ToString();
        if(mills.Length<2)
            mills="0"+mills;
		
		// pull together a formatted string to return
		timeString=minutes+":"+seconds+":"+mills;

	    return timeString;
	}
	
	public float GetTime ()
	{
		// remember to call UpdateTimer() before trying to use this function, otherwise the time value will
		// not be up to date
		//NEW
		UpdateTimer ();
		return (currentTime);
	}

	public void SetTimeToDisplay(float time){

		timeToDisplay = time;
	}
}