using WebApi.Domain.Entidades.Base;

namespace WebApi.Domain.Entidades.Estudante;

public class Estudante : EntidadeBase
{
    public Estudante() { }

    private Estudante(string nome, int idade, int serie, double notaMedia, string endereco, string nomePai, string nomeMae, DateTime dataNascimento)
    {
        Nome = nome;
        Idade = idade;
        Serie = serie;
        NotaMedia = notaMedia;
        Endereco = endereco;
        NomePai = nomePai;
        NomeMae = nomeMae;
        DataNascimento = dataNascimento;
    }

    public string Nome { get; private set; }
    public int Idade { get; private set; }
    public int Serie { get; private set; }
    public double NotaMedia { get; private set; }
    public string Endereco { get; private set; }
    public string NomePai { get; private set; }
    public string NomeMae { get; private set; }
    public DateTime DataNascimento { get; private set; }

    public static Estudante Criar(string nome, int idade, int serie, double notaMedia, string endereco, string nomePai, string nomeMae, DateTime dataNascimento)
    {
        return new Estudante(nome, idade, serie, notaMedia, endereco, nomePai, nomeMae, dataNascimento);
    }
}
