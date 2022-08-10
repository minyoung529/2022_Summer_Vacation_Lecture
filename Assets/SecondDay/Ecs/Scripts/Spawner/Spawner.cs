using Unity.Entities;

public struct Spawner : IComponentData
{
    public Entity prefab;
    public int rows;
    public int columns;
}
