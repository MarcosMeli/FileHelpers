#if !NETSTANDARD
namespace FileHelpers
{
    /// <summary>Indicates the visibility of a member.</summary>
    public enum NetVisibility
    {
        /// <summary>Public visibility.</summary>
        Public = 0,

        /// <summary>Internal visibility. (Friend in VB.NET)</summary>
        Internal,

        /// <summary>Protected visibility.</summary>
        Protected,

        /// <summary>Private visibility.</summary>
        Private
    }
}
#endif
