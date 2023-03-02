# Pokedex
Is a wiki page where you can find different information about different pokemon.
You can find a Figma preview [here](https://www.figma.com/file/1noFinasB5J37LGgrUrw7q/Pokedex?t=Rrq9T2ubfYfCijvC-6).

### Prerequisites
To run this project, you'll need to have the following installed on your machine:
- Node.js (version 18 or higher)
- .NET 7
- SQL Server(optional)

### Getting Started
To get started with this project, follow these steps:
1. Clone the repository to your machine by running this command from your terminal:

    ```git clone https://github.com/Asterrix/Pokedex.git```

2. Install the Node.js dependencies by opening up the terminal in ```Pokedex\Presentation\ClientApp``` location and running 
```npm install``` command.
3. To run the Vite App, run the following command ```npm run dev```.
4. You will need to create EntityFramework migrations if you plan to use the database but before that I recommend configuring SQL settings inside ```DependencyInjection.cs``` file found inside "Infrastructure" assembly.
5. Create EF migrations by running:
``` dotnet ef migrations add "InitialCreate" -p Infrastructure -s Presentation``` command from the Pokedex folder inside your terminal.
6. Apply the migrations by running ```dotnet ef database update -p Infrastructure -s Presentation``` command from the same Pokedex folder.
7. Run ```dotnet run --urls "https://localhost:5001" --project Presentation``` command from your terminal.
8. I recommend using Swagger for the ease of use which you can find at `https://localhost:7085/swagger/index.html` address.
9. Now you can send HTTP request to the local server.