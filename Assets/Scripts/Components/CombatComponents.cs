using Unity.Entities;

namespace VampireDynasty
{
    /// <summary>
    /// <para>摧毁倒计时，0后摧毁</para>
    /// <para>用法：烘焙在飞行物、特效等需要延迟销毁的物体上</para>
    /// <para>
    /// 由<see cref="DestroyOnTimerSystem"/>系统计算时间，倒计时结束后给Entity添加DestroyEntityTag标签
    /// </para>
    /// </summary>
    public struct DestroyTimer : IComponentData
    {
        public float Value;
    }

    /// <summary>
    /// <para>标记需要被摧毁的物体</para>
    /// <para>
    /// 用法：
    /// 1. 被<see cref="DestroyOnTimerSystem"/>系统添加到需要摧毁的物体上
    /// 2. 直接添加到被击杀的玩家、怪物Entity上
    /// </para>
    /// <para>
    /// <see cref="DestroyEntitySystem"/>系统捕获该标签，销毁Entity
    /// 1. 武器特效：直接销毁
    /// </para>
    /// </summary>
    public struct DestroyEntityTag : IComponentData { }
    
    public struct MaxHealth : IComponentData
    {
        public int Value;
    }
    
    public struct CurrentHealth : IComponentData
    {
        public int Value;
    }
    
    public struct DamageBufferElement : IBufferElementData
    {
        public int Value;
    }
}