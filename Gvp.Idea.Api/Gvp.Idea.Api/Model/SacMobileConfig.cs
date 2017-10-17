using System;

namespace Gvp.Idea.Api.Model
{
    public class SacMobileConfig
    {
        public int Id { get; set; }
        public int Idc { get; set; }
        public String Nome_Exibicao { get; set; }
        public int Tipo_Imagem { get; set; }
        public String Imagem_Url { get; set; }
        public Boolean Tipo_Exibicao_Grupo { get; set; }
        public Boolean Opcao_Custom { get; set; }
        public String Frase_Mobile { get; set; }
        public String Frase_Idea { get; set; }
        public String Banner_Url { get; set; }
        public String Titulo_Sistema { get; set; }
        public String Url_Minha_Senha { get; set; }
        public int Id_Grupo_Senha { get; set; }
        public String BannerHyperlink { get; set; }
    }
}
