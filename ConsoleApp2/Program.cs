using System;

class Program
{
    const int MAX_EMPLEADOS = 5;
    const int DIAS = 5;
    const double VALOR_HORA = 8000;
    const int HORAS_NORMALES = 40;
    const double RECARGO_EXTRA = 1.25;

    static string[] nombres = new string[MAX_EMPLEADOS];
    static double[,] horas = new double[MAX_EMPLEADOS, DIAS];
    static string[] dias = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes" };
    static int cantidad = 0; // contador de empleados registrados

    static void Main()
    {
        int opcion;
        do
        {
            MostrarMenu();
            opcion = LeerEntero("Elija una opción: ", 1, 5);

            switch (opcion)
            {
                case 1: RegistrarEmpleado(); break;
                case 2: RegistrarHoras(); break;
                case 3: MostrarReporte(); break;
                case 4: EmpleadoConMasHoras(); break;
                case 5: Console.WriteLine("¡Hasta luego!"); break;
            }
        } while (opcion != 5);
    }

    static void MostrarMenu()
    {
        Console.WriteLine("\n===== CONTROL DE TURNOS - SUPERMERCADO =====");
        Console.WriteLine("1. Registrar empleado");
        Console.WriteLine("2. Registrar horas de un empleado");
        Console.WriteLine("3. Ver reporte de pagos");
        Console.WriteLine("4. Empleado con más horas");
        Console.WriteLine("5. Salir");
    }

    static int LeerEntero(string mensaje, int min, int max)
    {
        int numero;
        bool valido;
        do
        {
            Console.Write(mensaje);
            valido = int.TryParse(Console.ReadLine(), out numero);
            if (!valido || numero < min || numero > max)
            {
                Console.WriteLine("Dato inválido. Debe ser un número entre " + min + " y " + max + ".");
                valido = false;
            }
        } while (!valido);
        return numero;
    }

    static double LeerDecimal(string mensaje, double min, double max)
    {
        double numero;
        bool valido;
        do
        {
            Console.Write(mensaje);
            valido = double.TryParse(Console.ReadLine(), out numero);
            if (!valido || numero < min || numero > max)
            {
                Console.WriteLine("Dato inválido. Debe ser un número entre " + min + " y " + max + ".");
                valido = false;
            }
        } while (!valido);
        return numero;
    }

    static void RegistrarEmpleado()
    {
        if (cantidad == MAX_EMPLEADOS)
        {
            Console.WriteLine("No se pueden registrar más empleados (máximo " + MAX_EMPLEADOS + ").");
            return;
        }

        string nombre;
        do
        {
            Console.Write("Nombre del empleado: ");
            nombre = Console.ReadLine();
            if (nombre == "")
            {
                Console.WriteLine("El nombre no puede estar vacío.");
            }
        } while (nombre == "");

        nombres[cantidad] = nombre;
        cantidad++;
        Console.WriteLine("Empleado registrado correctamente.");
    }

    static void RegistrarHoras()
    {
        if (cantidad == 0)
        {
            Console.WriteLine("Primero debe registrar al menos un empleado.");
            return;
        }

        for (int i = 0; i < cantidad; i++)
        {
            Console.WriteLine((i + 1) + ". " + nombres[i]);
        }
        int e = LeerEntero("Número del empleado: ", 1, cantidad) - 1;

        for (int d = 0; d < DIAS; d++)
        {
            horas[e, d] = LeerDecimal("Horas del " + dias[d] + " (0-12): ", 0, 12);
        }
        Console.WriteLine("Horas registradas para " + nombres[e] + ".");
    }

    static double CalcularHorasSemana(int e)
    {
        double total = 0;
        for (int d = 0; d < DIAS; d++)
        {
            total = total + horas[e, d];
        }
        return total;
    }

    static double CalcularPago(double totalHoras)
    {
        double pago;
        if (totalHoras <= HORAS_NORMALES)
        {
            pago = totalHoras * VALOR_HORA;
        }
        else
        {
            double extras = totalHoras - HORAS_NORMALES;
            pago = (HORAS_NORMALES * VALOR_HORA) + (extras * VALOR_HORA * RECARGO_EXTRA);
        }
        return pago;
    }

    static void MostrarReporte()
    {
        if (cantidad == 0)
        {
            Console.WriteLine("No hay empleados registrados.");
            return;
        }

        double totalNomina = 0;
        int conExtras = 0;

        Console.WriteLine("\n--- REPORTE SEMANAL ---");
        for (int i = 0; i < cantidad; i++)
        {
            double total = CalcularHorasSemana(i);
            double pago = CalcularPago(total);
            double extras = 0;

            if (total > HORAS_NORMALES)
            {
                extras = total - HORAS_NORMALES;
                conExtras++;
            }

            totalNomina = totalNomina + pago;
            Console.WriteLine(nombres[i] + " | Horas: " + total + " | Extras: " + extras + " | Pago: $" + pago);
        }

        Console.WriteLine("-----------------------");
        Console.WriteLine("Total nómina: $" + totalNomina);
        Console.WriteLine("Empleados con horas extra: " + conExtras);
    }

    static void EmpleadoConMasHoras()
    {
        if (cantidad == 0)
        {
            Console.WriteLine("No hay empleados registrados.");
            return;
        }

        int posMayor = 0;
        double mayor = CalcularHorasSemana(0);

        for (int i = 1; i < cantidad; i++)
        {
            double total = CalcularHorasSemana(i);
            if (total > mayor)
            {
                mayor = total;
                posMayor = i;
            }
        }
        Console.WriteLine("Empleado con más horas: " + nombres[posMayor] + " (" + mayor + " horas)");
    }
}
