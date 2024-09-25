# Sales Order

## Requisitos para Rodar a Aplicação Localmente

Para rodar a aplicação localmente, você precisará dos seguintes requisitos:

- .NET 6 SDK
- Docker
- Docker Compose

## Docker Compose para Levantar Banco de Dados

Para iniciar o banco de dados, execute o comando raiz do projeto:
    
```bash
docker-compose up -d
```

## Executando a Aplicação

Para executar a aplicação, siga os passos abaixo:

1. Certifique-se de que todos os requisitos estão instalados.
2. Levante o banco de dados com Docker Compose utilizando o comando:
```bash
docker-compose up -d
```
3. Execute a aplicação utilizando o comando:
```bash
dotnet run --project src/WebApi/WebApi.csproj
```

**Observação**: Ao executar o projeto as migrations e seeds serão executadas automaticamente.
## Arquitetura Utilizada

Domain-Driven Design (DDD) com Clean Architecture  
A aplicação segue os princípios do Domain-Driven Design (DDD) e Clean Architecture. A estrutura do projeto é dividida em camadas, cada uma com responsabilidades bem definidas:

- **Domain**: contém as entidades, objetos de valor, agregados e interfaces de repositório.
- **Application**: contém os casos de uso da aplicação.
- **Infrastructure**: contém as implementações concretas das interfaces definidas no domínio e a comunicação com interfaces externas.
- **WebApi**: contém os controladores da aplicação.
- **CrossCutting**: contém classes e métodos compartilhados entre as camadas.
- **UnitTest**: contém os testes unitários e de integração da aplicação.

## Padrões e Bibliotecas Utilizadas

### Mediator
O padrão de design Mediator é utilizado para gerenciar a comunicação entre objetos de forma desacoplada. Na aplicação, o Mediator é utilizado para orquestrar os casos de uso (use cases) e os comandos (commands). A biblioteca MediatR é utilizada para implementar este padrão, facilitando a comunicação entre os handlers e os comandos/queries.

### FluentValidation
A biblioteca FluentValidation é utilizada para validar os dados de entrada da aplicação. Na aplicação, os validators são utilizados para validar os comandos e queries antes de serem executados.  
Em conjunto ao MediatoR, os validators são executados automaticamente antes de cada comando ou query ser executado.

### Bogus
A biblioteca Bogus é utilizada para gerar dados fictícios para os testes de unitários. Com ela, é possível gerar dados aleatórios para os testes de forma simples e rápida.

## Swagger

A aplicação possui a documentação da API gerada automaticamente pelo Swagger. Para acessar a documentação, acesse a URL abaixo:

``` 
https://localhost:7065/swagger
```

## API's

- **GET /api/v1/orders/{id}**: Retorna um pedido de venda específico.
  - **Query Params**:
    - **id**: ID do pedido de venda.
  - **Response**:
    - **200 OK**: Objeto do pedido de venda.
    - **404 Not Found**: Pedido de venda não encontrado.
  

- **POST /api/v1/orders**: Cria um novo pedido.
  - **Request Body**:
    - **number**: Número do pedido.
    - **saleDate**: Data da venda.
    - **customerId**: ID do cliente.
    - **merchantId**: ID do comerciante.
    - **products**: Produtos do pedido.
      - **productId**: ID do produto.
      - **quantity**: Quantidade do produto.
  - **Response**:
    - **201 Created**: Pedido criado com sucesso.
    - **400 Bad Request**: Erro de validação no corpo da requisição.
    - **500 Internal Server Error**: Erro interno no servidor.
  - Observação: Caso o produto não tenha quantidade suficiente em estoque, o pedido não será criado e o sistema retornará um erro de validação.
  

- **PUT /api/v1/orders/{id}**: Atualiza um pedido existente.
  - **Query Params**:
    - **id**: ID do pedido de venda.
  - **Request Body**:
    - **number**: Número do pedido.
    - **saleDate**: Data da venda.
    - **customerId**: ID do cliente.
    - **merchantId**: ID do comerciante.
    - **products**: Produtos do pedido.
      - **productId**: ID do produto.
      - **quantity**: Quantidade do produto.
  - **Response**:
    - **200 OK**: Pedido atualizado com sucesso.
    - **400 Bad Request**: Erro de validação no corpo da requisição.
    - **404 Not Found**: Pedido de venda não encontrado.
    - **500 Internal Server Error**: Erro interno no servidor.
  - **Observações**: 
    - É possível cancelar um produto específico de um pedido ao atualizar a ordem. Para isso, adicione a propriedade `isCanceled` no corpo da requisição para cada produto. Quando `isCanceled` for `true`, o produto será removido do cálculo do valor total do pedido, permitindo que o pedido continue sem o produto cancelado.
    - Para adicionar novos produtos basta seguir o padrão de adicionar um novo objeto no array `products` com as propriedades `productId` e `quantity` e `id` como `null`.</p>
    - Para atualizar um produto existente, basta adicionar o `id` do produto no objeto do produto e as propriedades `productId` e `quantity`.

  
- **DELETE /api/v1/orders/{id}**: Cancela um pedido de venda específico.
  - **Path Params**:
    - **id**: ID do pedido de venda.
  - **Response**:
    - **200 OK**: Pedido cancelado com sucesso.
    - **404 Not Found**: Pedido de venda não encontrado.
    - **500 Internal Server Error**: Erro interno no servidor.
  - **Observação**: Ao cancelar um pedido, o status do pedido é alterado para `Canceled`. Ao tentar cancelar um pedido que já foi cancelado, o sistema retornará sucesso sendo uma API idempotente (não houve alterações, mas o resultado esperado final já foi obtido).


