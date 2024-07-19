namespace PM2App2.View
{
    public partial class actuEmple : ContentPage
    {
        FileResult photo;
        private int empleadoId;

        public actuEmple(int empleId)
        {
            InitializeComponent();
            empleadoId = empleId;
            LoadEmployeeData(empleadoId);
        }

        public String Getimage64()
        {
            if (photo != null)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    Stream stream = photo.OpenReadAsync().Result;
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
                    Stream stream = photo.OpenReadAsync().Result;
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

                foto.Source = ImageSource.FromStream(() => photo.OpenReadAsync().Result);

                await sourceStream.CopyToAsync(localFileStream);
            }
        }

        private async void btnsave_Clicked(object sender, EventArgs e)
        {
            var emple = new Models.Empleado
            {
                id = empleadoId,
                nombre = txtNombre.Text,
                apellido = txtApellido.Text,
                direccion = txtDireccion.Text,
                foto = Getimage64()
            };

            int result = await Controllers.EmpleadosController.Update(emple);

            if (result == 0)
            {
                await DisplayAlert("Error", "Los datos no se pudieron actualizar", "OK");
            }
            else
            {
                await DisplayAlert("", "Actualizado con éxito", "OK");
            }
        }

        private async void LoadEmployeeData(int empleId)
        {
            var empleados = await Controllers.EmpleadosController.Get();
            var emple = empleados.FirstOrDefault(e => e.id == empleId);
            if (emple != null)
            {
                txtNombre.Text = emple.nombre;
                txtApellido.Text = emple.apellido;
                txtDireccion.Text = emple.direccion;
                if (!string.IsNullOrEmpty(emple.foto))
                {
                    foto.Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(emple.foto)));
                }
            }
        }
    }
}

