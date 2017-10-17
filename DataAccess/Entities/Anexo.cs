using System;
using System.IO;

namespace DataAccess.Entities
{
    public class Anexo
    {
        public string Nome { get; set; }
        public int Tamanho { get; set; }
        public MemoryStream Arquivo { get; set; }
        public string Url { get; set; }
        public DateTime DataInclusao { get; set; }
    }

    public class AnexoRelacionado
    {
        public int Index { get; set; }
        public string GetRelatedFilename { get; set; }
        public string GetRelatedContentID { get; set; }
    }
}
