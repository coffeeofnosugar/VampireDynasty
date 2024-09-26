/// <summary>
/// 继承自这个类的单例不是MonoBehaviour，无法挂载到物体上
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> where T : new()
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
                (_instance as IInitialize)?.Init();
            }

            return _instance;
        }
    }
}