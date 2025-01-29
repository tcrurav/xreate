using Unity.Netcode;
using Unity.Collections;
using System;

[Serializable]
public struct PlayerSceneEntry : INetworkSerializable
{
    public int PlayerId;
    public FixedString128Bytes Scene;

    public PlayerSceneEntry(int playerId, string scene)
    {
        PlayerId = playerId;
        Scene = scene;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref PlayerId);
        serializer.SerializeValue(ref Scene);
    }
}
