using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliTool.ExternSDK.Common;

public struct UniverseUsingIdentity
{
    private byte[] m_preFlag;
    private byte[] m_mainSegment;
    private byte[] m_postFlag;

    internal bool m_unused;

    public byte[] PreFlag
    {
        get => m_preFlag;
    }
    public byte[] MainSegment
    {
        get => m_mainSegment;
    }
    public byte[] PostFlag
    {
        get => m_postFlag;
    }

    public bool Unused => m_unused;

    public static readonly UniverseUsingIdentity Zero;
    
    private static UniverseUsingIdentity _zero()
    {
        UniverseUsingIdentity uuid = new();
        uuid.m_preFlag = new byte[2] { 0, 0 };
        byte[] seg = new byte[256];
        seg.Initialize();
        uuid.m_mainSegment = seg;
        uuid.m_postFlag = new byte[2] { 0, 0 };
        return uuid;
    }

    static UniverseUsingIdentity()
    {
        Zero = _zero();
    }

    internal void SetUnused() => m_unused = true;

    public static UniverseUsingIdentity GeneraterUUID()
    {
        byte[][] _MagicNumber1 = new byte[][]
        {
            new byte[]{ 128, 64 },
            new byte[]{ 255, 16 },
            new byte[]{ 0xEF, 0x6B },
            new byte[]{ 0xAD, 0xDC },
            new byte[]{ 0xAE, 0XCB }
        };

        var _uuid = new UniverseUsingIdentity();
        _uuid.m_preFlag = _MagicNumber1[Random.Shared.Next() % 5];
        var v = new Random(Random.Shared.Next());

        byte[] MainSegmentRandom = new byte[256];
        v.NextBytes(MainSegmentRandom);

        _uuid.m_mainSegment = MainSegmentRandom;
        _uuid.m_postFlag = _MagicNumber1[Random.Shared.Next() % 5];

        return _uuid;
    }
}
