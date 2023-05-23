# Execução #
Aplicação esta na versão .net core 3.1.

Criar um publish do CalculoMelhorRotaConsole e executar o *CalculoMelhorRotaConsole.exe "C:\Folder\file.csv"*.

Executar o projeto CalculoMelhorRota.API. https://localhost:44339/help/index.html

Acessos a documentação
```
Usuario:admin
Senha:admin
```


Modelo de entrada de dados (Adicionar Rotas).

```
[
  {
    "valor": 0,
    "origem": "string",
    "destino": "string"
  }
]
```

Execução Rest
```
/v1/Rotas/MelhorRota/{rotas}

Exe: https://localhost:44339/v1/Rotas/MelhorRota/GRU-SCL
```


# Arquitetura #
A modelagem da aplicação foi baseada no DDD (Domain-Driven-Design).

Com uma arquitetura voltada para camadas isolando cada responsabilidade.

![image](https://github.com/somdesp/BancoMaster/assets/26486485/67eefb7d-83b1-47b7-8412-01b44d584647)

Dessa forma, se caso a aplicação for crescendo vamos inserindo serviços conforme a necessidade do dominio(negocio).

![image](https://github.com/somdesp/BancoMaster/assets/26486485/432a44a0-b836-46bb-9f8d-e104f63a4019)

Utilizaçoes de ViewModels para não retornar a entidade para o front.

![image](https://github.com/somdesp/BancoMaster/assets/26486485/9ab3dc11-b2c4-43c8-bdc5-191e385107dd)



# Rota de Viagem #
Escolha a rota de viagem mais barata independente da quantidade de conexões.
Para isso precisamos inserir as rotas através de um arquivo de entrada.

PS: não utilizar algoritimo Dijkstra 

## Arquivo de entrada ##
Formato:
Origem,Destino,Valor

```rotas.csv
GRU,BRC,10
BRC,SCL,5
GRU,CDG,75
GRU,SCL,20
GRU,ORL,56
ORL,CDG,5
SCL,ORL,20
```

## Explicando ## 
Uma viajem de **GRU** para **CDG** existem as seguintes rotas:

1. GRU - BRC - SCL - ORL - CDG ao custo de $40
2. GRU - ORL - CDG ao custo de $61
3. GRU - CDG ao custo de $75
4. GRU - SCL - ORL - CDG ao custo de $45

O melhor preço é da rota **1**, apesar de mais conexões, seu valor final é menor.
O resultado da consulta no programa deve ser: **GRU - BRC - SCL - ORL - CDG ao custo de $40**.

### Execução do programa ###
A inicializacao do teste se dará por linha de comando onde o primeiro argumento é o arquivo de entrada.

```cmd
$ executavel rotas.csv
```

### Projetos ###
Duas interfaces de consulta devem ser implementadas:

- Interface de console 
	O console deverá receber um input com a rota no formato "DE-PARA" e imprimir a melhor rota e valor.
  
  Exemplo:
  ```cmd
  Digite a rota: GRU-CGD
  Melhor Rota: GRU - BRC - SCL - ORL - CDG ao custo de $40
  Digite a rota: BRC-SCL
  Melhor Rota: BRC - SCL ao custo de $5
  ```

- Interface Rest
    A interface Rest deverá suportar 2 endpoints:
    - Registro de novas rotas. Essas novas rotas devem ser persistidas no arquivo csv utilizado como entrada(rotas.csv),
    - Consulta de melhor rota entre dois pontos.

## Entregáveis ##
* Envie apenas o código fonte
* Estruture sua aplicação seguindo as boas práticas de desenvolvimento
* Evite o uso de frameworks ou bibliotecas externas à linguagem
* Implemente testes unitários seguindo as boas práticas de mercado
* Em um arquivo Texto ou Markdown descreva:
  * Como executar a aplicação
  * Estrutura dos arquivos/pacotes
  * Explique as decisões de design adotadas para a solução
  * Descreva sua API Rest de forma simplificada
