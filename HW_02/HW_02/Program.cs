using Microsoft.Data.SqlClient;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// ADO.net ?!
//Install - Package Microsoft.Data.SqlClient
// в appsettings Определили строку подклчюения к БД   "ConnectionStrings"


//Считывание строки Подключения к БД в appsettings.json  При помощи сервиса который представлен интерфейсом IConfiguration
var configurationService = app.Services.GetService<IConfiguration>();
string connectionString = configurationService["ConnectionStrings:DefaultConnection"];



app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;
    response.ContentType = "text/html; charset=utf-8";

    if (request.Path == "/")
    {
        List<User> users = new List<User>();

        string sortBy = request.Query["sortBy"];
        string searchName = request.Query["searchName"];
        string sortForm = GenerateSortAndSearchForms(sortBy, searchName);

        StringBuilder commandString = new StringBuilder("SELECT * FROM Users");

        // Для поиска
        if (!string.IsNullOrEmpty(searchName))
        {
            commandString.Append(" WHERE Name LIKE @searchName");
        }

        // Для сортировки
        if (sortBy == "name")
        {
            commandString.Append(" ORDER BY Name");
        }
        else if (sortBy == "age")
        {
            commandString.Append(" ORDER BY Age");
        }

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            SqlCommand command = new SqlCommand(commandString.ToString(), connection);
            if(!string.IsNullOrEmpty(searchName))
            {
                command.Parameters.AddWithValue("@searchName", "%" + searchName + "%");
            }

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        users.Add(new User(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
                    }
                }
            }
        }

        StringBuilder htmlContent = new StringBuilder();
        htmlContent.Append(sortForm);
        htmlContent.Append(BuildHtmlTable(users));

        await response.WriteAsync(GenerateHtmlPage(htmlContent.ToString(), "All Users from DataBase"));
    }

    else if (request.Path == "/adduser" && request.Method == "GET")
    {
        await response.WriteAsync(GenerateHtmlPage(GenerateAddUserForm(), "Add User"));
    }

    else if (request.Path == "/adduser" && request.Method == "POST")
    {
        string name = request.Form["name"];
        int age = int.Parse(request.Form["age"]);
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            //await connection.OpenAsync();
            //SqlCommand command = new SqlCommand($"insert into [Users] ([Name], [Age]) values ('{name}', {age})", connection);
            //await command.ExecuteNonQueryAsync();

            // вариант через параметризацию
            SqlCommand command = new SqlCommand("INSERT INTO Users (Name, Age) VALUES (@name, @age)", connection);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@age", age);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
        response.Redirect("/");
    }

    else if (request.Path == "/delete/user" && request.Method == "POST")
    {
        string userId = request.Form["id"];
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand($"DELETE FROM Users WHERE Id = {userId}", connection);
            command.ExecuteNonQuery();
        }
        response.Redirect("/");
    }

    else if (request.Path == "/edit/user" && request.Method == "GET")
    {
        string userId = request.Query["Id"];
        User user = null;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand($"SELECT * FROM Users WHERE Id = {userId}", connection);
            using(SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        user = new User(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                    }
                }
            }
        }
        await response.WriteAsync(GenerateHtmlPage(GenerateEditUserForm(user), "Edit User"));
    }

    else if(request.Path == "/edit/user" && request.Method == "POST")
    {
        int id = int.Parse(request.Form["Id"]);
        string name = request.Form["name"];
        int age = int.Parse(request.Form["age"]);
        using(SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand("UPDATE Users SET Name = @name, Age = @age WHERE Id = @id", connection);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@age", age);
            command.Parameters.AddWithValue("@id", id);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
        response.Redirect("/");
    }


    else
    {
        response.StatusCode = 404;
        await response.WriteAsJsonAsync("Page Not Found");
    }
});

app.Run();


//  Выводит коллекцию в виде таблицы
static string BuildHtmlTable<T>(IEnumerable<T> collection)
{
    StringBuilder tableHtml = new StringBuilder();
    //tableHtml.Append("<table>");
    tableHtml.Append("<table class=\"table\">");

    PropertyInfo[] properties = typeof(T).GetProperties();

    tableHtml.Append("<tr>");
    foreach (PropertyInfo property in properties)
    {
        tableHtml.Append($"<th>{property.Name}</th>");
    }

    tableHtml.Append("<th>Actions</th>");

    tableHtml.Append("</tr>");

    foreach (T item in collection)
    {
        tableHtml.Append("<tr>");
        foreach (PropertyInfo property in properties)
        {
            object value = property.GetValue(item);
            tableHtml.Append($"<td>{value}</td>");
        }

        // генерация кнопок в таблице с ид
        var idProperty = properties.FirstOrDefault(p => p.Name == "Id");
        if (idProperty != null)
        {
            var idValue = idProperty.GetValue(item);
            tableHtml.Append(GenerateActionButtons((int)idValue));
        }


        tableHtml.Append("</tr>");
    }

    tableHtml.Append("</table>");
    return tableHtml.ToString();
}


static string GenerateHtmlPage(string body, string header)
{
    string html = $"""
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset="utf-8" />
            <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/css/bootstrap.min.css" rel="stylesheet" 
            integrity="sha384-KK94CHFLLe+nY2dmCWGMq91rCGa5gtU4mk92HdvYe+M/SXH301p5ILy+dN9+nJOZ" crossorigin="anonymous">
            <title>{header}</title>
        </head>
        <body>
        <div class="container">
        <h2 class="d-flex justify-content-center">{header}</h2>
        <div class="mt-5">

        <a href="/adduser" class="btn btn-primary">Add User</a>
        
        </div>
        {body}
            <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/js/bootstrap.bundle.min.js" 
            integrity="sha384-ENjdO4Dr2bkBIFxQpeoTz1HIcje39Wm4jDKdf19U8gI4ddQ3GYNS7NTKfAdVQSZe" crossorigin="anonymous"></script>
        </div>
        </body>
        </html>
        """;
    return html;
}

static string GenerateActionButtons(int userId)
{
    return $"""
        <td>
            <form method="get" action="/edit/user" style="display:inline;">
                <input type="hidden" name="id" value="{userId}" />
                <button type="submit" class="btn btn-warning me-2">Edit</button>
            </form>

            <form method="post" action="/delete/user" style="display:inline;">
                <input type="hidden" name="id" value="{userId}" />
                <button type="submit" class="btn btn-danger">Delete</button>
            </form>
        </td>
    """;
}

static string GenerateAddUserForm()
{
    return """
        <form method="post" action="/adduser" class="mt-3">
            <div class="mb-3">
                <label for="name" class="form-label">Name</label>
                <input type="text" name="name" class="form-control" required />
            </div>
            <div class="mb-3">
                <label for="age" class="form-label">Age</label>
                <input type="number" name="age" class="form-control" required />
            </div>
            <button type="submit" class="btn btn-primary">Add</button>
        </form>
    """;
}

static string GenerateEditUserForm(User user)
{
    return $"""
        <form method="post" action="/edit/user" class="mt-3">
            <input type="hidden" name="id" value="{user.Id}" />
            <div class="mb-3">
                <label for="name" class="form-label">Name</label>
                <input type="text" name="name" class="form-control" value="{user.Name}" required />
            </div>
            <div class="mb-3">
                <label for="age" class="form-label">Age</label>
                <input type="number" name="age" class="form-control" value="{user.Age}" required />
            </div>
            <button type="submit" class="btn btn-primary">Save Changes</button>
        </form>
    """;
}

static string GenerateSortAndSearchForms(string selectedSortBy, string searchName = "")
{
    return $"""
        <form method="get" action="/" class="mb-3">
            <div class="mb-3">
                <label for="searchName" class="form-label">Search by Name</label>
                <input type="text" name="searchName" class="form-control" value="{searchName}" />
            </div>
            <button type="submit" class="btn btn-secondary">Search</button>
        </form>


        <form method="get" action="/" class="mb-3">
            <div class="mb-3">
                <label for="sortBy" class="form-label">Sort By</label>
                <select name="sortBy" class="form-select">
                    <option value="name" {(selectedSortBy == "name" ? "selected" : "")}>Name</option>
                    <option value="age" {(selectedSortBy == "age" ? "selected" : "")}>Age</option>
                </select>
            </div>
            <input type="hidden" name="searchName" value="{searchName}" />
            <button type="submit" class="btn btn-secondary">Sort</button>
        </form>
    """;
}


record User(int Id, string Name, int Age)
{
    public User(string Name, int Age) : this(0, Name, Age)
    {

    }
}


