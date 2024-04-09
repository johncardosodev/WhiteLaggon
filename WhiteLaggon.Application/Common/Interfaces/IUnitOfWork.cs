namespace CardosoResort.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        //A interface IUnitOfWork define um contrato para uma classe que será responsavel por lidar com operações relacionadas a transações.
        IVillaRepository Villas { get; }   //Propriedade Villas do tipo IVillaRepository que retorna um repositório de villas para interagir com as villas no banco de dados

        IVillaFracaoRepository VillasFracao { get; } //Propriedade VillasFracao do tipo IVillaFracaoRepository que retorna um repositório de villas fracao para interagir com as villas fracao no banco de dados

        //Apenas preciameos de um get, pois não queremos que a classe que implementa a interface IUnitOfWork possa alterar o repositório de villas
        void Save();
    }
}