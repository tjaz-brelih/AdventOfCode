using var file = new StreamReader("input.txt");

var input = file.ReadLine();

var index = 0;
Span<byte> packetData = stackalloc byte[input!.Length * 4];

foreach (var c in input)
{
    var hexValue = Uri.FromHex(c);

    for (int i = 0; i < 4; i++)
    {
        packetData[index++] = (byte) ((hexValue & (1 << (3 - i))) > 0 ? 1 : 0);
    }
}



var packet = DecodePacket(packetData);

var resultOne = packet!.VersionSum;
Console.WriteLine(resultOne);



var resultTwo = packet.Value;
Console.WriteLine(resultTwo);



static Packet? DecodePacket(ReadOnlySpan<byte> packet)
{
    if (packet.Length < 11)
    {
        return null;
    }

    var version = (byte) DecodeBits(packet[0..], 3);
    var typeId = (byte) DecodeBits(packet[3..], 3);

    if (typeId == 4)
    {
        var index = 6;
        var leadingBit = 0;
        var value = 0ul;

        do
        {
            for (int i = 0; i < 4; i++)
            {
                value = (value << 1) | packet[index + i + 1];
            }

            leadingBit = packet[index];
            index += 5;
        } while (leadingBit != 0);

        return new(version, typeId, index, Literal: value);
    }

    var lengthTypeId = packet[6];
    var width = lengthTypeId == 0 ? 15 : 11;
    var length = DecodeBits(packet[7..], width);

    var nextPacketIndex = 7 + width;
    var packetNumber = 0;

    var endRange = (int) (lengthTypeId == 0 ? 7 + width + length : packet.Length);

    List<Packet> packets = new();

    while (!(lengthTypeId == 1 && packetNumber++ == length))
    {
        var newPacket = DecodePacket(packet[nextPacketIndex..endRange]);
        if (newPacket is null)
        {
            break;
        }

        packets.Add(newPacket);
        nextPacketIndex += newPacket.Length;
    }

    return new(version, typeId, nextPacketIndex, SubPackets: packets);
}


static uint DecodeBits(ReadOnlySpan<byte> packet, int count)
{
    uint value = 0;
    for (var i = 0; i < count; i++)
    {
        value |= (uint) packet[i] << (count - i - 1);
    }
    return value;
}


record class Packet(byte Version, byte TypeId, int Length, ulong Literal = 0, List<Packet>? SubPackets = null)
{
    public int VersionSum => this.Version + (this.SubPackets?.Sum(p => p.VersionSum) ?? 0);

    public ulong Value => this.TypeId switch
    {
        4 => this.Literal,

        0 => this.SubPackets!.Aggregate(0ul, (a, p) => a + p.Value),
        1 => this.SubPackets!.Aggregate(1ul, (a, p) => a * p.Value),

        2 => this.SubPackets!.Min(p => p.Value),
        3 => this.SubPackets!.Max(p => p.Value),

        5 => this.SubPackets![0].Value > this.SubPackets[1].Value ? 1ul : 0ul,
        6 => this.SubPackets![0].Value < this.SubPackets[1].Value ? 1ul : 0ul,
        7 => this.SubPackets![0].Value == this.SubPackets[1].Value ? 1ul : 0ul,

        _ => throw new ArgumentOutOfRangeException(nameof(this.TypeId))
    };
}
