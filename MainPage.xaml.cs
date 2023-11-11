using Ejercicio_1._4.Models;

namespace Ejercicio_1._4
{
    public partial class MainPage : ContentPage
    {
        //private readonly IMediaPicker mediaPicker;
        byte[] imageToSave;

        public MainPage()
        {
            InitializeComponent();
            
        }

        private async void btnFoto_Clicked(object sender, EventArgs e)
        {

            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    imageToSave = null;
                    // save the file into local storage
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    using Stream sourceStream = await photo.OpenReadAsync();
                    using FileStream localFileStream = File.OpenWrite(localFilePath);

                    await sourceStream.CopyToAsync(localFileStream);

                    var stream =photo.OpenReadAsync().Result;
                    using(MemoryStream ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        imageToSave=ms.ToArray();
                    }

                    img.Source = await LoadPhotoAsync(photo);

                }
            }


        }

        private async void btnSQlite_Clicked(object sender, EventArgs e)
        {
            var empleado = new Empleado
            {
                nombres = txtnombre.Text,
                descripcion = txtdescripcion.Text,
                imagen = imageToSave
            };

            var resultado = await App._DbContext.Guardar(empleado);

            if (resultado != 0)
            {
                await DisplayAlert("Aviso", "Datos guardado con exito!!", "Ok");
            }
            else
            {
                await DisplayAlert("Aviso", "Ha Ocurrido un Error", "Ok");
            }


            await Navigation.PopAsync();


        }

        private byte[] GetImageBytes(Stream stream)
        {
            byte[] ImageBytes;
            using (var memoryStream = new System.IO.MemoryStream())
            {
                stream.CopyTo(memoryStream);
                ImageBytes = memoryStream.ToArray();
            }
            return ImageBytes;
        }

        public async Task<String> LoadPhotoAsync(FileResult photo)
        {
            var stream = photo.OpenReadAsync().Result;

            byte[] imagedata;

            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                imagedata = ms.ToArray();
            }

            var folderpath = Path.Combine(FileSystem.AppDataDirectory, "EmployeePhoto");
            if (!File.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }

            var empfilename = Guid.NewGuid() + "_employeephoto.jpg";

            var newfile = Path.Combine(folderpath, empfilename);// Complete Path of the photo

            using (var stream2 = new MemoryStream(imagedata))
            using (var newstream = File.OpenWrite(newfile))
            {
                await stream2.CopyToAsync(newstream);
            }

            return newfile;
        }
    }
}