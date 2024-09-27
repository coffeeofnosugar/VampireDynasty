using Unity.Entities;

namespace VampireDynasty
{
    /// <summary>
    /// 摧毁倒计时，0后摧毁
    /// </summary>
    public struct DestroyTimer : IComponentData
    {
        public float Value;
    }

    public struct DestroyEntityTag : IComponentData { }
}