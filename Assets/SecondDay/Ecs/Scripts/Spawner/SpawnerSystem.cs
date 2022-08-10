using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class SpawnerSystem : JobComponentSystem
{
    EndSimulationEntityCommandBufferSystem entityCommandBufferSystem;

    protected override void OnCreate()
    {
        entityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    struct SpawnJob : IJobForEachWithEntity<Spawner, LocalToWorld>
    {
        public EntityCommandBuffer commandBuffer;
        public void Execute(Entity entity, int index, [ReadOnly] ref Spawner spawner, [ReadOnly] ref LocalToWorld location)
        {
            Debug.Log("Execute");

            for (int x = 0; x < spawner.rows; x++)
            {
                for (int z = 0; z < spawner.columns; z++)
                {
                    var instance = commandBuffer.Instantiate(spawner.prefab);
                    var pos = math.transform(location.Value, new float3(x, noise.cnoise(new float2(x, z) * 0.21f), z));

                    commandBuffer.SetComponent(instance, new Translation { Value = pos });
                }
            }

            commandBuffer.DestroyEntity(entity);
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new SpawnJob
        {
            commandBuffer = entityCommandBufferSystem.CreateCommandBuffer()
        }.ScheduleSingle(this, inputDeps);

        entityCommandBufferSystem.AddJobHandleForProducer(job);

        return job;
    }
}
