using UnityEngine;
using System.Collections;
using System.Threading;
using System;
 class TimeControl:MonoBehaviour {
    
    public  void HowTimeDoOnce( Action function, float time, bool IsCycle/*, int systemtime*/)
    {
        //if (systemtime == 0)
        //    StartCoroutine(Systemtime(systemtime));
        //if (systemtime == 5)


        StartCoroutine(WaitForTime( function, time, IsCycle));
    }

    IEnumerator WaitForTime( Action action, float waitTime, bool IsCycle)
    {
        if (IsCycle)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);action();
            }
        }
        else
        {
            yield return new WaitForSeconds(waitTime);action();
        }
    }

   // IEnumerator Systemtime( int systemtime)
   //{
   //    while (systemtime < 20)
   //    {
   //        yield return new WaitForSeconds(Time.deltaTime);
   //        systemtime++;
   //    }    
       
   //}
	
}
