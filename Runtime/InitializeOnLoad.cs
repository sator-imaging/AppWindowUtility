using UnityEngine;


namespace SatorImaging.AppWindowUtility
{
    static class InitializeOnLoad
    {

#if UNITY_EDITOR
        //[UnityEditor.InitializeOnLoadMethod]
#endif
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Install()
        {

#if UNITY_STANDALONE_WIN
            AppWindowUtility.platform = new Windows();
#endif

        }


    }//class
}//namespace
