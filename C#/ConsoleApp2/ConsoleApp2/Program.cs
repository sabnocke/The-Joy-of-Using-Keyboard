

namespace ConsoleApp2
{
    public static class ProteinTranslation
    {
        private static string Mapping(string? s)
            => s switch
            {
                "AUG" => "Methionine",
                "UUU" or "UUC" => "Phenylalanine",
                "UUA" or "UUG" => "Leucine",
                "UCU" or "UCC" or "UCA" or "UCG" => "Serine",
                "UAU" or "UAC" => "Tyrosine",
                "UGU" or "UGC" => "Cysteine",
                "UGG" => "Tryptophan",
                "UAA" or "UAG" or "UGA" => "STOP",
                _ => ""
            };

        public static string[] Proteins(string strand)
        {

            var batch = strand.Chunk(3).Select(item => Mapping(new string(item)));
            return new string[10];
        }
    }

    public static void Main()
    {
        
    }
}