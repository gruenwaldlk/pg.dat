# `pg.dat` Library

> ⚠️ **WARNING**: In an attempt to solidify and unite all Alamo-Engine related tools/libraries made by the Empire at War community, development has been moved to the consolidated [AlamoEngine-Tools/PetroglyphTools](https://github.com/AlamoEngine-Tools/PetroglyphTools repository).

## String File Format

Implements the Petroglyph `*.DAT` file type in `C#`.

Special thanks to [@MikeLankamp](https://github.com/MikeLankamp) for decoding the files and hosting them on [Petrolution](https://modtools.petrolution.net/docs/DatFileFormat).

### Introduction

String Files (extension: .DAT) in Petroglyph's games hold `<key, value>` string pairs and allow the game to be easily ported across different languages by having a separate String File for each language. In such a case, the sets of keys are typically identical for the different language string files while the sets of values differ for each language.

The keys are 8-bit ASCII strings and the values are 16-bit UTF-16LE strings.

### Format Specification

Each string file begins with a header, followed by the string index table and finally the string data itself.

Note that all string data is stored back-to-back, without zero-termination. All value strings are stored first, followed by all key strings.

#### Header

| Offset   | Name  | Type     | Description                                  |
| :------: | :---: | :------: | :--------------------------------------------|
| `+0000h` | count | `uint32` | Number of records in the String Index Table. |

#### String Index Table Record

| Offset   | Name      | Type     | Description                                |
| :------: | :-------: | :------: | :------------------------------------------|
| `+0000h` | crc       | `uint32` | CRC-32 of the key.                         |
| `+0004h` | nValChars | `uint32` | Length of the value string, in characters. |
| `+0008h` | nKeyChars | `uint32` | Length of the key string, in characters.   |

#### String Data
| Offset   | Type      | Description                             |
| :------: | :-------: | :-------------------------------------- |
| `+0000h` | `unichar` | `String_Index_Table[0].nValChars`       |
| `+????h` | `unichar` | ...                                     |
| `+????h` | `unichar` | `String_Index_Table[count-1].nValChars` |
| `+????h` | `char`    | `String_Index_Table[0].nKeyChars`       |
| `+????h` | `char`    | ...                                     |
| `+????h` | `char`    | `String_Index_Table[count-1].nKeyChars` |

### Sortedness

The String Index table is not necessarily sorted on the CRC. Typically, there are two usages for String Files: game strings and credits. In the former case, language-dependent values are located by doing a lookup based on the language-independent key. For those files, the String Index Table is usually sorted. For the latter case, the strings are displayed one by one and, in fact, cannot be sorted by key.

A solution is, when reading the file, to keep track of the sortedness of the index (the CRC of the previously read entry should always be less than the CRC of the currently read entry), and when it turns out the file is sorted, O(log n) lookups can be performed instead of O(n).
