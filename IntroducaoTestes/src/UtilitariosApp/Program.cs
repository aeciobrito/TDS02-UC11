using UtilitariosApp;

/*
Calculadora c1 = new Calculadora();

var resultado = c1.Somar(14, 17);
var resultado2 = c1.Multiplicar(14, 17);


Console.WriteLine($"O resultado da soma é: {resultado}");
Console.WriteLine($"O resultado da multiplicação é: {resultado2}");
*/

// == Testes com Clientes e Endereço ==
var endereco = new Endereco("Rua B", 123, "São Paulo", "SP");
var cliente = new Cliente(1, "Silva", "jsilva@mail.com", endereco);

Console.WriteLine(cliente.ObterInformacoes());