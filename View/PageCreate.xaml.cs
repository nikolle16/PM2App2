namespace PM2App2.View;

public partial class PageCreate : ContentPage
{
    FileResult photo;

	public PageCreate()
	{
		InitializeComponent();
	}

    public String Getimage64()
    {
        if (photo != null)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                Stream stream = photo.OpenReadAsync().Result; //photo.GetStream();
                stream.CopyTo(memory);
                byte[] fotobyte = memory.ToArray();

                String Base64 = Convert.ToBase64String(fotobyte);

                return Base64;
            }
        }

        return null;
    }


    public byte[] GetByteArray()
    {
        if (photo != null)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                Stream stream = photo.OpenReadAsync().Result; //photo.GetStream()
                stream.CopyTo(memory);
                byte[] fotobyte = memory.ToArray();


                return fotobyte;
            }
        }

        return null;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        photo = await MediaPicker.Default.CapturePhotoAsync();

        if (photo != null)
        {
            string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

            using Stream sourceStream = await photo.OpenReadAsync();
            using FileStream localFileStream = File.OpenWrite(localFilePath);

            // Mostrar la foto en el objeto Image
            foto.Source = ImageSource.FromStream(() => photo.OpenReadAsync().Result);


            await sourceStream.CopyToAsync(localFileStream);
        }
    }

    private async void btnsave_Clicked(object sender, EventArgs e)
    {
        var emple = new Models.Empleado
        {
            nombre = txtNombre.Text,
            apellido = txtApellido.Text,
            direccion = txtDireccion.Text,
            foto = Getimage64()
        };

        int result = await Controllers.EmpleadosController.Create(emple);

        if (result == 0)
        {
            await DisplayAlert("Error","Los datos no se pudieron ingresar ingresar","OK");
        }
        else
        {
            await DisplayAlert("", "Ingresado con exito", "OK");
        }
    }
}