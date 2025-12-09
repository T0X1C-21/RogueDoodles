/// <summary>
/// To reset runtime data when using object pooling :)
/// </summary>

public interface IPoolable {
    
    public void OnSpawnFromPool();
    public void OnReturnToPool();

}