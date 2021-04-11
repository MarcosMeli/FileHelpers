namespace FileHelpers
{
    /// <summary>
    /// Interface used to provide record type transformations
    /// </summary>
    public interface ITransformable<T>
    {
        /// <summary>
        /// Method called to transform the current record to Type T.
        /// </summary>
        T TransformTo();
    }
}