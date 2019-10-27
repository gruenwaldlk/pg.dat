namespace pg.dat.utility
{
    /// <summary>
    /// String Files (extension: .DAT) in Petroglyph's games can occur in two types.
    /// <br/>
    /// The String Index table is not necessarily sorted on the CRC. Typically, there are two usages for String Files:
    /// game strings and credits. In the former case, language-dependent values are located by doing a lookup based on
    /// the language-independent key. For those files, the String Index Table is usually sorted. For the latter case,
    /// the strings are displayed one by one and, in fact, cannot be sorted by key.
    /// </summary>
    public enum FileType
    {
        /// <summary>
        /// A String File with its contents sorted on the key's CRC. Used for game strings and usually called
        /// <code>mastertextfile_language.dat</code>
        /// Duplicate keys are not allowed.
        /// </summary>
        SortedGameStringFile,
        /// <summary>
        /// A String File with its contents unsorted. The sequence of the items in the translation table used to
        /// generate the file dictates the sequence that ends up in the String File. Used for credits and usually
        /// called <code>creditstext_language.dat</code>
        /// Keys are used for formatting and duplicates are allowed.
        /// </summary>
        UnsortedCreditsStringFile
    }
}
