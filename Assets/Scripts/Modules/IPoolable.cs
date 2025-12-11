/// <summary>
/// To reset runtime data when using object pooling :)
/// </summary>

public interface IPoolable {
    
    public abstract void OnSpawnFromPool();
    public abstract void OnReturnToPool();

}