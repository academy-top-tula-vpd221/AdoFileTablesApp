using AdoFileTablesApp;
using Microsoft.Data.SqlClient;

string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=work_db;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

using (SqlConnection connection = new SqlConnection(connectionString))
{
    await connection.OpenAsync();
    Console.WriteLine("Connection is open\n");

    SqlCommand command = connection.CreateCommand();

    /*
    command.CommandText = @"CREATE TABLE files
                                (id INT IDENTITY(1,1) PRIMARY KEY,
                                title NVARCHAR(50) NOT NULL,
                                file_name NVARCHAR(50) NOT NULL,
                                file_data VARBINARY(MAX))";
    await command.ExecuteNonQueryAsync();
    */

    /*
    string filePath = @"imgs/file.png";
    string fileTitle = "иконка файла неопределенного типа";
    string fileName = filePath.Substring(filePath.LastIndexOf('/') + 1);

    command.CommandText = @"INSERT INTO files VALUES(@title, @file_name, @file_data)";
    command.Parameters.Add("@title", System.Data.SqlDbType.NVarChar, 50);
    command.Parameters.Add("@file_name", System.Data.SqlDbType.NVarChar, 50);

    byte[] fileBuffer;
    using(FileStream fs = new(filePath, FileMode.Open))
    {
        fileBuffer = new byte[fs.Length];
        fs.Read(fileBuffer, 0, fileBuffer.Length);
        command.Parameters.Add("@file_data", System.Data.SqlDbType.Image, Convert.ToInt32(fs.Length));
    }

    command.Parameters["@title"].Value = fileTitle;
    command.Parameters["@file_name"].Value = fileName;
    command.Parameters["@file_data"].Value = fileBuffer;

    await command.ExecuteNonQueryAsync();
    */

    List<Image> images = new List<Image>();

    command.CommandText = "SELECT * FROM files";
    using(SqlDataReader reader = await command.ExecuteReaderAsync())
    {
        while (await reader.ReadAsync())
        {
            int id = reader.GetInt32(0);
            string title = reader.GetString(1);
            string fileName = reader.GetString(2);
            byte[] data = (byte[])reader.GetValue(3);

            Image image = new Image(id, title, fileName, data);
            images.Add(image);
        }
    }

    if(images.Count > 0)
    {
        using(FileStream fs = new("X:/" + images[0].FileName, FileMode.OpenOrCreate))
        {
            fs.Write(images[0].Data, 0, images[0].Data.Length);
        }
    }
}