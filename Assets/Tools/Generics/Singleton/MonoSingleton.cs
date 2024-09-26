using UnityEngine;

/// <summary>
/// 继承自这个类的单例同样也是MonoBehaviour，可以挂载到物体上，并且会将该物体放置DontDestroyOnLoad内
/// 如果项目本身就使用多场景控制物件，可以不用调用DontDestroyOnLoad方法
/// </summary>
/// <typeparam name="T"></typeparam>
public class MonoSingleton<T> : MonoBehaviour where T : Component
{
    private static T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name, new[] {typeof(T)});
                    DontDestroyOnLoad(obj);
                    _instance = obj.GetComponent<T>();
                    (_instance as IInitialize)?.Init();
                }
                else
                {
                    Debug.LogWarning($"typeof: {typeof(T)} Instance is already exist!");
                }
            }

            return _instance;
        }
    }

    /// <summary>
    /// 继承Mono单例的类如果写了Awake方法，需要在Awake方法最开始的地方调用一次base.Awake()，来给_instance赋值
    /// </summary>
    protected virtual void Awake()
    {
        _instance = this as T;
        DontDestroyOnLoad(this.transform.root);
    }
}