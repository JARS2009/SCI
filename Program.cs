using System;
using System.Threading;

namespace SCI
{
    class Program
    {
        // Credenciales
        const string USER = "admin";
        const string PASS = "1234";

        // Configuración del edificio
        const int PISOS = 2;
        const int SALONES_POR_PISO = 8;

        // Rangos de temperatura
        const int TEMP_MIN = 20;
        const int TEMP_MAX_NORMAL = 30;
        const int TEMP_ALERTA = 50;

        // Variables individuales de temperatura
        static int temp11, temp12, temp13, temp14, temp15, temp16, temp17, temp18;
        static int temp21, temp22, temp23, temp24, temp25, temp26, temp27, temp28;
        // Variables individuales de humo
        static bool humo11, humo12, humo13, humo14, humo15, humo16, humo17, humo18;
        static bool humo21, humo22, humo23, humo24, humo25, humo26, humo27, humo28;

        static void Main()
        {
            if (!Login()) return;
            InitSensores();
            MenuPrincipal();
        }

        static bool Login()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Usuario: ");
                string u = Console.ReadLine();
                Console.Write("Clave: ");
                string p = Console.ReadLine();

                if (u == USER && p == PASS) return true;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("¡Credenciales incorrectas!");
                Console.ResetColor();
                Thread.Sleep(800);
            }
        }

        static void InitSensores()
        {
            var rnd = new Random();
            for (int piso = 1; piso <= PISOS; piso++)
            {
                for (int salon = 1; salon <= SALONES_POR_PISO; salon++)
                {
                    int t = rnd.Next(TEMP_MIN, TEMP_MAX_NORMAL + 1);
                    SetTemp(piso, salon, t);
                    SetHumo(piso, salon, false);
                }
            }
        }

        static void MenuPrincipal()
        {
            int op;
            do
            {
                Console.Clear();
                Console.WriteLine("=== SISTEMA CONTRA INCENDIOS ===");
                Console.WriteLine("1) Simulación en tiempo real");
                Console.WriteLine("2) Probar sensor único");
                Console.WriteLine("3) Salir");
                Console.Write("Opción: ");

                if (!int.TryParse(Console.ReadLine(), out op)) op = 0;

                switch (op)
                {
                    case 1: Simular(); break;
                    case 2: ProbarSensor(); break;
                    case 3: Console.WriteLine("Adiós..."); Thread.Sleep(500); break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Opción inválida");
                        Console.ResetColor();
                        Thread.Sleep(700);
                        break;
                }
            } while (op != 3);
        }

        static void Simular()
        {
            var rnd = new Random();
            Console.Clear();
            Console.WriteLine("Presiona ESC para volver.");

            while (true)
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                    break;

                // Actualiza todos los sensores
                for (int piso = 1; piso <= PISOS; piso++)
                {
                    for (int salon = 1; salon <= SALONES_POR_PISO; salon++)
                    {
                        int t = (rnd.Next(100) < 90)
                            ? rnd.Next(TEMP_MIN, TEMP_MAX_NORMAL + 1)
                            : rnd.Next(TEMP_MAX_NORMAL + 1, TEMP_ALERTA + 20);
                        bool h = (rnd.Next(100) < 5);

                        SetTemp(piso, salon, t);
                        SetHumo(piso, salon, h);
                    }
                }

                MostrarEdificio();
                Thread.Sleep(1000);
            }
        }

        static void MostrarEdificio()
        {
            Console.Clear();
            // Pintamos desde el último piso al primero
            for (int piso = PISOS; piso >= 1; piso--)
            {
                Console.WriteLine($" Piso {piso} ");
                for (int salon = 1; salon <= SALONES_POR_PISO; salon++)
                {
                    int t = GetTemp(piso, salon);
                    bool h = GetHumo(piso, salon);
                    bool alerta = t >= TEMP_ALERTA || h;

                    Console.BackgroundColor = alerta ? ConsoleColor.Red : ConsoleColor.Green;
                    Console.Write($" {salon}:{t}° ");
                    Console.ResetColor();
                    if (alerta) Console.Beep();
                }
                Console.WriteLine("\n");
            }
        }

        static void ProbarSensor()
        {
            var rnd = new Random();
            int piso, salon;

            // Elegir piso
            do
            {
                Console.Clear();
                Console.Write($"Escoge piso (1–{PISOS}): ");
            } while (!int.TryParse(Console.ReadLine(), out piso) || piso < 1 || piso > PISOS);

            // Elegir salón
            do
            {
                Console.Clear();
                Console.Write($"Escoge salón (1–{SALONES_POR_PISO}): ");
            } while (!int.TryParse(Console.ReadLine(), out salon) || salon < 1 || salon > SALONES_POR_PISO);

            // Simular valores
            int t = rnd.Next(TEMP_MIN, TEMP_ALERTA + 20);
            bool h = rnd.Next(100) < 5;

            Console.Clear();
            Console.WriteLine($"Sensor Piso {piso}, Salón {salon}");
            Console.WriteLine($"Temperatura: {t}°C");
            Console.WriteLine($"Humo:        {(h ? "Sí" : "No")}");
            Console.WriteLine("\nPresiona tecla para volver...");
            Console.ReadKey(true);
        }

        static int GetTemp(int piso, int salon)
        {
            switch (piso)
            {
                case 1:
                    switch (salon)
                    {
                        case 1: return temp11;
                        case 2: return temp12;
                        case 3: return temp13;
                        case 4: return temp14;
                        case 5: return temp15;
                        case 6: return temp16;
                        case 7: return temp17;
                        case 8: return temp18;
                    }
                    break;
                case 2:
                    switch (salon)
                    {
                        case 1: return temp21;
                        case 2: return temp22;
                        case 3: return temp23;
                        case 4: return temp24;
                        case 5: return temp25;
                        case 6: return temp26;
                        case 7: return temp27;
                        case 8: return temp28;
                    }
                    break;
            }
            return 0;
        }

        static bool GetHumo(int piso, int salon)
        {
            switch (piso)
            {
                case 1:
                    switch (salon)
                    {
                        case 1: return humo11;
                        case 2: return humo12;
                        case 3: return humo13;
                        case 4: return humo14;
                        case 5: return humo15;
                        case 6: return humo16;
                        case 7: return humo17;
                        case 8: return humo18;
                    }
                    break;
                case 2:
                    switch (salon)
                    {
                        case 1: return humo21;
                        case 2: return humo22;
                        case 3: return humo23;
                        case 4: return humo24;
                        case 5: return humo25;
                        case 6: return humo26;
                        case 7: return humo27;
                        case 8: return humo28;
                    }
                    break;
            }
            return false;
        }

        static void SetTemp(int piso, int salon, int valor)
        {
            switch (piso)
            {
                case 1:
                    switch (salon)
                    {
                        case 1: temp11 = valor; break;
                        case 2: temp12 = valor; break;
                        case 3: temp13 = valor; break;
                        case 4: temp14 = valor; break;
                        case 5: temp15 = valor; break;
                        case 6: temp16 = valor; break;
                        case 7: temp17 = valor; break;
                        case 8: temp18 = valor; break;
                    }
                    break;
                case 2:
                    switch (salon)
                    {
                        case 1: temp21 = valor; break;
                        case 2: temp22 = valor; break;
                        case 3: temp23 = valor; break;
                        case 4: temp24 = valor; break;
                        case 5: temp25 = valor; break;
                        case 6: temp26 = valor; break;
                        case 7: temp27 = valor; break;
                        case 8: temp28 = valor; break;
                    }
                    break;
            }
        }

        static void SetHumo(int piso, int salon, bool valor)
        {
            switch (piso)
            {
                case 1:
                    switch (salon)
                    {
                        case 1: humo11 = valor; break;
                        case 2: humo12 = valor; break;
                        case 3: humo13 = valor; break;
                        case 4: humo14 = valor; break;
                        case 5: humo15 = valor; break;
                        case 6: humo16 = valor; break;
                        case 7: humo17 = valor; break;
                        case 8: humo18 = valor; break;
                    }
                    break;
                case 2:
                    switch (salon)
                    {
                        case 1: humo21 = valor; break;
                        case 2: humo22 = valor; break;
                        case 3: humo23 = valor; break;
                        case 4: humo24 = valor; break;
                        case 5: humo25 = valor; break;
                        case 6: humo26 = valor; break;
                        case 7: humo27 = valor; break;
                        case 8: humo28 = valor; break;
                    }
                    break;
            }
        }
    }
}
