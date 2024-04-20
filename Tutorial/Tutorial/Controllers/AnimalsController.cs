using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Tutorial.Models;
using Tutorial.Models.DTOs;

namespace Tutorial.Controllers;


[ApiController]
//[Route("api/animals")]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{

    private readonly IConfiguration _configuration;
    public AnimalsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    // [HttpGet]
    // public IActionResult GetAnimals()
    // {
    //     //Otwieramy polaczenie
    //     SqlConnection connection=new SqlConnection(_configuration.GetConnectionString("Default"));
    //     connection.Open();
    //     
    //     
    //     
    //     //Definiujemy commanda
    //     SqlCommand command = new SqlCommand();
    //     command.Connection = connection;
    //     command.CommandText = "SELECT * FROM Animal";
    //     
    //     //Wykonanie commanda
    //     var reader = command.ExecuteReader();
    //     List<Animal> animals = new List<Animal>();
    //
    //     int idAnimalOrdinal = reader.GetOrdinal("IdAnimal");
    //     int nameOrdinal = reader.GetOrdinal("Name");
    //     
    //     while (reader.Read())
    //     {
    //             animals.Add(new Animal()
    //             {
    //                 //Name = reader.GetString(1)
    //                 IdAnimal = reader.GetInt32(idAnimalOrdinal),
    //                 Name = reader.GetString(nameOrdinal)
    //                 
    //             });
    //     }
    //     
    //     
    //     return Ok(animals);
    // }
    
    
    [HttpGet]
    public IActionResult GetAnimals(string orderBy = "name")
    {
        //Otwieramy polaczenie
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        
        if (orderBy != "name" && orderBy != "description" && orderBy != "category" && orderBy != "area")
        {
            orderBy = "name";
        }

        //Definiujemy commanda
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = $"SELECT * FROM Animal ORDER BY {orderBy}";

        //Wykonanie commanda
        using var reader = command.ExecuteReader();
        List<Animal> animals = new List<Animal>();

        int idAnimalOrdinal = reader.GetOrdinal("IdAnimal");
        int nameOrdinal = reader.GetOrdinal("Name");

        while (reader.Read())
        {
            animals.Add(new Animal()
            {
                IdAnimal = reader.GetInt32(idAnimalOrdinal),
                Name = reader.GetString(nameOrdinal)
            });
        }

        return Ok(animals);
    }

    
    
    
    
    
    
    
    
    
    
    

    [HttpPost]
    public IActionResult AddAnimal(AddAnimal animal)
    {
        /*using ()
        {  
        }
        try
        {
        }
        finally
        {
            connection.Dispose();
        }*/
        
        //Otwieramy polaczenie
        SqlConnection connection=new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        
        
        
        //Definiujemy commanda
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "INSERT INTO Animal (Name, Description, Category, Area) VALUES (@animalName, @animalDescription, @animalCategory, @animalArea)";
        command.Parameters.AddWithValue("@animalName", animal.Name);
        command.Parameters.AddWithValue("@animalDescription", animal.Description );
        command.Parameters.AddWithValue("@animalCategory", animal.Category );
        command.Parameters.AddWithValue("@animalArea", animal.Area );
        
        command.ExecuteNonQuery();
        
        return Created("", null);
    }
    
    
    
    [HttpPut("{idAnimal}")]
    public IActionResult UpdateAnimal(int idAnimal, string name, string description, string category, string area)
    {
        //Otwieramy polaczenie
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
    
        //Definiujemy commanda
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "UPDATE Animal SET Name = @animalName, Description = @animalDescription, Category = @animalCategory, Area = @animalArea WHERE IdAnimal = @idAnimal";
        command.Parameters.AddWithValue("@animalName", name);
        command.Parameters.AddWithValue("@animalDescription", description );
        command.Parameters.AddWithValue("@animalCategory", category ); 
        command.Parameters.AddWithValue("@animalArea", area ); 
        command.Parameters.AddWithValue("@idAnimal", idAnimal);
        command.ExecuteNonQuery();

        return Ok();
    }
    
    
    
    
    
    
}
