using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Events;
using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class PlayerEntityPacket : EntityPacket
{
    //Parameterless Constructor for MessagePack
    public PlayerEntityPacket()
    {
    }


    [Key(24)]
    public Access AccessLevel { get; set; }


    [Key(25)]
    public Gender Gender { get; set; }


    [Key(26)]
    public Guid ClassId { get; set; }


    [Key(27)]
    public EquipmentPacket Equipment { get; set; }


    [Key(28)]
    public long CombatTimeRemaining { get; set; }


    [Key(29)]
    public string Guild { get; set; }


    [Key(30)]
    public int GuildRank { get; set; }

    // BSC - Combat Skill Levels
    [Key(31)]
    public int MeleeLevel { get; set; }

    [Key(32)]
    public int ShieldingLevel { get; set; }

    [Key(33)]
    public int MagicLevel { get; set; }

    [Key(34)]
    public int DistanceLevel { get; set; }

    [Key(35)]
    public int MiningLevel { get; set; }

    [Key(36)]
    public int SmithingLevel { get; set; }

    [Key(37)]
    public int WoodcuttingLevel { get; set; }

    [Key(38)]
    public int CookingLevel { get; set; }

    [Key(39)]
    public int FishingLevel { get; set; }

    [Key(40)]
    public int CraftingLevel { get; set; }
}
