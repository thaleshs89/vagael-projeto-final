# locathales
Diagramas arquitetural da aplicação. utilizado o site 4model.com
- Diagramas

![Alt text](img_readme/persons.png?raw=true "Diagrama de Persons")
![Alt text](img_readme/DesenhoEL-Container.png?raw=true "Diagrama de Componentes")
![Alt text](img_readme/DesenhoEL-Component.png?raw=true "Diagrama Container")


*Oque foi utilizado*

- Arquitura Onin com DDD
- Cache em mémoria para melhorar perfomace
- Metodos assync
- Teste unitário XUnit
- Chamadas API utilizando HTTPClient Factory
- Flunt.Validations para validações
- EntityFrameworkCore na camada de repository
- Sqllite para vacilitar o desenvolvimento local
- Serilog para controle de logs
- AutoMapper

*Informações da Aplicação*

- Comsumo da API Externa da Fipe para Obter dados de marca e modelo do véiculo, facilitando o cadastro do veículo
- API de Cliente - Cadastro e Obter Cliente
- API de Operador - Cadastro e Obter Operador
- API de Veículo - Cadastro do Veículo - Obter Veículo por placa - Listar Veiculo por Categoria 
- API de Contrato Locação - Cadastro Contrato - Alterar Status Contrato - Obter Contrato por numero contrato
