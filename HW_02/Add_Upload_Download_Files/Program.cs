using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//������� ���� �� ��������� ��� �������� ��������� ������. 
//    ����������� �������� ������ � ���������� �� � �����. 
//    �������� ������ ��������, ��� �������� �� �������, ����� ��������� ��� ����� ����������� � �����.
//    ��� �������� ������������ �����, �������� ����.


DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
defaultFilesOptions.DefaultFileNames.Clear();
defaultFilesOptions.DefaultFileNames.Add("html/index.html");

// ���� ��� ����� ��������� �����
var uploadPath = $"{Directory.GetCurrentDirectory()}/wwwroot/uploads";
app.UseDirectoryBrowser(new DirectoryBrowserOptions()
{
    FileProvider = new PhysicalFileProvider(uploadPath),
    RequestPath = new PathString("/uploads")
});

app.UseDefaultFiles(defaultFilesOptions);
app.UseStaticFiles();


app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;
    response.ContentType = "text/html; charset=utf-8";

    if (request.Path == "/upload" && request.Method == "POST")
    {
        IFormFileCollection files = request.Form.Files;
        if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

        foreach (var file in files)
        {
            // ���� � ����� uploads
            string fullPath = $"{uploadPath}/{file.FileName}";

            // ��������� ���� � ����� uploads
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
        await response.WriteAsync("����� ������� ���������.<br><a href='/'>��������� ���</a>");
    }

    else if (request.Path == "/getFile")
    {
        string fileName = request.Query["fileName"];
        string fullFileName = $"{uploadPath}/{fileName}";
        if (File.Exists(fullFileName))
        {
            context.Response.Headers.ContentDisposition = $"attachment; filename=\"{fileName}\"";
            response.ContentType = "application/octet-stream";
            await context.Response.SendFileAsync(fullFileName);
        }
        else
        {
            response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("File Not Found");
        }
    }
    else
    {
        response.Redirect("/");
        //await response.SendFileAsync("html/index.html");
    }
});

app.Run();