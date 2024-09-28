using Unity.Entities;

namespace VampireDynasty
{
    public struct Random : IComponentData
    {
        public Unity.Mathematics.Random Value;
    }
    
    /// <summary>
    /// <para>游戏难度，随着游戏进度增加。烘焙在GamePlay上进行设置</para>
    /// </summary>
    public struct DegreeOfDifficulty : IComponentData
    {
        public int Value;
    }

    /// <summary>
    /// <para>游戏的各种设置</para>
    /// </summary>
    public struct GamePlayProperties : IComponentData
    {
        public float SpawnInterval;     // 怪物生成间隔
        public float SpawnWide;         // 怪物生成x轴范围，需设置成屏幕宽度的1/2，目的是让怪物生成在屏幕外
        public float SpawnHigh;         // 怪物生成y轴范围
    }

    /// <summary>
    /// 怪物生成计时器
    /// </summary>
    public struct SpawnMonsterTimer : IComponentData
    {
        public float Value;
    }
}