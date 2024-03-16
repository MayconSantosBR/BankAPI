Baixe e instale o Visual Studio Community 2022
Link: https://visualstudio.microsoft.com/pt-br/thank-you-downloading-visual-studio/?sku=Community&channel=Release&version=VS2022&source=VSLandingPage&passive=false&cid=2030

Baixe o .NET Framework 8.0
Link: https://dotnet.microsoft.com/pt-br/download/dotnet/thank-you/sdk-8.0.203-windows-x64-installer

Após instalar ambos, abre o arquivo "BancoApi.sln", irá abrir uma instância do VS Community

Defina o projeto de instalação o "BancoApi", exemplo na imagem abaixo:
![image](https://github.com/MayconSantosBR/BankAPI/assets/102183646/828e3209-afbd-42dc-a87f-b1caed177e2d)
Explorador de Soluções -> click direito no BancoApi -> Definir projeto como inicializador/startup
![image](https://github.com/MayconSantosBR/BankAPI/assets/102183646/955882af-910e-40d0-8ad4-83b8ecb7c400)

Após isso, iniciar o projeto como "https", desta forma a API estará executando para realizar os testes integrados posteriormente
![image](https://github.com/MayconSantosBR/BankAPI/assets/102183646/947ab726-e7c1-468a-9006-fced7b36a113)

Depois de iniciar a API, um swagger irá abrir, com ele aberto, abra outra instância do "BancoApi.sln", assim você terá 2 VS Community abertos
No segundo VS Community, acione o atalho CTRL R + T, esse comando irá abrir o Explorador de Testes e rodar todos os testes disponíveis

Outra forma de fazer isso, é utilizar a pesquisa para emcontrar a função "Explorador de testes", depois você poderá acionar manualmente cada teste ou rodar todos
![image](https://github.com/MayconSantosBR/BankAPI/assets/102183646/9191d2b9-80d7-47c7-859e-b8baaed3c083)
![image](https://github.com/MayconSantosBR/BankAPI/assets/102183646/6d8b56ff-631b-49e2-8b87-451bbace3a8b)
