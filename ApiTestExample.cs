using RestSharp;

namespace ApiTests.Tests
{
    public class ApiTests
    {
        [Fact]
        public void TestGetUsers()
        {
            // Criar uma instância do cliente RestSharp
            var client = new RestClient("https://petstore.swagger.io");

            // Criar uma requisição GET para a rota '/users'
            var request = new RestRequest("v2/pet/findByStatus", Method.Get);

            // Executar a requisição
            var response = client.Execute(request);

            // Verificar se a resposta foi bem-sucedida
            Assert.Equal(200, (int)response.StatusCode);
            
            // Obter o conteúdo da resposta como uma string (JSON)
            var content = response.Content;
            System.Console.WriteLine(content);
        }
    }
}