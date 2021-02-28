#if !NETSTANDARD
namespace FileHelpers.DataLink
{
    /// <summary>
    /// Define the different Modes of Transaction that are
    /// used by the <see cref="DatabaseStorage" />
    /// </summary>
    public enum TransactionMode
    {
        /// <summary>No transaction used.</summary>
        NoTransaction = 0,

        /// <summary>Default Transaction Mode.</summary>
        UseDefault,

        /// <summary>Chaos Level Transaction Mode.</summary>
        UseChaosLevel,

        /// <summary>ReadCommitted Transaction Mode.</summary>
        UseReadCommitted,

        /// <summary>ReadUnCommitted Transaction Mode.</summary>
        UseReadUnCommitted,

        /// <summary>Repeatable Transaction Mode.</summary>
        UseRepeatableRead,

        /// <summary>Serializable Transaction Mode.</summary>
        UseSerializable
    }
}
#endif