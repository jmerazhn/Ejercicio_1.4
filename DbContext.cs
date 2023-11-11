using Ejercicio_1._4.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ejercicio_1._4
{
    public class DbContext
    {
        string _dbPath;

        public string StatusMessage { get; set; }

        private SQLiteAsyncConnection conn;

        private async void Init()
        {
            if (conn is not null)
                return;

            conn = new(_dbPath);
            await conn.CreateTableAsync<Empleado>();
        }

        public DbContext(string dbPath)
        {
            _dbPath = dbPath;
        }


        public Task<int> Guardar(Empleado emple)
        {
            Init();
            if (emple.codigo != 0)
            {
                return conn.UpdateAsync(emple);
            }
            else
            {
                return conn.InsertAsync(emple);
            }
        }

        public Task<List<Empleado>> ObtenerListaEmpleados()
        {
            Init();
            return conn.Table<Empleado>().ToListAsync();
        }

        public Task<Empleado> ObtenerEmpleadoPorId(int pid)
        {
            Init();
            return conn.Table<Empleado>()
                .Where(i => i.codigo == pid)
                .FirstOrDefaultAsync();
        }

        public Task<int> EliminarEmpleado(Empleado emple)
        {

            return conn.DeleteAsync(emple);
        }
    }
}
